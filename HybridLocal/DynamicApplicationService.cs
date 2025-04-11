using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wisej.Hybrid;
using Wisej.Web;

namespace HybridLocal
{
	public class DynamicApplicationService
	{
		private int _progress = 0;
		private readonly string _endpoint;
		private IEnumerable<string> _files;
		private readonly HttpClient _httpClient;
		private readonly string _applicationStoragePath;

		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicApplicationService"/> class with the specified endpoint.
		/// </summary>
		/// <param name="endpoint">The endpoint URL for the service.</param>
		public DynamicApplicationService(string endpoint)
		{
			_endpoint = endpoint;
			_httpClient = new HttpClient { BaseAddress = new Uri(_endpoint) };
			_applicationStoragePath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				typeof(DynamicApplicationService).Assembly.GetName().Name);

			Directory.CreateDirectory(_applicationStoragePath);
		}

		/// <summary>
		/// Fired when the update progress changes.
		/// </summary>
		public event EventHandler<int> ProgressChanged;

		/// <summary>
		/// Attempts to update the local application if updates are available.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the app was updated.</returns>
		public async Task<bool> UpdateAsync()
		{
			var platform = GetPlatform();
			var response = await _httpClient.GetAsync($"/fetch?action=update&args={GetLocalVersion()},{platform}");

			if (response.StatusCode != HttpStatusCode.OK)
				return false;

			var content = await response.Content.ReadAsStringAsync();
			var data = JSON.Parse(content);

			if (data == null)
				return false;

			if (data.hasUpdate)
			{
				// Try to delete existing local files.
				if (!TryDeleteDirectory(_applicationStoragePath))
					return false;

				// Create directory for new files.
				Directory.CreateDirectory(_applicationStoragePath);

				// Download and save the new files.
				if (!await DownloadFilesAsync(data.files, platform))
					return false;
			}

			// Update startup preference if specified.
			if (!string.IsNullOrEmpty(data.startup))
				Device.Preferences.Set("startup", data.startup);

			return data.hasUpdate;
		}

		/// <summary>
		/// Gets the local dynamic startup type.
		/// </summary>
		/// <returns>The local dynamic startup type as a <see cref="Page"/>.</returns>
		public Page? GetStartup()
		{
			var startup = Device.Preferences.Get("startup", string.Empty) as string;
			if (string.IsNullOrEmpty(startup))
				return null;

			var startupAssemblyName = startup.Split(',')[1].Trim();

			LoadDynamicAssemblies();

			var type = Type.GetType(startup);

			return type == null ? null : Activator.CreateInstance(type) as Page;
		}

		/// <summary>
		/// Downloads a list of files to the system asynchronously.
		/// </summary>
		/// <param name="files">The list of files to download.</param>
		/// <param name="platform">The platform identifier.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the download was successful.</returns>
		private async Task<bool> DownloadFilesAsync(IEnumerable<string> files, string platform)
		{
			this._progress = 0;
			this._files = files;

			var tasks = new List<Task>();

			foreach (var file in files)
				tasks.Add(DownloadAndSaveFileAsync(platform, file));

			try
			{
				await Task.WhenAll(tasks);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}

		/// <summary>
		/// Downloads and saves a file to the local system asynchronously.
		/// </summary>
		/// <param name="platform">The platform identifier.</param>
		/// <param name="file">The file to download.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the download was successful.</returns>
		private async Task<bool> DownloadAndSaveFileAsync(string platform, string file)
		{
			try
			{
				var path = Path.Combine(_applicationStoragePath, file);
				var bytes = await _httpClient.GetByteArrayAsync($"/fetch?action=file&args={platform},{file}");
				File.WriteAllBytes(path, bytes);

				this._progress += (int)(1.0F / this._files.Count() * 100);
				this.ProgressChanged?.Invoke(this, this._progress);

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while downloading {file}: {ex.Message}");
				return false;
			}
		}

		/// <summary>
		/// Determines the platform identifier based on the device information.
		/// </summary>
		/// <returns>A string representing the platform identifier.</returns>
		private string GetPlatform()
		{
			var version = $"net{Environment.Version.Major}.0";
			return Device.Info.System.IsApple ? $"{version}-ios" :
				   Device.Info.System.IsAndroid ? $"{version}-android" :
				   $"{version}-windows10.0.19041.0";
		}

		/// <summary>
		/// Gets the local version of the application.
		/// </summary>
		/// <returns>The local version of the application.</returns>
		public Version GetLocalVersion()
		{
			// TODO: Determine version check
			return new Version(0, 1);
		}

		/// <summary>
		/// Loads dynamic assemblies from the application storage path.
		/// </summary>
		private void LoadDynamicAssemblies()
		{
			var assemblies = Directory.GetFiles(_applicationStoragePath, "*.dll");
			foreach (var assembly in assemblies)
				Application.LoadAssembly(assembly);
		}

		/// <summary>
		/// Tries to delete the specified directory and its contents.
		/// </summary>
		/// <param name="path">The path of the directory to delete.</param>
		/// <returns>True if the directory was deleted successfully; otherwise, false.</returns>
		private bool TryDeleteDirectory(string path)
		{
			try
			{
				if (Directory.Exists(path))
					Directory.Delete(path, true);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while deleting directory {path}: {ex.Message}");
				return false;
			}
		}
	}
}
