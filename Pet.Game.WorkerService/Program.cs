using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pet.Game.Domain.Interfaces;
using Pet.Game.Repository.Implementations;
using Serilog;

namespace Pet.Game.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = Infrastructure.Logging.SerilogHelper.GetNewLoggerToFileConfiguration("ApiTrace");

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
                    AddRepositories(services);

                    services.AddHostedService<Worker>();
                    services.AddMemoryCache();
                })
                .UseSerilog();

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPetRepository, PetRepository>();
        }
    }
}
