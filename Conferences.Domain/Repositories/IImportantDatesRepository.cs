using Conferences.Domain.Entities;

namespace Conferences.Domain.Repositories
{
    public interface IImportantDatesRepository
    {
        Task<int> CreateAsync(ImportantDate importantDate);
    }
}
