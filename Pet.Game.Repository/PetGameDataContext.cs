using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pet.Game.Domain.Entities;

namespace Pet.Game.Repository
{
    public class PetGameDataContext : DbContext
    {
        private readonly string connectionString;
        private readonly bool useConsoleLogger;

        public PetGameDataContext()
        {

        }

        public PetGameDataContext(string connectionString, bool useConsoleLogger)
        {
            this.connectionString = connectionString;
            this.useConsoleLogger = useConsoleLogger;
        }

        public DbSet<Domain.Entities.Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
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

                optionsBuilder
                    .UseSqlServer(this.connectionString)
                    .UseLazyLoadingProxies();

                if(useConsoleLogger)
                {
                    optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string idPropertyName = "Id";

            modelBuilder.Entity<User>().HasKey(idPropertyName);
            modelBuilder.Entity<Domain.Entities.Pet>().HasKey(idPropertyName);
            modelBuilder.Entity<PetType>().HasKey(idPropertyName);
        }

    }
}
