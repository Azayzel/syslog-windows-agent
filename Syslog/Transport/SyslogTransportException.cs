using System;

namespace Jakkl.Syslog.Transport
{
	public class SyslogTransportException : Exception
	{
		public SyslogTransportException(string message) : base(message)
		{
		}
	}
}
