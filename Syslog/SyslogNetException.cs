using System;

namespace Jakkl.Syslog
{
	public class SyslogNetException : Exception
	{
		public SyslogNetException()
		{
		}

		public SyslogNetException(string message) : base(message)
		{
		}

		public SyslogNetException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
