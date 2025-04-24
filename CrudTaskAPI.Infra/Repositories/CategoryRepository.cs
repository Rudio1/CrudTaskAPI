using CrudTaskAPI.Domain.Interfaces;
using CrudTaskAPI.Infra.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudTaskAPI.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ConnectionContext _context;

        public CategoryRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Chores)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
