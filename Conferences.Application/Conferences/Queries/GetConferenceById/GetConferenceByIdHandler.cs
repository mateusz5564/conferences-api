using AutoMapper;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Queries.GetConferenceById
{
    public class GetConferenceByIdHandler(ILogger<GetConferenceByIdHandler> logger, IMapper mapper,
        IConferencesRepository conferencesRepository) : IRequestHandler<GetConferenceByIdQuery, ConferenceDto>
    {
        public async Task<ConferenceDto> Handle(GetConferenceByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting conference with id: {ConferenceId}", request.Id);

            var conference = await conferencesRepository.GetByIdAsync(request.Id);

            var conferenceDto = mapper.Map<ConferenceDto>(conference);

            return conferenceDto;
        }
    }
}
