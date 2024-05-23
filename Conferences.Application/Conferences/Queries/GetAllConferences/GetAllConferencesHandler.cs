using AutoMapper;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Queries.GetAllConferences
{
    public class GetAllConferencesHandler(ILogger<GetAllConferencesHandler> logger, IMapper mapper, 
        IConferencesRepository conferencesRepository) : IRequestHandler<GetAllConferencesQuery, IEnumerable<ConferenceDto>>
    {
        public async Task<IEnumerable<ConferenceDto>> Handle(GetAllConferencesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all conferences");

            var conferences = await conferencesRepository.GetAllAsync();
            var conferencesDto = mapper.Map<IEnumerable<ConferenceDto>>(conferences);

            return conferencesDto;
        }
    }
}
