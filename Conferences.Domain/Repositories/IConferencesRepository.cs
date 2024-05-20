
using Conferences.Domain.Entities;

namespace Conferences.Domain.Repositories
{
    public interface IConferencesRepository
    {
        Task<IEnumerable<Conference>> GetAllAsync();
        Task<Conference?> GetByIdAsync(int id);
    }
}
