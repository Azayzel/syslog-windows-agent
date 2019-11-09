using System.IO;

namespace Jakkl.Syslog.Serialization
{
	public interface ISyslogMessageSerializer
	{
		void Serialize(SyslogMessage message, Stream stream);
	}
}