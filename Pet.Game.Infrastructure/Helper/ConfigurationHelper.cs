using Microsoft.Extensions.Configuration;
using System.IO;

namespace Pet.Game.Infrastructure.Helper
{
    public static class ConfigurationHelper
    {
        public static IConfigurationSection GetSection(string name)
        {
            var configurationBuilder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");

            var configurationRoot = configurationBuilder
                                    .Build();

            return configurationRoot
                    .GetSection(name);
        }
    }
}
