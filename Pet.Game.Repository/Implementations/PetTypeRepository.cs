using Microsoft.EntityFrameworkCore;
using Pet.Game.Domain.Interfaces;
using Pet.Game.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pet.Game.Domain.Entities;

namespace Pet.Game.Repository.Implementations
{
    public class PetTypeRepository : RepositoryBase, IPetTypeRepository
    {
        public PetTypeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Domain.Entities.PetType> AddOrUpdateAsync(Domain.Entities.PetType petType)
        {
            var nameDuplicated = await GetByNameAsync(petType.Name);
            if (nameDuplicated != null)
                throw new Exception($"The Pet Type with name: {petType.Name} already exists");

            var petTypeById = await GetAsync(petType.Id);
            if (petTypeById != null)
            {
                petTypeById.SetName(petType.Name);
                petTypeById.HappinessInterval = petType.HappinessInterval;
                petTypeById.HungrinessInterval = petType.HungrinessInterval;
                await this.DbContext.SaveChangesAsync();
                return petTypeById;
            }
            else
            {
                var newPetType = new PetType(petType.Name, petType.HappinessInterval, petType.HungrinessInterval);
                this.DbContext.PetTypes.Attach(petType);
                await this.DbContext.SaveChangesAsync();

                return newPetType;
            }
        }

        public async Task<Domain.Entities.PetType> GetAsync(Guid id)
        {
            return await this.DbContext.PetTypes.FindAsync(id);
        }

        public async Task<Domain.Entities.PetType> GetByNameAsync(string name)
        {
            return await this.DbContext.PetTypes
                .SingleOrDefaultAsync(u => u.Name.Equals(name));
        }

        public async Task<IEnumerable<Domain.Entities.PetType>> ListAsync()
        {
            return await this.DbContext.PetTypes.ToListAsync();
        }
    }
}
