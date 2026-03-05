using System;
using System.Net.Security;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Jakkl.Syslog.Transport
{
	public class SyslogEncryptedTcpSender : SyslogTcpSender
	{
		protected int IOTimeout;
		public bool IgnoreTLSChainErrors { get; private set; }

		protected MessageTransfer _messageTransfer;
		public override MessageTransfer messageTransfer
		{
			get { return _messageTransfer; }
			set
			{
				if (!value.Equals(MessageTransfer.OctetCounting) && transportStream is SslStream)
					throw new SyslogTransportException("Non-Transparent-Framing cannot be used with TLS transport");

				_messageTransfer = value;
			}
		}

		public SyslogEncryptedTcpSender(string hostname, int port, int timeout = Timeout.Infinite, bool ignoreChainErrors = false) : base(hostname, port)
		{
			IOTimeout = timeout;
			IgnoreTLSChainErrors = ignoreChainErrors;
			StartTls();
		}

		public override void Reconnect()
		{
			base.Reconnect();
			StartTls();
		}

		private void StartTls()
		{
			transportStream = new SslStream(tcpClient.GetStream(), false, ValidateServerCertificate)
			{
				ReadTimeout = IOTimeout,
				WriteTimeout = IOTimeout
			};

			// Use system default TLS version (allows TLS 1.2 and 1.3)
			((SslStream)transportStream).AuthenticateAsClient(
				hostname,
				null,
				SslProtocols.None, // Let the OS negotiate the best protocol
				false);

			if (!((SslStream)transportStream).IsEncrypted)
				throw new SecurityException("Could not establish an encrypted connection");

			messageTransfer = MessageTransfer.OctetCounting;
		}

		private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
				return true;

			if (IgnoreTLSChainErrors && sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors)
				return true;

			CertificateErrorHandler?.Invoke($"Certificate error: {sslPolicyErrors}");
			return false;
		}

		public static Action<string> CertificateErrorHandler { get; set; }
	}
}
