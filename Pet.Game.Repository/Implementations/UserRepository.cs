using Pet.Game.Domain.Interfaces;
using Pet.Game.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pet.Game.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Pet.Game.Repository.Implementations
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly IPetRepository petRepository;

        public UserRepository(IConfiguration configuration,
            IPetRepository petRepository) : base(configuration)
        {
            this.petRepository = petRepository;
        }

        public async Task<User> AddOrUpdateAsync(User user)
        {
            var userDuplicated = await GetByName(user.Name);
            if (Guid.Empty == user.Id)
            {
                if (userDuplicated != null)
                    throw new Exception($"The user name: {user.Name} already exists");

                var newUser = new User(user.Name, user.Pets);
                this.DbContext.Users.Add(newUser);
                await this.DbContext.SaveChangesAsync();
                return newUser;
            }
            else
            {                
                this.DbContext.Users.Update(user);
                await this.DbContext.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await this.DbContext.Users
                .Include(u => u.Pets)
                .SingleOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<User> GetByName(string name)
        {
            return await this.DbContext.Users
                .Include(u => u.Pets)
                .SingleOrDefaultAsync(u => u.Name.Equals(name));
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await this.DbContext.Users.ToListAsync();
        }
    }
}
