using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pet.Game.Domain.Interfaces;
using Pet.Game.Repository.Implementations;
using Serilog;
using Serilog.Events;
using System;

namespace Pet.Game.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.File(System.IO.Path.Combine(Environment.CurrentDirectory, @"Logs\WorkServiceLog.log"))
                    .CreateLogger();

            try
            {
                Log.Information("Service Starte it");
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "There was and Error trying to start the service");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddHostedService<Worker>();
                    services.AddMemoryCache();
                })
                .UseSerilog();
    }
}
