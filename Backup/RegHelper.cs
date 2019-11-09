using System;
using Microsoft.Win32;

namespace EventReader
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
				key.SetValue("EventMonitor", path);
			else
				key.DeleteValue("EventMonitor", false);
			
			key.Close();
		}

		private void SetKey()
		{
			RegistryKey key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Event Log Monitor");
			key.SetValue("WatchLog", "Application");
			key.SetValue("Filter", "");
			key.Close();
		}

		/// <summary>
		/// Save registry key
		/// </summary>
		/// <param name="logName">Event log name</param>
		public static void SetKey(string logName, string filter)
		{
			RegistryKey key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Event Log Monitor");
			key.SetValue("WatchLog", logName);
			key.SetValue("Filter", filter);
			key.Close();
		}

		private void GetKey()
		{
			string log = "Application";
			string filter = "";
			RegistryKey key = null;

			try
			{
				key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Event Log Monitor");
				log = key.GetValue("WatchLog").ToString();
				filter = key.GetValue("Filter").ToString();

				if (log.Length == 0 || filter.Length == 0)
				{
					SetKey(log, filter);
				}
			}
			catch (NullReferenceException)
			{
				SetKey(log, filter);
			}
			finally
			{
				key.Close();
			}
			
			watchLog = log;
			eventFilter = filter;
			
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

		private string watchLog = "";
		private string eventFilter = "";
	}
}
