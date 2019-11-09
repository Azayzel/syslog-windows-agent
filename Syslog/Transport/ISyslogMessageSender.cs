using System;
using System.Collections.Generic;
using Jakkl.Syslog.Serialization;

namespace Jakkl.Syslog.Transport
{
	public interface ISyslogMessageSender : IDisposable
	{
		void Reconnect();
		void Send(SyslogMessage message, ISyslogMessageSerializer serializer);
		void Send(IEnumerable<SyslogMessage> messages, ISyslogMessageSerializer serializer);
	}
}