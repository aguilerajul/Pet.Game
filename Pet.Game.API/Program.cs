using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
namespace Pet.Game.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = Infrastructure.Logging.SerilogHelper.GetNewLoggerToFileConfiguration("ApiTrace");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
