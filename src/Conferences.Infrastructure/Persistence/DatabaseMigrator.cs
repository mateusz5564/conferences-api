using Microsoft.EntityFrameworkCore;

namespace Conferences.Infrastructure.Persistence
{
    internal class DatabaseMigrator(ConferencesDbContext dbContext) : IDatabaseMigrator
    {
        public async Task ApplyMigrations()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }
        }
    }
}
