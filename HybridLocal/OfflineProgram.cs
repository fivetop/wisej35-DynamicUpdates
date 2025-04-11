using Wisej.Web;

namespace HybridLocal
{
	internal static class Program
	{
		// Remote app endpoint. This example is for DevTunnels.
		private const string DYNAMIC_APP_ENDPOINT = "https://1bhgwbx5-5000.use.devtunnels.ms/";

		static Program()
		{
			Application.Services.AddService<DynamicApplicationService>(new DynamicApplicationService(DYNAMIC_APP_ENDPOINT));
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			Application.MainPage = new MainPage();
		}
	}
}