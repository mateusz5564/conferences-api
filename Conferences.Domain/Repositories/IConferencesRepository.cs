
using Conferences.Domain.Entities;

namespace Conferences.Domain.Repositories
{
    public interface IConferencesRepository
    {
        Task<IEnumerable<Conference>> GetAllAsync();
        Task<IEnumerable<Conference>> GetAllMatchingAsync(string? searchPhrase);
        Task<Conference?> GetByIdAsync(int id);
        Task<int> CreateAsync(Conference conference);
        Task DeleteAsync(Conference conference);
        Task SaveChangesAsync();
    }
}
