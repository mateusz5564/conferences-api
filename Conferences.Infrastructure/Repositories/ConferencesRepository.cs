using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;
using Conferences.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Conferences.Infrastructure.Repositories
{
    internal class ConferencesRepository(ConferencesDbContext dbContext) : IConferencesRepository
    {
        public async Task<IEnumerable<Conference>> GetAllAsync()
        {
            var conferences = await dbContext.Conferences
                .Include(x => x.Category)
                .Include(x => x.ImportantDates)
                .ToListAsync();

            return conferences;
        }

        public async Task<(IEnumerable<Conference>, int)> GetAllMatchingAsync(string? searchPhrase,
            int pageSize,
            int pageNumber)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext.Conferences
                .Include(x => x.Category)
                .Include(x => x.ImportantDates)
                .Where(c => searchPhrase == null || c.Title.ToLower().Contains(searchPhraseLower)
                                                 || c.Description.ToLower().Contains(searchPhraseLower));

            var totalCount = await baseQuery.CountAsync();

            var conferences = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (conferences, totalCount);
        }

        public async Task<Conference?> GetByIdAsync(int id)
        {
            var conference = await dbContext.Conferences
                .Include(x => x.Category)
                .Include(x => x.ImportantDates)
                .FirstOrDefaultAsync(c => c.Id == id);

            return conference;
        }

        public async Task<int> CreateAsync(Conference conference)
        {
            dbContext.Conferences.Add(conference);
            await dbContext.SaveChangesAsync();

            return conference.Id;
        }

        public async Task DeleteAsync(Conference conference)
        {
            dbContext.Conferences.Remove(conference);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
