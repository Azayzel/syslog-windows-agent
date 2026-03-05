using System;
using Microsoft.Win32;
using Microsoft.Extensions.Logging;

namespace Jakkl.Services
{
    public class AppSettings
    {
        public string WatchLogs { get; set; } = "Application";
        public string EventFilter { get; set; } = "";
        public string SyslogServer { get; set; } = "";
        public string SyslogPort { get; set; } = "514";
        public SyslogProtocol Protocol { get; set; } = SyslogProtocol.TcpWithTls;
        public bool RunOnStartup { get; set; } = true;
    }

    public class SettingsService
    {
        private const string RegistryPath = @"SOFTWARE\Jakkl";
        private const string StartupRegistryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string StartupKeyName = "Jakkl";

        private readonly ILogger<SettingsService> _logger;

        public SettingsService(ILogger<SettingsService> logger)
        {
            _logger = logger;
        }

        public AppSettings Load()
        {
            var settings = new AppSettings();

            try
            {
                using (var key = OpenSettingsKey(false))
                {
                    if (key != null)
                    {
                        settings.WatchLogs = key.GetValue("WatchLogs", "Application")?.ToString() ?? "Application";
                        settings.EventFilter = key.GetValue("Filter", "")?.ToString() ?? "";
                        settings.SyslogServer = key.GetValue("SyslogServer", "")?.ToString() ?? "";
                        settings.SyslogPort = key.GetValue("SyslogPort", "514")?.ToString() ?? "514";

                        var protocolStr = key.GetValue("Protocol", "TcpWithTls")?.ToString() ?? "TcpWithTls";
                        if (Enum.TryParse<SyslogProtocol>(protocolStr, out var protocol))
                            settings.Protocol = protocol;
                    }
                }

                settings.RunOnStartup = IsRunOnStartup();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to load settings from registry, using defaults");
            }

            return settings;
        }

        public void Save(AppSettings settings)
        {
            try
            {
                using (var key = CreateSettingsKey())
                {
                    key.SetValue("WatchLogs", settings.WatchLogs);
                    key.SetValue("Filter", settings.EventFilter);
                    key.SetValue("SyslogServer", settings.SyslogServer);
                    key.SetValue("SyslogPort", settings.SyslogPort);
                    key.SetValue("Protocol", settings.Protocol.ToString());
                }

                SetRunOnStartup(settings.RunOnStartup);

                _logger.LogInformation("Settings saved to registry");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save settings to registry");
            }
        }

        private RegistryKey OpenSettingsKey(bool writable)
        {
            // Try HKLM first (admin), fall back to HKCU
            try
            {
                var key = Registry.LocalMachine.OpenSubKey(RegistryPath, writable);
                if (key != null) return key;
            }
            catch (System.Security.SecurityException) { }

            return Registry.CurrentUser.OpenSubKey(RegistryPath, writable);
        }

        private RegistryKey CreateSettingsKey()
        {
            // Try HKLM first (admin), fall back to HKCU
            try
            {
                return Registry.LocalMachine.CreateSubKey(RegistryPath);
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogInformation("No admin access, saving settings to HKCU");
                return Registry.CurrentUser.CreateSubKey(RegistryPath);
            }
        }

        private bool IsRunOnStartup()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(StartupRegistryPath))
                {
                    return key?.GetValue(StartupKeyName) != null;
                }
            }
            catch
            {
                return false;
            }
        }

        private void SetRunOnStartup(bool enable)
        {
            try
            {
                // Use HKCU\Run for startup - doesn't require admin
                using (var key = Registry.CurrentUser.OpenSubKey(StartupRegistryPath, true))
                {
                    if (key == null) return;

                    if (enable)
                    {
                        var exePath = Environment.ProcessPath ?? System.Reflection.Assembly.GetExecutingAssembly().Location;
                        key.SetValue(StartupKeyName, exePath);
                    }
                    else
                    {
                        key.DeleteValue(StartupKeyName, false);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to update run-on-startup setting");
            }
        }
    }
}
