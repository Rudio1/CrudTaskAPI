using CrudTaskAPI.Domain.Entities;
using CrudTaskAPI.Domain.Interfaces;
using CrudTaskAPI.Infra.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudTaskAPI.Infra.Repositories
{
    public class ChoreRepository : IChoreRepository
    {
        private readonly ConnectionContext _context;

        public ChoreRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chore>> GetAllAsync()
        {
            return await _context.chores
                          .Include(chore => chore.Category)
                          .ToListAsync();
        }

        public async Task<Chore> GetByIdAsync(int id)
        {
            return await _context.chores
                        .Include(c => c.Category)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Chore chore)
        {
            await _context.chores.AddAsync(chore);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chore chore)
        {
            _context.chores.Update(chore);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chore = await _context.chores.FindAsync(id);
            if (chore != null)
            {
                chore.Active = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Chore> GetByChoreAsync(Chore chore)
        {
            return await _context.chores.FirstOrDefaultAsync(c => c.Name == chore.Name && c.CategoryId == chore.CategoryId);
        }
    }
}
