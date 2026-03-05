using System;
using System.IO;
using System.Windows.Forms;
using Jakkl.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jakkl
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            var logDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Jakkl");
            Directory.CreateDirectory(logDir);

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddSimpleConsole(options =>
                {
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                });
            });

            services.AddSingleton<SettingsService>();
            services.AddSingleton<SyslogService>();
            services.AddSingleton<EventMonitorService>();
            services.AddSingleton<JakklConfig>();

            using var provider = services.BuildServiceProvider();

            var form = provider.GetRequiredService<JakklConfig>();
            Application.Run(form);
        }
    }
}
