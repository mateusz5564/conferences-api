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
            var conferences = await dbContext.Conferences.Include(x => x.Category).ToListAsync();

            return conferences;
        }

        public async Task<Conference?> GetByIdAsync(int id)
        {
            var conference = await dbContext.Conferences.Include(x => x.Category).FirstOrDefaultAsync(c => c.Id == id);

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
