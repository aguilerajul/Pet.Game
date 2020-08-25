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
            if (Guid.Empty == petType.Id)
            {
                var newPetType = new PetType(petType.Name, petType.HappinessInterval, petType.HungrinessInterval);
                this.DbContext.PetTypes.Add(petType);
                await this.DbContext.SaveChangesAsync();

                return newPetType;
            }                
            else
            {
                this.DbContext.PetTypes.Update(petType);
                await this.DbContext.SaveChangesAsync();
            }

            return petType;
        }

        public async Task<Domain.Entities.PetType> GetAsync(Guid id)
        {
            return await this.DbContext.PetTypes.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.PetType>> ListAsync()
        {
            return await this.DbContext.PetTypes.ToListAsync();
        }
    }
}
