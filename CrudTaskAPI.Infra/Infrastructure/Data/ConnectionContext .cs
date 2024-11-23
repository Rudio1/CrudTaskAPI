using CrudTaskAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudTaskAPI.Infra.Infrastructure.Data
{
    public class ConnectionContext : DbContext
    {

        public DbSet<Chore> chores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChoreMap).Assembly);

        }
    }
}
