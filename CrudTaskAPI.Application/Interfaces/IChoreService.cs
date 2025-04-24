using CrudTaskAPI.Application.Dto;
using CrudTaskAPI.Domain.Entities;

namespace CrudTaskAPI.Application.Interfaces
{
    public interface IChoreService
    {
        Task<IEnumerable<Chore>> GetAllAsync();
        Task<Chore> GetByIdAsync(int id);
        Task AddAsync(Chore chore);
        Task UpdateAsync(Chore chore);
        Task DeleteAsync(int id);
        Task UncompleteTask(Chore chore);
        Task CompletedTask(Chore chore);
        Task<Chore> GetChoreIfExistsAsync(ChoreCreateDto choredto);
        Task<Chore> GetChoreIfExistsForUpdateAsync(int id, string name, int categoryId);
    }

}
