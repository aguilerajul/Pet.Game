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
            var petById = await GetAsync(pet.Id);
            if (petById != null)
            {
                petById.SetName(pet.Name);
                await this.DbContext.SaveChangesAsync();
                return petById;
            }
            else
            {
                var newPet = new Domain.Entities.Pet(pet.Name, pet.Type, pet.UserId);
                this.DbContext.PetTypes.Attach(pet.Type);
                this.DbContext.Pets.Attach(newPet);
                await this.DbContext.SaveChangesAsync();

                return newPet;
            }
        }

        public async Task<Domain.Entities.Pet> Feed(Guid id)
        {
            var pet = await GetAsync(id);
            if (pet != null)
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

        public async Task<Domain.Entities.Pet> GetByNameAsync(string name)
        {
            return await this.DbContext.Pets
                .Include(p => p.Type)
                .SingleOrDefaultAsync(u => u.Name.Equals(name));
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
