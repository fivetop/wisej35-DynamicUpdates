using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using Wisej.Core;

namespace HybridRemote
{
	/// <summary>
	/// The Startup class configures services and the app's request pipeline.
	/// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
	/// </summary>
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }

		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(new WebApplicationOptions
			{
				Args = args,
				WebRootPath = "./"
			});

			var app = builder.Build();
			app.UseWisej();
			app.UseFileServer();
			app.MapGet("/fetch", async context =>
			{
				try
				{
					var action = context.Request.Query["action"];
					var args = context.Request.Query["args"].ToString().Split(',');

					switch (action)
					{
						case "update":
							await context.Response.WriteAsync(DynamicApplicationService.Update(args[0], args[1]));
							break;

						case "file":
							await context.Response.Body.WriteAsync(DynamicApplicationService.GetFile(args[0], args[1]));
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			});
			app.Run();
		}
	}
}
