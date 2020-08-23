using Pet.Game.Infrastructure.Helper;

namespace Pet.Game.Repository.Base
{
    public abstract class RepositoryBase
    {
        protected readonly PetGameDataContext DbContext;

        public RepositoryBase()
        {
            var connectionString = ConfigurationHelper.GetSection("ConnectionStrings:PetsDbContext").Value;
            DbContext = new PetGameDataContext(connectionString, true);
        }
    }
}
