using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;
using Conferences.Infrastructure.Persistence;

namespace Conferences.Infrastructure.Repositories
{
    internal class ImportantDatesRepository(ConferencesDbContext dbContext) : IImportantDatesRepository
    {
        public async Task<int> CreateAsync(ImportantDate importantDate)
        {
            dbContext.ImportantDates.Add(importantDate);
            await dbContext.SaveChangesAsync();

            return importantDate.Id;
        }
    }
}
