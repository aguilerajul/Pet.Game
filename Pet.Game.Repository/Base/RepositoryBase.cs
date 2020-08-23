using Microsoft.Extensions.Configuration;

namespace Pet.Game.Repository.Base
{
    public abstract class RepositoryBase
    {
        public readonly IConfiguration configuration;

        protected readonly PetGameDataContext DbContext;

        public RepositoryBase(IConfiguration configuration)
        {            
            DbContext = new PetGameDataContext(configuration, true);
            this.configuration = configuration;
        }
    }
}
