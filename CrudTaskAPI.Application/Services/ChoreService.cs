using CrudTaskAPI.Application.Dto;
using CrudTaskAPI.Application.Interfaces;
using CrudTaskAPI.Domain.Entities;
using CrudTaskAPI.Domain.Interfaces;

namespace CrudTaskAPI.Application.Services
{
    public class ChoreService : IChoreService
    {
        private readonly IChoreRepository _iChoreRepository;

        public ChoreService(IChoreRepository iChoreRepository)
        {
            _iChoreRepository = iChoreRepository;
        }

        public async Task<IEnumerable<Chore>> GetAllAsync()
        {
            return await _iChoreRepository.GetAllAsync();
        }

        public async Task<Chore> GetByIdAsync(int id)
        {
            return await _iChoreRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Chore chore)
        {
            await _iChoreRepository.AddAsync(chore);
        }

        public async Task UpdateAsync(Chore task)
        {
            await _iChoreRepository.UpdateAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            await _iChoreRepository.DeleteAsync(id);
        }

        public async Task UncompleteTask(Chore chore)
        {
            chore.isCompleted = false;
            await _iChoreRepository.UpdateAsync(chore);
        }

        public async Task CompletedTask(Chore chore)
        {
            chore.isCompleted = true;
            await _iChoreRepository.UpdateAsync(chore);
        }
        public async Task<Chore> GetChoreIfExistsAsync(ChoreCreateDto choredto)
        {
            var chore = new Chore
            {
                Name = choredto.Name,
                CategoryId = choredto.CategoryId
            };

            var choreExists = await _iChoreRepository.GetByChoreAsync(chore);
            return choreExists;
        }

        public async Task<Chore> GetChoreIfExistsForUpdateAsync(int id, string name, int categoryId)
        {
            var chore = new Chore
            {
                Id = id,
                Name = name,
                CategoryId = categoryId
            };

            var choreExists = await _iChoreRepository.GetByChoreForUpdateAsync(chore);
            return choreExists;
        }
    }
}
