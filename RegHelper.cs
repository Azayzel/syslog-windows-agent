using System;
using Microsoft.Win32;

namespace Jakkl
{
	/// <summary>
	/// Summary description for RegHelper.
	/// </summary>
	public class RegHelper
	{

		public RegHelper()
		{
			GetKey();
		}

		/// <summary>
		/// Check run on startup status
		/// </summary>
		/// <returns></returns>
		public static bool IsRunOnStartup()
		{
			bool isStartup = false;
			RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
			
			try
			{
				if (key.GetValue("EventMonitor") != null)
					isStartup = true;
			}
			catch (NullReferenceException)
			{
			}

			key.Close();
			return isStartup;
		}

		/// <summary>
		/// Set or remove run on startup
		/// </summary>
		/// <param name="path"></param>
		/// <param name="remove"></param>
		public static void SetRunOnStartup(string path, bool remove)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if (!remove)
				key.SetValue("Jakkl", path);
			else
				key.DeleteValue("Jakkl", false);
			
			key.Close();
		}

		private void SetKey()
		{
			RegistryKey key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Jakkl");
			key.SetValue("WatchLog", "Application");
			key.SetValue("Filter", "");
            key.SetValue("SyslogServer", "");
            key.SetValue("SyslogPort", "");
			key.Close();
		}

		/// <summary>
		/// Save registry key
		/// </summary>
		/// <param name="logName">Event log name</param>
		public static void SetKey(string logName, string filter, string port, string syslogServer)
		{
			RegistryKey key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Jakkl");
			key.SetValue("WatchLog", logName);
			key.SetValue("Filter", filter);
            key.SetValue("SyslogPort", port);
            key.SetValue("SyslogServer", syslogServer);
			key.Close();
		}

		private void GetKey()
		{
			string log = "Application";
			string filter = "";
            string _port = "";
            string _syslogServer = "";
			RegistryKey key = null;

			try
			{
				key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Jakkl");
				log = key.GetValue("WatchLog").ToString();
				filter = key.GetValue("Filter").ToString();
                _port = key.GetValue("SyslogPort").ToString();
                _syslogServer = key.GetValue("SyslogServer").ToString();
				if (log.Length == 0 || filter.Length == 0)
				{
					SetKey(log, filter, _port, _syslogServer);
				}
			}
			catch (NullReferenceException)
			{
				SetKey(log, filter, _port, _syslogServer);
			}
			finally
			{
				key.Close();
			}
			
			watchLog = log;
			eventFilter = filter;
            syslogPort = _port;
            syslogServer = _syslogServer;
		}

		/// <summary>
		/// The name of the event log to monitor.
		/// </summary>
		public string WatchLog
		{
			set{ watchLog = value; }
			get{ return watchLog; }
		}
		/// <summary>
		/// The event filter
		/// </summary>
		public string EventFilter
		{
			set{ eventFilter = value; }
			get{ return eventFilter; }
		}

        /// <summary>
        /// The Syslog Port
        /// </summary>
        public string SyslogPort
        {
            set { syslogPort = value; }
            get { return syslogPort; }
        }

        /// <summary>
        /// The Syslog Server
        /// This can be either IP or hostname
        /// </summary>
        public string SyslogServer
        {
            set { syslogServer = value; }
            get { return syslogServer; }
        }

		private string watchLog = "";
		private string eventFilter = "";
        private string syslogPort = "";
        private string syslogServer = "";
	}
}
