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
            var nameDuplicated = await GetByNameAsync(user.Name);
            if (nameDuplicated != null)
                throw new Exception($"The user name: {user.Name} already exists");

            var userById = await GetAsync(user.Id);
            if (userById != null)
            {
                userById.SetName(user.Name);
                await this.DbContext.SaveChangesAsync();
                return userById;
            }
            else
            {
                var newUser = new User(user.Name);
                this.DbContext.Users.Attach(newUser);
                await this.DbContext.SaveChangesAsync();
                return newUser;
            }
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await this.DbContext.Users
                .Include(u => u.Pets)
                .SingleOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<User> GetByNameAsync(string name)
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
