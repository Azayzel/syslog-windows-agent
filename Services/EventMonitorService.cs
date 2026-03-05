using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace Jakkl.Services
{
    public class EventMonitorService : IDisposable
    {
        private readonly ILogger<EventMonitorService> _logger;
        private readonly SyslogService _syslogService;
        private readonly List<EventLog> _activeWatchers = new List<EventLog>();

        public event Action<EventLogEntry> EventReceived;

        public EventMonitorService(ILogger<EventMonitorService> logger, SyslogService syslogService)
        {
            _logger = logger;
            _syslogService = syslogService;
        }

        public void StartWatching(IEnumerable<string> logNames, IEnumerable<string> eventTypeFilters)
        {
            StopWatching();

            var filters = new HashSet<string>(eventTypeFilters ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);
            bool filterAll = filters.Count == 0 || filters.Contains("All Event Types");

            foreach (var logName in logNames)
            {
                if (string.IsNullOrWhiteSpace(logName))
                    continue;

                try
                {
                    var eventLog = new EventLog(logName);
                    eventLog.EntryWritten += (sender, e) => OnEntryWritten(e.Entry, filters, filterAll);
                    eventLog.EnableRaisingEvents = true;
                    _activeWatchers.Add(eventLog);
                    _logger.LogInformation("Started watching event log: {LogName}", logName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to start watching event log: {LogName}", logName);
                }
            }
        }

        public void StopWatching()
        {
            foreach (var watcher in _activeWatchers)
            {
                try
                {
                    watcher.EnableRaisingEvents = false;
                    watcher.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error stopping event log watcher");
                }
            }
            _activeWatchers.Clear();
        }

        private void OnEntryWritten(EventLogEntry entry, HashSet<string> filters, bool filterAll)
        {
            var entryType = entry.EntryType.ToString();

            if (!filterAll && !filters.Contains(entryType))
                return;

            _logger.LogDebug("Event received: {EntryType} from {Source}", entryType, entry.Source);

            EventReceived?.Invoke(entry);

            var message = $"[{entry.EntryType}] {entry.Source}: {entry.Message}";
            _ = _syslogService.SendAsync(message);

            LogToAlertFile(entry);
        }

        private void LogToAlertFile(EventLogEntry entry)
        {
            try
            {
                const string filename = "alerts.xml";

                if (!System.IO.File.Exists(filename))
                {
                    using (var writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8))
                    {
                        writer.Formatting = Formatting.Indented;
                        writer.WriteStartDocument();
                        writer.WriteStartElement("alerts");
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                }

                var doc = new XmlDocument();
                doc.Load(filename);

                var elem = doc.CreateElement("alert");
                elem.SetAttribute("time", entry.TimeGenerated.ToShortTimeString());
                elem.SetAttribute("type", entry.EntryType.ToString());
                elem.SetAttribute("category", entry.Category);
                elem.SetAttribute("source", entry.Source);
                elem.SetAttribute("eventid", entry.InstanceId.ToString());
                elem.SetAttribute("user", entry.UserName);
                elem.InnerText = entry.Message;

                doc.DocumentElement.AppendChild(elem);
                doc.Save(filename);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to write to alerts.xml");
            }
        }

        public static List<string> GetAvailableEventLogs()
        {
            var logs = new List<string>();
            foreach (var log in EventLog.GetEventLogs())
            {
                try
                {
                    logs.Add(log.LogDisplayName);
                }
                catch
                {
                    // Some logs require elevated permissions for display name
                    try { logs.Add(log.Log); } catch { }
                }
            }
            logs.Sort();
            return logs;
        }

        public void Dispose()
        {
            StopWatching();
        }
    }
}
