using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jakl.Syslog.Transport
{
	class SyslogTransportException : Exception
	{
		public SyslogTransportException(string message) : base(message)
		{

		}
	}
}
