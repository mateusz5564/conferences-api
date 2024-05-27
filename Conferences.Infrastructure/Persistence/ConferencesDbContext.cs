using Microsoft.EntityFrameworkCore;
using Conferences.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Conferences.Infrastructure.Persistence
{
    internal class ConferencesDbContext(DbContextOptions<ConferencesDbContext> options) 
        : IdentityDbContext<User>(options)
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImportantDate> ImportantDates { get; set; }
    }
}
