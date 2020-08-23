using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pet.Game.Domain.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> ListAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(Guid id, T entity);
    }
}
