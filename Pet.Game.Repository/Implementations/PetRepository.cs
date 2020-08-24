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
            if (Guid.Empty == pet.Id)
            {
                var newPet = new Domain.Entities.Pet(pet.Name, pet.Type, pet.User);                
                
                this.DbContext.PetTypes.Attach(pet.Type);
                this.DbContext.Users.Attach(pet.User);
                
                this.DbContext.Pets.Add(newPet);
                await this.DbContext.SaveChangesAsync();

                return newPet;
            }                
            else
            {
                this.DbContext.Pets.Update(pet);
                await this.DbContext.SaveChangesAsync();
            }

            return pet;
        }

        public async Task<Domain.Entities.Pet> Feed(Guid id)
        {
            var pet = await GetAsync(id);
            if(pet != null)
            {
                pet.Feed(pet.Type.HappinessInterval);
                await this.DbContext.SaveChangesAsync();
            }
            return pet;
        }

        public async Task<Domain.Entities.Pet> GetAsync(Guid id)
        {
            return await this.DbContext.Pets
                .Include(p => p.Type)
                .SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<Domain.Entities.Pet>> ListAsync()
        {
            return await this.DbContext.Pets
                        .Include(p => p.Type)
                        .ToListAsync();
        }

        public async Task<Domain.Entities.Pet> Stroke(Guid id)
        {
            var pet = await GetAsync(id);
            if (pet != null)
            {
                pet.Stroke(pet.Type.HungrinessInterval);
                await this.DbContext.SaveChangesAsync();
            }
            return pet;
        }
    }
}
