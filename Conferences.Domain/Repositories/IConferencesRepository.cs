
using Conferences.Domain.Entities;

namespace Conferences.Domain.Repositories
{
    public interface IConferencesRepository
    {
        Task<IEnumerable<Conference>> GetAllAsync();
        Task<Conference?> GetByIdAsync(int id);
        Task<int> CreateAsync(Conference conference);
        Task DeleteAsync(Conference conference);
    }
}
