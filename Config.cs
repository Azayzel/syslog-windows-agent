using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Xml;
using FontAwesome.Sharp;
using Jakkl;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Icon = System.Drawing.Icon;
using System.Drawing;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Jakkl
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class JakklConfig : MetroForm
	{
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuItemConfigure;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemVwr;
		private System.Windows.Forms.MenuItem menuItem2;
        private MetroFramework.Controls.MetroButton SaveButton;
        private MetroFramework.Controls.MetroButton cancelButton;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroToggle runOnStartup;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroRadioButton _saveSettings;
        
        
        private ComboBox cbFilter;
        private ComboBox cbLogs;
        private CheckBox checkBoxROS;
        private MetroFramework.Controls.MetroLabel _saveSettingsLabel;
        private MetroFramework.Controls.MetroToggle _saveSettings1;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox syslogServer;
        private MetroFramework.Controls.MetroLabel syslogPortLabel;
        private MetroFramework.Controls.MetroTextBox _syslogPort;
        private PictureBox pictureBox1;
        private MetroFramework.Controls.MetroButton testConnButton;
        private MetroTextBox logsSelectedBox;
        private ListBox logsCheckBox;
        private IconPictureBox metroButton1;
        private IconPictureBox iconPictureBox1;
        private MetroTextBox filterTextBox;
        private ListBox _eventFilter;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public JakklConfig()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			SetInitValues();			

			// start watching
			StartWatch();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JakklConfig));
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemConfigure = new System.Windows.Forms.MenuItem();
            this.menuItemVwr = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.SaveButton = new MetroFramework.Controls.MetroButton();
            this.cancelButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.runOnStartup = new MetroFramework.Controls.MetroToggle();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this._saveSettings = new MetroFramework.Controls.MetroRadioButton();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.cbLogs = new System.Windows.Forms.ComboBox();
            this.checkBoxROS = new System.Windows.Forms.CheckBox();
            this._saveSettingsLabel = new MetroFramework.Controls.MetroLabel();
            this._saveSettings1 = new MetroFramework.Controls.MetroToggle();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.syslogServer = new MetroFramework.Controls.MetroTextBox();
            this.syslogPortLabel = new MetroFramework.Controls.MetroLabel();
            this._syslogPort = new MetroFramework.Controls.MetroTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.testConnButton = new MetroFramework.Controls.MetroButton();
            this.logsSelectedBox = new MetroFramework.Controls.MetroTextBox();
            this.logsCheckBox = new System.Windows.Forms.ListBox();
            this.metroButton1 = new FontAwesome.Sharp.IconPictureBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.filterTextBox = new MetroFramework.Controls.MetroTextBox();
            this._eventFilter = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemConfigure,
            this.menuItemVwr,
            this.menuItem2,
            this.menuItemExit});
            // 
            // menuItemConfigure
            // 
            this.menuItemConfigure.DefaultItem = true;
            this.menuItemConfigure.Index = 0;
            this.menuItemConfigure.Text = "Configure...";
            this.menuItemConfigure.Click += new System.EventHandler(this.menuItemConfigure_Click);
            // 
            // menuItemVwr
            // 
            this.menuItemVwr.Index = 1;
            this.menuItemVwr.Text = "Event Viewer";
            this.menuItemVwr.Click += new System.EventHandler(this.menuItemVwr_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 3;
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(182, 350);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(82, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Apply";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(23, 350);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 61);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(115, 19);
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "Log(s) to monitor:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 126);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(128, 19);
            this.metroLabel2.TabIndex = 8;
            this.metroLabel2.Text = "Filter by event types:";
            // 
            // runOnStartup
            // 
            this.runOnStartup.AutoSize = true;
            this.runOnStartup.Checked = true;
            this.runOnStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runOnStartup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runOnStartup.Location = new System.Drawing.Point(183, 273);
            this.runOnStartup.Name = "runOnStartup";
            this.runOnStartup.Size = new System.Drawing.Size(80, 17);
            this.runOnStartup.TabIndex = 8;
            this.runOnStartup.Text = "On";
            this.runOnStartup.UseVisualStyleBackColor = true;
            this.runOnStartup.CheckedChanged += new System.EventHandler(this.MetroToggle1_CheckedChanged);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(23, 273);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(103, 19);
            this.metroLabel3.TabIndex = 11;
            this.metroLabel3.Text = "Run On Startup:";
            // 
            // _saveSettings
            // 
            this._saveSettings.Location = new System.Drawing.Point(0, 0);
            this._saveSettings.Name = "_saveSettings";
            this._saveSettings.Size = new System.Drawing.Size(104, 24);
            this._saveSettings.TabIndex = 0;
            // 
            // cbFilter
            // 
            this.cbFilter.Location = new System.Drawing.Point(0, 0);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(121, 21);
            this.cbFilter.TabIndex = 0;
            // 
            // cbLogs
            // 
            this.cbLogs.Location = new System.Drawing.Point(0, 0);
            this.cbLogs.Name = "cbLogs";
            this.cbLogs.Size = new System.Drawing.Size(121, 21);
            this.cbLogs.TabIndex = 0;
            // 
            // checkBoxROS
            // 
            this.checkBoxROS.Location = new System.Drawing.Point(0, 0);
            this.checkBoxROS.Name = "checkBoxROS";
            this.checkBoxROS.Size = new System.Drawing.Size(104, 24);
            this.checkBoxROS.TabIndex = 0;
            // 
            // _saveSettingsLabel
            // 
            this._saveSettingsLabel.AutoSize = true;
            this._saveSettingsLabel.Location = new System.Drawing.Point(23, 305);
            this._saveSettingsLabel.Name = "_saveSettingsLabel";
            this._saveSettingsLabel.Size = new System.Drawing.Size(88, 19);
            this._saveSettingsLabel.TabIndex = 12;
            this._saveSettingsLabel.Text = "Save Settings:";
            // 
            // _saveSettings1
            // 
            this._saveSettings1.AutoSize = true;
            this._saveSettings1.Checked = true;
            this._saveSettings1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._saveSettings1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveSettings1.Location = new System.Drawing.Point(183, 307);
            this._saveSettings1.Name = "_saveSettings1";
            this._saveSettings1.Size = new System.Drawing.Size(80, 17);
            this._saveSettings1.TabIndex = 13;
            this._saveSettings1.Text = "On";
            this._saveSettings1.UseVisualStyleBackColor = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(23, 195);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(92, 19);
            this.metroLabel4.TabIndex = 14;
            this.metroLabel4.Text = "Syslog Server:";
            // 
            // syslogServer
            // 
            this.syslogServer.CustomBackground = true;
            this.syslogServer.Location = new System.Drawing.Point(26, 217);
            this.syslogServer.Name = "syslogServer";
            this.syslogServer.Size = new System.Drawing.Size(125, 23);
            this.syslogServer.TabIndex = 15;
            this.syslogServer.Text = "0.0.0.0";
            this.syslogServer.Click += new System.EventHandler(this.MetroTextBox1_Click);
            // 
            // syslogPortLabel
            // 
            this.syslogPortLabel.AutoSize = true;
            this.syslogPortLabel.Location = new System.Drawing.Point(161, 195);
            this.syslogPortLabel.Name = "syslogPortLabel";
            this.syslogPortLabel.Size = new System.Drawing.Size(37, 19);
            this.syslogPortLabel.TabIndex = 16;
            this.syslogPortLabel.Text = "Port:";
            this.syslogPortLabel.Click += new System.EventHandler(this.SyslogPort_Click);
            // 
            // _syslogPort
            // 
            this._syslogPort.BackColor = System.Drawing.SystemColors.Window;
            this._syslogPort.CustomBackground = true;
            this._syslogPort.Location = new System.Drawing.Point(161, 217);
            this._syslogPort.Name = "_syslogPort";
            this._syslogPort.Size = new System.Drawing.Size(43, 23);
            this._syslogPort.TabIndex = 17;
            this._syslogPort.Text = "514";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Jakkl.Properties.Resources.Jakkl_sm;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(202, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // testConnButton
            // 
            this.testConnButton.Location = new System.Drawing.Point(210, 217);
            this.testConnButton.Name = "testConnButton";
            this.testConnButton.Size = new System.Drawing.Size(54, 23);
            this.testConnButton.TabIndex = 20;
            this.testConnButton.Text = "Test";
            this.testConnButton.Click += new System.EventHandler(this.testConnButton_Click);
            // 
            // logsSelectedBox
            // 
            this.logsSelectedBox.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.logsSelectedBox.Location = new System.Drawing.Point(26, 85);
            this.logsSelectedBox.Name = "logsSelectedBox";
            this.logsSelectedBox.Size = new System.Drawing.Size(209, 23);
            this.logsSelectedBox.TabIndex = 22;
            this.logsSelectedBox.Click += new System.EventHandler(this.metroTextBox1_Click_1);
            // 
            // logsCheckBox
            // 
            this.logsCheckBox.FormattingEnabled = true;
            this.logsCheckBox.Location = new System.Drawing.Point(26, 114);
            this.logsCheckBox.Name = "logsCheckBox";
            this.logsCheckBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.logsCheckBox.Size = new System.Drawing.Size(237, 95);
            this.logsCheckBox.Sorted = true;
            this.logsCheckBox.TabIndex = 25;
            this.logsCheckBox.Visible = false;
            this.logsCheckBox.MouseLeave += new System.EventHandler(this.Close_ListBox);
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.metroButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.metroButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.metroButton1.IconColor = System.Drawing.SystemColors.ControlText;
            this.metroButton1.IconSize = 23;
            this.metroButton1.Location = new System.Drawing.Point(241, 85);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(23, 23);
            this.metroButton1.TabIndex = 26;
            this.metroButton1.TabStop = false;
            this.metroButton1.Click += new System.EventHandler(this.ShowLogsCheckBox);
            this.metroButton1.MouseHover += new System.EventHandler(this.ShowLogsCheckBox);
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.iconPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconSize = 23;
            this.iconPictureBox1.Location = new System.Drawing.Point(241, 148);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(23, 23);
            this.iconPictureBox1.TabIndex = 28;
            this.iconPictureBox1.TabStop = false;
            this.iconPictureBox1.MouseHover += new System.EventHandler(this.ShoweventFilters);
            // 
            // filterTextBox
            // 
            this.filterTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.filterTextBox.Location = new System.Drawing.Point(28, 148);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(207, 23);
            this.filterTextBox.TabIndex = 27;
            // 
            // _eventFilter
            // 
            this._eventFilter.FormattingEnabled = true;
            this._eventFilter.Items.AddRange(new object[] {
            "All Event Types",
            "Error",
            "FailureAudit",
            "Information",
            "SuccessAudit",
            "Warning"});
            this._eventFilter.Location = new System.Drawing.Point(28, 177);
            this._eventFilter.Name = "_eventFilter";
            this._eventFilter.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this._eventFilter.Size = new System.Drawing.Size(235, 69);
            this._eventFilter.Sorted = true;
            this._eventFilter.TabIndex = 29;
            this._eventFilter.Visible = false;
            this._eventFilter.MouseLeave += new System.EventHandler(this.Close_FiltersListBox);
            // 
            // JakklConfig
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(293, 388);
            this.ContextMenu = this.contextMenu;
            this.ControlBox = false;
            this.Controls.Add(this.logsCheckBox);
            this.Controls.Add(this._eventFilter);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.logsSelectedBox);
            this.Controls.Add(this.testConnButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._syslogPort);
            this.Controls.Add(this.syslogPortLabel);
            this.Controls.Add(this.syslogServer);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this._saveSettings1);
            this.Controls.Add(this._saveSettingsLabel);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.runOnStartup);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.SaveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "JakklConfig";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Jakkl Config";
            this.Load += new System.EventHandler(this.Config_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new JakklConfig());
		}

		private void SetInitValues()
		{
			RegHelper reg = new RegHelper();
			watchLog = reg.WatchLog;
			eventFilter = reg.EventFilter;
            syslogport = reg.SyslogPort;
            syslogserver = reg.SyslogServer;

            syslogServer.Text = syslogserver;
            _syslogPort.Text = syslogport;

			appIcon = new Icon(GetType(), "events.ico");
			localPath = System.Environment.CurrentDirectory;

			this.WindowState = FormWindowState.Minimized;
			this.Hide();

            this.metroButton1.Image = IconChar.AngleDown.ToBitmap(22, Color.Black);
            this.iconPictureBox1.Image = IconChar.AngleDown.ToBitmap(22, Color.Black);

            if (!string.IsNullOrEmpty(watchLog))
            {
                this.logsSelectedBox.Text = "Not Listening to Logs. Set logs to watch.";
            }
            this.logsSelectedBox.Text = watchLog;

			// populate monitorLogs with available event logs
			ArrayList alLogs = GetAvailEventLogs();
			foreach (EventLog log in GetAvailEventLogs())
			{
                MetroFramework.Controls.MetroCheckBox checkBox = new MetroFramework.Controls.MetroCheckBox();
                checkBox.Text = log.LogDisplayName;
                logsCheckBox.Items.Add(log.LogDisplayName);
            }

            logsCheckBox.SelectedIndex = 0;

            NotifyIcon = new NotifyIconEx();
			NotifyIcon.Icon = this.appIcon;
			NotifyIcon.Text = tipText+"("+watchLog+")";
			NotifyIcon.Visible = true;
			NotifyIcon.ContextMenu = this.contextMenu;
			
			logsCheckBox.SelectedItem = watchLog;

			// add event handlers
			NotifyIcon.BalloonClick += new EventHandler(OnClickBalloon);
			NotifyIcon.DoubleClick += new EventHandler(OnDoubleClickIcon);

           
                    
        }
        

        /// <summary>
        /// Retrieves available event logs on the local machine
        /// </summary>
        /// <returns>ArrayList of available EventLog objects</returns>
        private ArrayList GetAvailEventLogs()
		{
			ArrayList alLogs = new ArrayList();
			foreach (EventLog log in EventLog.GetEventLogs())
				alLogs.Add(log);

			return alLogs;
		}

		/// <summary>
		/// Retrieves available event logs on the specified machine
		/// </summary>
		/// <param name="machine"></param>
		/// <returns>ArrayList of available EventLog objects</returns>
		private ArrayList GetAvailEventLogs(string machine)
		{
			ArrayList alLogs = new ArrayList();
			foreach (EventLog log in EventLog.GetEventLogs(machine))
				alLogs.Add(log);

			return alLogs;
		}

		private void OnClickBalloon(object sender, EventArgs e)
		{
			// start Event Viewer
			Process.Start("eventvwr.exe");
		}
		
		private void OnDoubleClickIcon(object sender, EventArgs e)
		{
			ShowConfiguration();
		}

		#region "Properties"
		private NotifyIconEx NotifyIcon
		{
			get{ return notifyIconA; }

			set{ notifyIconA = value; }
		}
		/// <summary>
		/// Tip text displayed on mouseover icon event
		/// </summary>
		private string TipText
		{
			get{ return tipText; }
		}
		/// <summary>
		/// Event log entry category
		/// </summary>
		private string LogCategory
		{
			get{ return logCategory; }
		}
		/// <summary>
		/// Event log entry message
		/// </summary>
		private string LogMessage
		{
			get{ return logMessage; }
		}
		/// <summary>
		/// The machine the log entry was generated on
		/// </summary>
		private string LogMachine
		{
			get{ return logMachine; }
		}
		/// <summary>
		/// The event log entry source
		/// </summary>
		private string LogSource
		{
			get{ return logSource; }
		}
		/// <summary>
		/// The event log entry type
		/// Can be "Success Audit" "Failure Audit" "Error" "Warning" or "Information"
		/// </summary>
		private string LogType
		{
			get{ return logType; }
		}
		/// <summary>
		/// The event log entry ID
		/// </summary>
		private string EventID
		{
			get{ return eventID; }
		}
		/// <summary>
		/// The user associated with the log entry, if any.
		/// </summary>
		private string User
		{
			get{ return user; }
		}
		/// <summary>
		/// Time the event was generated
		/// </summary>
		private string LogTime
		{
			get{ return logTime; }
		}
		#endregion

		#region "Private Variables"
		private string watchLog = "";
        private string syslogport = "";
        private string syslogserver = "";
		private string eventFilter = "";
		private string tipText = "Jakkl Event Monitoring to Syslog";
		private Icon appIcon;
		private NotifyIconEx notifyIconA;
		private string logMessage = "";
		private string logMachine = "";
		private string logSource = "";
		private string logType;
		private string eventID = "";
		private string user = "";
		private string logTime = "";
		private string logCategory = "";
		private string localPath = "";
		#endregion

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			
		}

		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			if (LogComboOK())
			{
				localPath+="\\"+Assembly.GetExecutingAssembly().GetName().Name+
					".exe";			
				
				this.Hide(); // hide from Alt-Tab

				if (eventFilter == "All Event Types")
					eventFilter = "";				

				// save settings
				if (_saveSettings1.Checked)
					RegHelper.SetKey(watchLog, eventFilter, _syslogPort.Text, syslogServer.Text);

				if (runOnStartup.Checked)
					RegHelper.SetRunOnStartup(localPath, false);
				else
					RegHelper.SetRunOnStartup(localPath, true);				
				
				logsCheckBox.SelectedItem = watchLog;

				NotifyIcon.Text = tipText+"("+watchLog+")";
				StartWatch();
			}
			else
				MessageBox.Show(this,
					"The filter is invalid for this event log type",
					"Invalid Combination",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
		}

		/// <summary>
		/// Determine if the log combination is valid.
		/// </summary>
		/// <returns>T/F</returns>
		private bool LogComboOK()
		{
			bool comboOK = false;
			eventFilter = _eventFilter.SelectedItem.ToString();
			watchLog = logsCheckBox.SelectedItem.ToString();
			switch (watchLog)
			{
				// Security can only have success or failure events
				case "Security":
					if (eventFilter == "All Event Types" || eventFilter == "SuccessAudit" ||
						eventFilter == "FailureAudit")
						comboOK = true;
					break;
				default:
					comboOK = true;
					break;
			}
			return comboOK;						
		}

		private void menuItemConfigure_Click(object sender, System.EventArgs e)
		{
			ShowConfiguration();
		}

		private void menuItemVwr_Click(object sender, System.EventArgs e)
		{
			Process.Start("eventvwr.exe");
		}
		
		/// <summary>
		/// Displays the configuration window
		/// </summary>
		private void ShowConfiguration()
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
			
			if (RegHelper.IsRunOnStartup())
				runOnStartup.Checked = true;
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			// cleanup
			NotifyIcon.Remove();
			NotifyIcon = null;
			this.Close();
		}

		private void StartWatch()
		{
            Debug.WriteLine(watchLog, eventFilter);
			EventLog myLog = new EventLog(watchLog);
            
			// set event handler
			myLog.EntryWritten += new EntryWrittenEventHandler(OnEntryWritten);
			myLog.EnableRaisingEvents = true;
		}

		/// <summary>
		/// Retrieves the stats on the last event log entry
		/// </summary>
		/// <param name="logName">Event log to open</param>
		private void GetLogEntryStats(string logName)
		{
			int e = 0;
			
			EventLog log = new EventLog(logName);
			e = log.Entries.Count - 1; // last entry

			logMessage = log.Entries[e].Message;
			logMachine = log.Entries[e].MachineName;
			logSource = log.Entries[e].Source;
			logCategory = log.Entries[e].Category;
			logType = Convert.ToString(log.Entries[e].EntryType);
			eventID = log.Entries[e].InstanceId.ToString();
			user = log.Entries[e].UserName;
			logTime = log.Entries[e].TimeGenerated.ToShortTimeString();
            
            log.Close();	// close log
		}

		private void OnEntryWritten(object source, EntryWrittenEventArgs e)
		{
			string logName = watchLog;
			GetLogEntryStats(watchLog);
			
			if (logType == eventFilter || eventFilter.Length == 0)
			{
				// show balloon
				NotifyIcon.ShowBalloon("Jakkl",
					"An event was written to the "+logName+" event log."+
					"\nType: "+LogType+
					"\nSource: "+LogSource+
					"\nCategory: "+LogCategory+
					"\nEventID: "+EventID+
					"\nUser: "+User,
					NotifyIconEx.NotifyInfoFlags.Info,
					5000);
				
                // Log locally
				LogNotification();
                // Send to Syslog
                SysLogMessage sysLog = new SysLogMessage();
                bool result = sysLog.SendLog(syslogServer.Text, _syslogPort.Text, LogMessage);
            }
		}

		private void LogNotification()
		{
			string filename = "alerts.xml";
			
			if (!System.IO.File.Exists(filename))
				WriteAlertDoc(filename);

			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			XmlElement elem = doc.CreateElement("alert");			
			elem.SetAttribute("time", LogTime);
			elem.SetAttribute("type", LogType);
			elem.SetAttribute("category", LogCategory);
			elem.SetAttribute("source", LogSource);
			elem.SetAttribute("eventid", EventID);
			elem.SetAttribute("user", User);
			elem.InnerText = LogMessage;

			doc.DocumentElement.AppendChild(elem);
			doc.Save(filename);				
		}

		private void WriteAlertDoc(string filename)
		{
			XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
			writer.Formatting = Formatting.Indented;
			writer.WriteStartDocument();
			
			writer.WriteStartElement("alerts");
			writer.WriteEndElement();
			writer.WriteEndDocument();
			
			writer.Flush();
			writer.Close();
		}

        private void Config_Load(object sender, EventArgs e)
        {

        }

        private void MetroToggle1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MetroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void SyslogPort_Click(object sender, EventArgs e)
        {

        }

        public void testConnButton_Click(object sender, EventArgs e)
        {
            SysLogMessage sysLog = new SysLogMessage();
            sysLog.TestConnect(syslogServer.Text, _syslogPort.Text);
        }

        private void metroTextBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (logsCheckBox.Visible)
            {
                this.metroButton1.Image = IconChar.AngleDown.ToBitmap(22, Color.Black);
                this.logsCheckBox.Visible = false;
            }
            else
            {
                this.metroButton1.Image = IconChar.AngleUp.ToBitmap(22, Color.Black);
                this.logsCheckBox.Visible = true;
            }
        }

        private void Close_ListBox(object sender, EventArgs e)
        {
            this.metroButton1.Image = IconChar.AngleDown.ToBitmap(22, Color.Black);
            this.logsCheckBox.Visible = false;
        }

        private void ShowLogsCheckBox(object sender, EventArgs e)
        {
            this._eventFilter.Visible = false;
            this.metroButton1.Image = IconChar.AngleUp.ToBitmap(22, Color.Black);
            this.logsCheckBox.Visible = true;
        }

        private void Close_FiltersListBox(object sender, EventArgs e)
        {
            this.iconPictureBox1.Image = IconChar.AngleDown.ToBitmap(22, Color.Black);
            this._eventFilter.Visible = false;
        }

        private void ShoweventFilters(object sender, EventArgs e)
        {
            this.logsCheckBox.Visible = false;
            this.iconPictureBox1.Image = IconChar.AngleUp.ToBitmap(22, Color.Black);
            this._eventFilter.Visible = true;
        }

    }
}
