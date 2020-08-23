using Microsoft.Extensions.Configuration;
using Pet.Game.Domain.Interfaces;
using Pet.Game.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pet.Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Pet.Game.Repository.Implementations
{
    public class UserPetsRepository : RepositoryBase, IUserPetsRepository
    {
        public UserPetsRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<UserPets> AddOrUpdateAsync(UserPets userPets)
        {
            var dbExistingEntity = await GetAsync(userPets.User.Id, userPets.Pet.Id);
            if (dbExistingEntity == null)
            {
                var newUserPets = new UserPets(userPets.User, userPets.Pet);
                this.DbContext.UsersPets.Add(newUserPets);
                this.DbContext.SaveChanges();

                return newUserPets;
            }
            else
            {
                this.DbContext.UsersPets.Update(userPets);
                this.DbContext.SaveChanges();
            }

            return userPets;
        }

        public async Task<UserPets> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserPets> GetAsync(Guid userId, Guid petId)
        {
            return await this.DbContext.UsersPets.FirstOrDefaultAsync(up => up.User.Id.Equals(userId) && up.Pet.Id.Equals(petId));
        }

        public async Task<IEnumerable<UserPets>> ListAsync()
        {
            return await this.DbContext.UsersPets.ToListAsync();
        }
    }
}
