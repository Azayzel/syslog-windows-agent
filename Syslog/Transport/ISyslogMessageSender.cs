using System;
using System.Collections.Generic;
using Jakl.Syslog.Serialization;

namespace Jakl.Syslog.Transport
{
	public interface ISyslogMessageSender : IDisposable
	{
		void Reconnect();
		void Send(SyslogMessage message, ISyslogMessageSerializer serializer);
		void Send(IEnumerable<SyslogMessage> messages, ISyslogMessageSerializer serializer);
	}
}