using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pet.Game.Domain.Interfaces;
using Pet.Game.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pet.Game.Repository.Implementations
{
    public class PetRepository : RepositoryBase, IPetRepository
    {
        public PetRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Domain.Entities.Pet> AddOrUpdateAsync(Domain.Entities.Pet pet)
        {
            var dbExistingEntity = await GetAsync(pet.Id);
            if (dbExistingEntity == null)
            {
                var newPet = new Domain.Entities.Pet(pet.Name, pet.Type, pet.HappinessDecreaseInterval, pet.HungrinessIncreaseInterval);
                this.DbContext.Pets.Add(newPet);
                this.DbContext.SaveChanges();

                return newPet;
            }                
            else
            {
                this.DbContext.Pets.Update(pet);
                this.DbContext.SaveChanges();
            }

            return pet;
        }

        public async Task<Domain.Entities.Pet> GetAsync(Guid id)
        {
            return await this.DbContext.Pets.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.Pet>> ListAsync()
        {
            return await this.DbContext.Pets.ToListAsync();
        }
    }
}
