using AutoMapper;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Repositories;

namespace Conferences.Application.Conferences
{
    public class ConferencesService(IConferencesRepository conferencesRepository, IMapper mapper) : IConferencesService
    {
        public  async Task<IEnumerable<ConferenceDto>> GetAllConferences()
        {
            var conferences = await conferencesRepository.GetAllAsync();
            var conferencesDto = mapper.Map<IEnumerable<ConferenceDto>>(conferences);

            return conferencesDto;
        }
    }
}
