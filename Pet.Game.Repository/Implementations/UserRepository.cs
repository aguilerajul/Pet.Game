using Pet.Game.Domain.Interfaces;
using Pet.Game.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pet.Game.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Internal;

namespace Pet.Game.Repository.Implementations
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly IPetRepository petRepository;
        private readonly IUserPetsRepository userPetsRepository;

        public UserRepository(IConfiguration configuration, 
            IPetRepository petRepository,
            IUserPetsRepository userPetsRepository) : base(configuration)
        {
            this.petRepository = petRepository;
            this.userPetsRepository = userPetsRepository;
        }

        public async Task<User> AddOrUpdateAsync(User user)
        {
            var dbExistingEntity = await GetAsync(user.Id);
            if (dbExistingEntity == null)
            {
                var newUser = new User(user.Name, user.Pets);
                if(user.Pets.Any())
                {
                    foreach (var pet in user.Pets)
                    {
                        var existingPet = await petRepository.GetAsync(pet.Id);
                        if (existingPet != null)
                        {
                            var userPets = new UserPets(newUser, existingPet);
                            await userPetsRepository.AddOrUpdateAsync(userPets);
                        }
                        else
                            throw new Exception($"The Pet Id: {pet.Id} doesn't exists in the database");
                    }
                }

                this.DbContext.Users.Add(user);
                this.DbContext.SaveChanges();

                return newUser;
            }                
            else
            {
                this.DbContext.Users.Update(user);
                this.DbContext.SaveChanges();
            }                

            return user;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await this.DbContext.Users
                .Include(u => u.Pets)
                .SingleOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            var users = this.DbContext.Users.ToList();
            if(users.Any())
            {
                foreach (var user in users)
                {
                    user.Pets = this.DbContext.Pets.Select(p => this.DbContext.UsersPets.Any(up => up.Pet.Id.Equals(p.Id) && user.Id.Equals(up.User.Id)));
                }
            }
        }
    }
}
