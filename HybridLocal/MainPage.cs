using System;
using Wisej.Hybrid;
using Wisej.Hybrid.Shared.Communication;
using Wisej.Services;
using Wisej.Web;

namespace HybridLocal
{
	public partial class MainPage : Page
	{
		[Inject]
		private DynamicApplicationService DynamicApplicationService { get; set; }

		public MainPage()
		{
			InitializeComponent();

			this.DynamicApplicationService.ProgressChanged += DynamicApplicationService_ProgressChanged;
		}

		private void DynamicApplicationService_ProgressChanged(object sender, int e)
		{
			this.progressBar1.Value = e;
			this.progressBar1.Text = $"Checking for updates ({e}%)";

			Application.Update(this);
		}

		private async void Page1_Load(object sender, EventArgs e)
		{
			// show the local version string.
			this.labelVersion.Text = $"Current Version: {this.DynamicApplicationService.GetLocalVersion()}";

			// check for updates if the device has an internet connection...
			if (Device.Info.Networking.NetworkAccess == NetworkAccess.Internet)
			{
				var updated = await this.DynamicApplicationService.UpdateAsync();

				// TODO: some logic to show the user the app has been updated...
			}
	
			// load the page from the dynamic app saved on the system.
			var page = this.DynamicApplicationService.GetStartup();
			if (page == null)
			{
				// a page couldn't be found. i.e. if the user tried to open the app for the first time without an internet connection.
				AlertBox.Show($"No local page available", autoCloseDelay: 0);
			}
			else
			{
				// if the dynamic page could be found, show it.
				Application.MainPage = page;
			}

			// restore the app context from the async operation.
			Application.Update(this);
		}
	}
}
