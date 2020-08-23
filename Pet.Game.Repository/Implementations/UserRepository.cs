using Pet.Game.Domain.Interfaces;
using Pet.Game.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pet.Game.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Pet.Game.Repository.Implementations
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public Task<User> AddAsync(User user)
        {
            try
            {
                this.DbContext.Users.Add(user);
                this.DbContext.SaveChanges();

                return Task.FromResult(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> GetAsync(Guid id)
        {
            try
            {
                return await this.DbContext.Users
                    .Include(u => u.Pets)
                    .SingleOrDefaultAsync(u => u.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            try
            {
                return await this.DbContext.Users
                    .Include(u => u.Pets)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> UpdateAsync(Guid id, User entity)
        {
            try
            {
                var dbEntity = await GetAsync(id);
                if (dbEntity != null)
                {
                    dbEntity.LastModified = DateTime.UtcNow;
                    dbEntity.Pets = entity.Pets;

                    this.DbContext.Users.Update(dbEntity);
                    this.DbContext.SaveChanges();
                }

                return dbEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
