using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Xml;
using JCMLib;

namespace EventReader
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Config : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuItemConfigure;
		private System.Windows.Forms.MenuItem menuItemExit;

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxMem;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.MenuItem menuItemVwr;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbFilter;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbLogs;
		private System.Windows.Forms.CheckBox checkBoxROS;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Config()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Config));
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItemConfigure = new System.Windows.Forms.MenuItem();
			this.menuItemVwr = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxROS = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbLogs = new System.Windows.Forms.ComboBox();
			this.cbFilter = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBoxMem = new System.Windows.Forms.CheckBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBoxROS);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cbLogs);
			this.groupBox1.Controls.Add(this.cbFilter);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.checkBoxMem);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(248, 144);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			// 
			// checkBoxROS
			// 
			this.checkBoxROS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxROS.Location = new System.Drawing.Point(112, 120);
			this.checkBoxROS.Name = "checkBoxROS";
			this.checkBoxROS.Size = new System.Drawing.Size(120, 16);
			this.checkBoxROS.TabIndex = 8;
			this.checkBoxROS.Text = "Run On Startup";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Select log to monitor:";
			// 
			// cbLogs
			// 
			this.cbLogs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLogs.Location = new System.Drawing.Point(16, 40);
			this.cbLogs.Name = "cbLogs";
			this.cbLogs.Size = new System.Drawing.Size(216, 21);
			this.cbLogs.TabIndex = 6;
			// 
			// cbFilter
			// 
			this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFilter.Items.AddRange(new object[] {
														  "All Event Types",
														  "Information",
														  "Warning",
														  "Error",
														  "SuccessAudit",
														  "FailureAudit"});
			this.cbFilter.Location = new System.Drawing.Point(16, 88);
			this.cbFilter.Name = "cbFilter";
			this.cbFilter.Size = new System.Drawing.Size(216, 21);
			this.cbFilter.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Filter by these event types:";
			// 
			// checkBoxMem
			// 
			this.checkBoxMem.Checked = true;
			this.checkBoxMem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxMem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxMem.Location = new System.Drawing.Point(16, 120);
			this.checkBoxMem.Name = "checkBoxMem";
			this.checkBoxMem.Size = new System.Drawing.Size(96, 16);
			this.checkBoxMem.TabIndex = 2;
			this.checkBoxMem.Text = "Save Settings";
			// 
			// btnUpdate
			// 
			this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUpdate.Location = new System.Drawing.Point(200, 160);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(56, 23);
			this.btnUpdate.TabIndex = 2;
			this.btnUpdate.Text = "Apply";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Location = new System.Drawing.Point(136, 160);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(56, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// Config
			// 
			this.AcceptButton = this.btnUpdate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(266, 191);
			this.ContextMenu = this.contextMenu;
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Config";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Configuration";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Config());
		}

		private void SetInitValues()
		{
			RegHelper reg = new RegHelper();
			watchLog = reg.WatchLog;
			eventFilter = reg.EventFilter;
			appIcon = new Icon(GetType(), "events.ico");
			localPath = System.Environment.CurrentDirectory;

			this.WindowState = FormWindowState.Minimized;
			this.Hide();

			// populate cbLogs with available event logs
			ArrayList alLogs = GetAvailEventLogs();
			foreach (EventLog log in GetAvailEventLogs())
			{
				cbLogs.Items.Add(log.LogDisplayName);
			}
		
			NotifyIcon = new NotifyIconEx();
			NotifyIcon.Icon = this.appIcon;
			NotifyIcon.Text = tipText+"("+watchLog+")";
			NotifyIcon.Visible = true;
			NotifyIcon.ContextMenu = this.contextMenu;
			
			cbLogs.SelectedItem = watchLog;
			if (eventFilter.Length > 0)
				cbFilter.SelectedItem = eventFilter;
			else
				cbFilter.SelectedIndex = 0;

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
		private string eventFilter = "";
		private string tipText = "Event Log Watcher";
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

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			cbLogs.SelectedItem = watchLog;
			if (eventFilter.Length > 0)
				cbFilter.SelectedItem = eventFilter;
			else
				cbFilter.SelectedIndex = 0;
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			if (LogComboOK())
			{
				localPath+="\\"+Assembly.GetExecutingAssembly().GetName().Name+
					".exe";			
				
				this.Hide(); // hide from Alt-Tab

				if (eventFilter == "All Event Types")
					eventFilter = "";				

				// save settings
				if (checkBoxMem.Checked)
					RegHelper.SetKey(watchLog, eventFilter);

				if (checkBoxROS.Checked)
					RegHelper.SetRunOnStartup(localPath, false);
				else
					RegHelper.SetRunOnStartup(localPath, true);				
				
				cbLogs.SelectedItem = watchLog;

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
			eventFilter = cbFilter.SelectedItem.ToString();
			watchLog = cbLogs.SelectedItem.ToString();
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
				checkBoxROS.Checked = true;
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
			eventID = log.Entries[e].EventID.ToString();
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
				NotifyIcon.ShowBalloon("Event Log Monitor",
					"An event was written to the "+logName+" event log."+
					"\nType: "+LogType+
					"\nSource: "+LogSource+
					"\nCategory: "+LogCategory+
					"\nEventID: "+EventID+
					"\nUser: "+User,
					NotifyIconEx.NotifyInfoFlags.Info,
					5000);
				
				LogNotification();
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
	}
}
