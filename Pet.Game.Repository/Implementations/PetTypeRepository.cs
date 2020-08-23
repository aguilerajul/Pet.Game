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
            var dbExistingEntity = await GetAsync(petType.Id);
            if (dbExistingEntity == null)
            {
                var newPetType = new PetType(petType.Name);
                this.DbContext.PetTypes.Add(newPetType);
                this.DbContext.SaveChanges();

                return newPetType;
            }                
            else
            {
                this.DbContext.PetTypes.Update(petType);
                this.DbContext.SaveChanges();
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
