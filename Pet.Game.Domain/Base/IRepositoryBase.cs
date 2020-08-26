using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pet.Game.Domain.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<T> GetByNameAsync(string name);
        Task<IEnumerable<T>> ListAsync();
        Task<T> AddOrUpdateAsync(T entity);
    }
}
