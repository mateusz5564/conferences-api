
using Microsoft.EntityFrameworkCore;
using Conferences.Domain.Entities;

namespace Conferences.Infrastructure.Persistence
{
    internal class ConferencesDbContext(DbContextOptions<ConferencesDbContext> options) : DbContext(options)
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
