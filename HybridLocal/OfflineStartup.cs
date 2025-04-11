using EmbedIO;
using System.Threading;
using Wisej.EmbedIO;

namespace HybridLocal
{
	public class OfflineStartup
	{
		public static string Main(CancellationToken token, object[] args)
		{
			var url = "http://localhost:5001";
			var server = new WebServer(url);
			server.WithWisej();
			server.Run(token);

			return url;
		}
	}
}