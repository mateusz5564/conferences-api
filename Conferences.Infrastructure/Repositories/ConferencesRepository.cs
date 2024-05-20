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
            var restaurants = await dbContext.Conferences.Include(x => x.Category).ToListAsync();

            return restaurants;
        }
    }
}
