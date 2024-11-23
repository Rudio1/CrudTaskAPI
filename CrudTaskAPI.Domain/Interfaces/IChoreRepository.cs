using CrudTaskAPI.Domain.Entities;

namespace CrudTaskAPI.Domain.Interfaces
{
    public interface IChoreRepository
    {
        Task<IEnumerable<Chore>> GetAllAsync();
        Task<Chore> GetByIdAsync(int id);
        Task AddAsync(Chore chore);
        Task UpdateAsync(Chore chore);
        Task DeleteAsync(int id);
        Task<Chore> GetByChoreAsync(Chore chore);
    }
}
