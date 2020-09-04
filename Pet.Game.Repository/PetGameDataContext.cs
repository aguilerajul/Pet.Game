using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pet.Game.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Pet.Game.Infrastructure.Helper;
using Pet.Game.Repository.EntityConfigurations;
using Pet.Game.Repository.Extensions;

namespace Pet.Game.Repository
{
    public class PetGameDataContext : DbContext
    {
        public readonly IConfiguration configuration;
        private readonly bool useConsoleLogger;

        public PetGameDataContext()
        {

        }

        public PetGameDataContext(IConfiguration configuration, bool useConsoleLogger)
        {
            this.configuration = configuration;
            this.useConsoleLogger = useConsoleLogger;
        }

        public DbSet<Domain.Entities.Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information
                        )
                        .AddConsole()
                        .AddDebug();
                });

                var cnnPath = "ConnectionStrings:PetGameDataBase";
                var connectionString = this.configuration == null ? ConfigurationHelper.GetSection(cnnPath).Value : this.configuration.GetSection(cnnPath).Value;
                optionsBuilder
                    .UseSqlServer(connectionString)
                    .UseLazyLoadingProxies();

                if (useConsoleLogger)
                {
                    optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PetTyeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PetEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new UserPetsEntityConfiguration());

            ModelBuilderExtensions.Seed(modelBuilder);
        }

    }
}
