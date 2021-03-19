using System;
using System.IO;
using System.Threading.Tasks;

using CsBot.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace CsBot
{
	class Program
    {
        static async Task Main(string[] args)
        {
	        var builder = new ConfigurationBuilder ();
			BuildConfig (builder);

			Log.Logger = new LoggerConfiguration ()
				.ReadFrom.Configuration (builder.Build ())
				.Enrich.FromLogContext ()
				.WriteTo.Console ()
				.CreateLogger ();

			Log.Logger.Information ("Application Starting");

			var host = Host.CreateDefaultBuilder ()
				.ConfigureServices ((context, services) => {
					services.AddTransient<IIrcBotService, IrcBotService> ();
				})
				.UseSerilog ()
				.Build ();

			var svc = ActivatorUtilities.CreateInstance<IrcBotService> (host.Services);
			await svc.Run ();
        }

        static void BuildConfig (IConfigurationBuilder builder)
        {
	        builder.SetBasePath (Directory.GetCurrentDirectory ())
		        .AddJsonFile (Constants.SettingsFile, optional: false, reloadOnChange: true)
		        .AddJsonFile (
			        $"settings.{Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
		        .AddEnvironmentVariables ();
        }
    }
}
