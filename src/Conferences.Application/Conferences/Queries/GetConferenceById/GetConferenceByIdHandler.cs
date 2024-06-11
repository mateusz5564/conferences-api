using AutoMapper;
using Conferences.Application.Conferences.Dtos;
using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Queries.GetConferenceById
{
    public class GetConferenceByIdHandler(ILogger<GetConferenceByIdHandler> logger, IMapper mapper,
        IConferencesRepository conferencesRepository,
        IBlobStorageService blobStorageService) : IRequestHandler<GetConferenceByIdQuery, ConferenceDto>
    {
        public async Task<ConferenceDto> Handle(GetConferenceByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting conference with id: {ConferenceId}", request.Id);

            var conference = await conferencesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Conference), request.Id.ToString());

            var conferenceDto = mapper.Map<ConferenceDto>(conference);

            conferenceDto.LogoUrl = blobStorageService.GetBlobSasUrl(conference.LogoUrl);
            
            return conferenceDto;
        }
    }
}
