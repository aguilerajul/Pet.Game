using System;
using System.Threading.Tasks;

namespace Pet.Game.Domain.Interfaces
{
    public interface IPetRepository : Base.IRepositoryBase<Entities.Pet>
    {
        Task<Entities.Pet> Stroke(Guid id);
        Task<Entities.Pet> Feed(Guid id);
    }
}
