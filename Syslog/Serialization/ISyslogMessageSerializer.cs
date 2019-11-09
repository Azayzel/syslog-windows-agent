using System.IO;

namespace Jakl.Syslog.Serialization
{
	public interface ISyslogMessageSerializer
	{
		void Serialize(SyslogMessage message, Stream stream);
	}
}