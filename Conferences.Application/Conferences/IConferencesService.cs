using Conferences.Application.Conferences.Dtos;

namespace Conferences.Application.Conferences
{
    public interface IConferencesService
    {
        Task<IEnumerable<ConferenceDto>> GetAllConferences();
        Task<ConferenceDto?> GetConferenceById(int id);
    }
}