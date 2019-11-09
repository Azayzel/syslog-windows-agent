using Jakkl.Syslog;
using Jakkl.Syslog.Serialization;
using Jakkl.Syslog.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Jakkl
{
    internal class Options
    {
        // The host name. If not set, defaults to the NetBIOS name of the local machine
        public string LocalHostName { get; set; } = Environment.MachineName;

        // The application name
        public string AppName { get; set; }

        // The process identifier
        public string ProcId { get; set; }

        // The message type (called msgId in spec)
        public string MsgType { get; set; }

        // The message
        public string Message { get; set; }

        // Host name of the syslog server
        public string SyslogServerHostname { get; set; }

        // The syslog server port
        public int SyslogServerPort { get; set; } = 514;

        // The version of syslog protocol to use. Possible values are '3164' and '5424' (from corresponding RFC documents) or 'local' to send messages to a local syslog (only on Linux or OS X). Default is '5424'
        public string SyslogVersion { get; set; } = "5424";

        // The network protocol to use. Possible values are 'tcp' or 'udp' to send to a remote syslog server, or 'local' to send to a local syslog over Unix sockets (only on Linux or OS X). Default is 'tcp'. Note: TCP always uses SSL connection.
        public string NetworkProtocol { get; set; } = "tcp";

        // Optional path to a CA certificate used to verify Syslog server certificate when using TCP protocol
        public string CACertPath { get; set; }
    }

    public class SysLogMessage
    {
        public static string SyslogServer { get; set; }
        public static int Port { get; set; }
        public static string Host { get; set; }
        public static Int32 MaxLength = 2048;
        public static EventLogEntry Eventlog { get; set;}

        public enum Syslog_Facility
        {
            kern,
            user,
            mail,
            daemon,
            auth,
            syslog,
            lpr,
            news,
            uucp,
            clock,
            authpriv,
            ftp,
            ntp,
            logaudit,
            logalert,
            cron,
            local0,
            local1,
            local2,
            local3,
            local4,
            local5,
            local6,
            local7,
        }

        public enum Syslog_Severity
        {
            Emergency,
            Alert,
            Critical,
            Error,
            Warning,
            Notice,
            Informational,
            Debug
        }

        public enum Syslog_Protocol
        {
            UDP,
            TCP,
            TCPwithTLS
        }


        /// <summary>
        /// Send event to Syslog
        /// </summary>
        /// <returns></returns>
        public bool SendLog(string _server,string _port, string msg)
        {
            Options options = new Options();
            options.SyslogServerHostname = _server;
            options.SyslogServerPort = Convert.ToInt32(_port);
            options.Message = msg;
            options.NetworkProtocol = "tcp";
            
            
            try
            {
                ISyslogMessageSerializer serializer = (ISyslogMessageSerializer)new SyslogRfc5424MessageSerializer();

                ISyslogMessageSender sender = (ISyslogMessageSender)new SyslogTcpSender(options.SyslogServerHostname, options.SyslogServerPort, true);
                
                SyslogMessage msg1 = CreateSyslogMessage(options);
                File.AppendAllText("C:\\Jakkl_log.txt", msg1.Facility.ToString() + msg1.Severity.ToString());
                sender.Send(msg1, serializer);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
                File.AppendAllText("C:\\Jakkl_log.txt", e.Message);
                return false;
            }
        }

        public bool TestConnect(string _server, string _port)
        {
            try
            {
                TcpClient client = new TcpClient(_server, Convert.ToInt32(_port));
                if (client.Connected)
                    return true;
                return false;
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
                return false;
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
                return false;
            }
        }

        private static SyslogMessage CreateSyslogMessage(Options options)
        {
            return new SyslogMessage(
                DateTimeOffset.Now,
                Facility.LogAlert,
                Severity.Informational,
                options.LocalHostName ?? Environment.MachineName,
                options.AppName,
                options.ProcId ?? "3104",
                options.MsgType ?? "ID47",
                options.Message ?? "Test message at " + DateTime.Now);
        }
    }
}
