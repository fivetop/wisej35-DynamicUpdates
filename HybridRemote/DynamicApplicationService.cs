using System;
using System.IO;
using System.Linq;

namespace HybridRemote
{
	public class DynamicApplicationService
	{
		private static readonly string DIST_DIRECTORY;

		static DynamicApplicationService()
		{
			DIST_DIRECTORY = Path.Combine(Directory.GetCurrentDirectory(), "Dist");
		}

		public static string Update(string versionString, string framework)
		{
			var version = new Version(versionString);
			var hasUpdate = version < GetVersion();

			return new
			{
				hasUpdate,
				files = hasUpdate? GetFiles(framework) : null,

				// TODO: Determine startup type fully qualified namespace.
				startup = "HybridDynamic.Page1, HybridDynamic",
			}.ToJSON();
		}

		private static Version GetVersion()
		{
			// TODO: determine version check.
			return typeof(DynamicApplicationService).Assembly.GetName().Version;
		}

		public static byte[] GetFile(string framework, string fileName)
		{
			return File.ReadAllBytes(Path.Combine(DIST_DIRECTORY, framework, fileName));
		}

		public static string[] GetFiles(string framework)
		{
			var files = Directory.GetFiles(Path.Combine(DIST_DIRECTORY, framework));

			return files.Select(Path.GetFileName).ToArray();
		}
	}
}
