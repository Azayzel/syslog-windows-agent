using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Jakkl.Services;
using Microsoft.Extensions.Logging;

namespace Jakkl
{
    public class JakklConfig : Form
    {
        private readonly ILogger<JakklConfig> _logger;
        private readonly EventMonitorService _eventMonitor;
        private readonly SyslogService _syslogService;
        private readonly SettingsService _settings;

        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenu;

        // UI Controls
        private Label _logLabel;
        private ListBox _logsList;
        private Label _filterLabel;
        private ListBox _filterList;
        private Label _serverLabel;
        private TextBox _serverTextBox;
        private Label _portLabel;
        private TextBox _portTextBox;
        private Label _protocolLabel;
        private ComboBox _protocolCombo;
        private CheckBox _runOnStartup;
        private CheckBox _saveSettings;
        private Button _applyButton;
        private Button _cancelButton;
        private Button _testButton;
        private PictureBox _logoPicture;

        private const string TipText = "Jakkl Event Monitoring to Syslog";

        public JakklConfig(
            ILogger<JakklConfig> logger,
            EventMonitorService eventMonitor,
            SyslogService syslogService,
            SettingsService settings)
        {
            _logger = logger;
            _eventMonitor = eventMonitor;
            _syslogService = syslogService;
            _settings = settings;

            InitializeComponent();
            LoadSettings();
            SetupNotifyIcon();
            StartMonitoring();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Jakkl Config";
            this.ClientSize = new Size(340, 430);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = new Icon(GetType(), "events.ico");

            // Logo
            _logoPicture = new PictureBox
            {
                Location = new Point(260, 8),
                Size = new Size(60, 60),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Properties.Resources.Jakkl_sm
            };

            // Log(s) to monitor
            _logLabel = new Label
            {
                Text = "Log(s) to monitor:",
                Location = new Point(16, 16),
                AutoSize = true
            };

            _logsList = new ListBox
            {
                Location = new Point(16, 36),
                Size = new Size(230, 80),
                SelectionMode = SelectionMode.MultiSimple,
                Sorted = true
            };

            // Filter by event types
            _filterLabel = new Label
            {
                Text = "Filter by event types:",
                Location = new Point(16, 122),
                AutoSize = true
            };

            _filterList = new ListBox
            {
                Location = new Point(16, 142),
                Size = new Size(230, 80),
                SelectionMode = SelectionMode.MultiSimple
            };
            _filterList.Items.AddRange(new object[]
            {
                "All Event Types",
                "Error",
                "FailureAudit",
                "Information",
                "SuccessAudit",
                "Warning"
            });

            // Syslog Server
            _serverLabel = new Label
            {
                Text = "Syslog Server:",
                Location = new Point(16, 232),
                AutoSize = true
            };

            _serverTextBox = new TextBox
            {
                Location = new Point(16, 252),
                Size = new Size(140, 23),
                Text = "0.0.0.0"
            };

            // Port
            _portLabel = new Label
            {
                Text = "Port:",
                Location = new Point(166, 232),
                AutoSize = true
            };

            _portTextBox = new TextBox
            {
                Location = new Point(166, 252),
                Size = new Size(50, 23),
                Text = "514"
            };

            // Test button
            _testButton = new Button
            {
                Text = "Test",
                Location = new Point(226, 251),
                Size = new Size(60, 25)
            };
            _testButton.Click += TestButton_Click;

            // Protocol
            _protocolLabel = new Label
            {
                Text = "Protocol:",
                Location = new Point(16, 284),
                AutoSize = true
            };

            _protocolCombo = new ComboBox
            {
                Location = new Point(16, 304),
                Size = new Size(140, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _protocolCombo.Items.AddRange(new object[] { "TCP + TLS", "TCP", "UDP" });
            _protocolCombo.SelectedIndex = 0;

            // Run on Startup
            _runOnStartup = new CheckBox
            {
                Text = "Run on Startup",
                Location = new Point(16, 340),
                AutoSize = true,
                Checked = true
            };

            // Save Settings
            _saveSettings = new CheckBox
            {
                Text = "Save Settings",
                Location = new Point(16, 365),
                AutoSize = true,
                Checked = true
            };

            // Apply button
            _applyButton = new Button
            {
                Text = "Apply",
                Location = new Point(200, 390),
                Size = new Size(80, 28)
            };
            _applyButton.Click += ApplyButton_Click;

            // Cancel button
            _cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(16, 390),
                Size = new Size(80, 28),
                DialogResult = DialogResult.Cancel
            };
            _cancelButton.Click += CancelButton_Click;

            this.AcceptButton = _applyButton;
            this.CancelButton = _cancelButton;

            this.Controls.AddRange(new Control[]
            {
                _logoPicture, _logLabel, _logsList,
                _filterLabel, _filterList,
                _serverLabel, _serverTextBox,
                _portLabel, _portTextBox, _testButton,
                _protocolLabel, _protocolCombo,
                _runOnStartup, _saveSettings,
                _applyButton, _cancelButton
            });

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadSettings()
        {
            var settings = _settings.Load();

            _serverTextBox.Text = settings.SyslogServer;
            _portTextBox.Text = settings.SyslogPort;

            // Protocol
            _protocolCombo.SelectedIndex = settings.Protocol switch
            {
                SyslogProtocol.Tcp => 1,
                SyslogProtocol.Udp => 2,
                _ => 0
            };

            // Populate available logs
            var availableLogs = EventMonitorService.GetAvailableEventLogs();
            _logsList.Items.Clear();
            foreach (var log in availableLogs)
                _logsList.Items.Add(log);

            // Select previously saved logs
            if (!string.IsNullOrEmpty(settings.WatchLogs))
            {
                var savedLogs = settings.WatchLogs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < _logsList.Items.Count; i++)
                {
                    if (savedLogs.Contains(_logsList.Items[i].ToString(), StringComparer.OrdinalIgnoreCase))
                        _logsList.SetSelected(i, true);
                }
            }

            // Select previously saved filters
            if (!string.IsNullOrEmpty(settings.EventFilter))
            {
                var savedFilters = settings.EventFilter.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < _filterList.Items.Count; i++)
                {
                    if (savedFilters.Contains(_filterList.Items[i].ToString(), StringComparer.OrdinalIgnoreCase))
                        _filterList.SetSelected(i, true);
                }
            }
            else
            {
                _filterList.SetSelected(0, true); // "All Event Types"
            }

            _runOnStartup.Checked = settings.RunOnStartup;

            // Start minimized to tray
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void SetupNotifyIcon()
        {
            _contextMenu = new ContextMenuStrip();
            var configureItem = new ToolStripMenuItem("Configure...");
            configureItem.Click += (s, e) => ShowConfiguration();
            configureItem.Font = new Font(configureItem.Font, FontStyle.Bold);

            var eventViewerItem = new ToolStripMenuItem("Event Viewer");
            eventViewerItem.Click += (s, e) => Process.Start(new ProcessStartInfo("eventvwr.exe") { UseShellExecute = true });

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => ExitApplication();

            _contextMenu.Items.Add(configureItem);
            _contextMenu.Items.Add(eventViewerItem);
            _contextMenu.Items.Add(new ToolStripSeparator());
            _contextMenu.Items.Add(exitItem);

            _notifyIcon = new NotifyIcon
            {
                Icon = this.Icon,
                Text = TipText,
                Visible = true,
                ContextMenuStrip = _contextMenu
            };

            _notifyIcon.DoubleClick += (s, e) => ShowConfiguration();
            _notifyIcon.BalloonTipClicked += (s, e) => Process.Start(new ProcessStartInfo("eventvwr.exe") { UseShellExecute = true });

            _eventMonitor.EventReceived += OnEventReceived;
        }

        private void OnEventReceived(EventLogEntry entry)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<EventLogEntry>(OnEventReceived), entry);
                return;
            }

            _notifyIcon.ShowBalloonTip(
                5000,
                "Jakkl",
                $"Event in {entry.Source}\nType: {entry.EntryType}\nID: {entry.InstanceId}",
                ToolTipIcon.Info);
        }

        private void StartMonitoring()
        {
            var selectedLogs = _logsList.SelectedItems.Cast<string>().ToList();
            var selectedFilters = _filterList.SelectedItems.Cast<string>().ToList();

            if (selectedLogs.Count == 0)
            {
                _logger.LogWarning("No logs selected for monitoring");
                return;
            }

            _syslogService.Server = _serverTextBox.Text;
            if (int.TryParse(_portTextBox.Text, out int port))
                _syslogService.Port = port;

            _syslogService.Protocol = _protocolCombo.SelectedIndex switch
            {
                1 => SyslogProtocol.Tcp,
                2 => SyslogProtocol.Udp,
                _ => SyslogProtocol.TcpWithTls
            };

            _eventMonitor.StartWatching(selectedLogs, selectedFilters);

            var logNames = string.Join(", ", selectedLogs);
            _notifyIcon.Text = $"{TipText} ({logNames})".Truncate(63);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (_saveSettings.Checked)
            {
                var selectedLogs = string.Join(",", _logsList.SelectedItems.Cast<string>());
                var selectedFilters = string.Join(",", _filterList.SelectedItems.Cast<string>());

                var protocol = _protocolCombo.SelectedIndex switch
                {
                    1 => SyslogProtocol.Tcp,
                    2 => SyslogProtocol.Udp,
                    _ => SyslogProtocol.TcpWithTls
                };

                _settings.Save(new AppSettings
                {
                    WatchLogs = selectedLogs,
                    EventFilter = selectedFilters,
                    SyslogServer = _serverTextBox.Text,
                    SyslogPort = _portTextBox.Text,
                    Protocol = protocol,
                    RunOnStartup = _runOnStartup.Checked
                });
            }

            StartMonitoring();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            _testButton.Enabled = false;
            _testButton.Text = "...";

            _syslogService.Server = _serverTextBox.Text;
            if (int.TryParse(_portTextBox.Text, out int port))
                _syslogService.Port = port;

            bool connected = await _syslogService.TestConnectionAsync();

            _testButton.Enabled = true;
            _testButton.Text = "Test";

            MessageBox.Show(
                this,
                connected ? "Connection successful!" : "Connection failed.",
                "Connection Test",
                MessageBoxButtons.OK,
                connected ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void ShowConfiguration()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void ExitApplication()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            _eventMonitor.Dispose();
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                return;
            }
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _notifyIcon?.Dispose();
                _contextMenu?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    internal static class StringExtensionHelpers
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
