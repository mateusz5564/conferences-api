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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conference>()
                .HasOne(c => c.Category)
                .WithMany(ca => ca.Conferences)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Conference>()
                .HasMany(c => c.ImportantDates)
                .WithOne(id => id.Conference)
                .HasForeignKey(id => id.ConferenceId);

            modelBuilder.Entity<Conference>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.Conferences)
                .HasForeignKey(c => c.OwnerId);
        }
    }
}
