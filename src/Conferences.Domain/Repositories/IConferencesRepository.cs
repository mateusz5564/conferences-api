﻿
using Conferences.Domain.Constants;
using Conferences.Domain.Entities;

namespace Conferences.Domain.Repositories
{
    public interface IConferencesRepository
    {
        Task<IEnumerable<Conference>> GetAllAsync();
        Task<(IEnumerable<Conference>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize,
            int pageNumber, string? sortBy, SortDirection? sortDirection);
        Task<Conference?> GetByIdAsync(int id);
        Task<int> CreateAsync(Conference conference);
        Task DeleteAsync(Conference conference);
        Task SaveChangesAsync();
    }
}
