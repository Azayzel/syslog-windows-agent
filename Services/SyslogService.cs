using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Jakkl.Syslog;
using Jakkl.Syslog.Serialization;
using Jakkl.Syslog.Transport;
using Microsoft.Extensions.Logging;

namespace Jakkl.Services
{
    public enum SyslogProtocol
    {
        Tcp,
        TcpWithTls,
        Udp
    }

    public class SyslogService : IDisposable
    {
        private readonly ILogger<SyslogService> _logger;

        public string Server { get; set; } = "";
        public int Port { get; set; } = 514;
        public SyslogProtocol Protocol { get; set; } = SyslogProtocol.TcpWithTls;
        public Facility Facility { get; set; } = Facility.LogAudit;
        public Severity DefaultSeverity { get; set; } = Severity.Informational;

        public SyslogService(ILogger<SyslogService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(Server))
            {
                _logger.LogWarning("Syslog server not configured, skipping send");
                return false;
            }

            return await Task.Run(() =>
            {
                try
                {
                    var serializer = new SyslogRfc5424MessageSerializer();
                    var syslogMessage = new SyslogMessage(
                        DateTimeOffset.Now,
                        Facility,
                        DefaultSeverity,
                        Environment.MachineName,
                        "Jakkl",
                        System.Diagnostics.Process.GetCurrentProcess().Id.ToString(),
                        null,
                        message);

                    using (var sender = CreateSender())
                    {
                        sender.Send(syslogMessage, serializer);
                    }

                    _logger.LogDebug("Syslog message sent to {Server}:{Port}", Server, Port);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send syslog message to {Server}:{Port}", Server, Port);
                    return false;
                }
            });
        }

        public async Task<bool> TestConnectionAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var client = new TcpClient(Server, Port))
                    {
                        return client.Connected;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Connection test failed for {Server}:{Port}", Server, Port);
                    return false;
                }
            });
        }

        private ISyslogMessageSender CreateSender()
        {
            return Protocol switch
            {
                SyslogProtocol.Tcp => new SyslogTcpSender(Server, Port),
                SyslogProtocol.TcpWithTls => new SyslogEncryptedTcpSender(Server, Port),
                SyslogProtocol.Udp => new SyslogUdpSender(Server, Port),
                _ => throw new ArgumentOutOfRangeException(nameof(Protocol))
            };
        }

        public void Dispose()
        {
            // No persistent connections to dispose
        }
    }
}
