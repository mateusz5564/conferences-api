using Conferences.Domain.Constants;
using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Commands.UploadConferenceLogo
{
    internal class UploadConferenceLogoHandler(ILogger<UploadConferenceLogoCommand> logger,
        IConferencesRepository conferencesRepository,
        IConferenceAuthorizationService conferenceAuthorizationService,
        IBlobStorageService blobStorageService)
        : IRequestHandler<UploadConferenceLogoCommand>
    {
        public async Task Handle(UploadConferenceLogoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uploading logo for conference with id: {ConferenceId}, filename: {Filename}",
                request.ConferenceId, request.Filename);

            var conference = await conferencesRepository.GetByIdAsync(request.ConferenceId)
                ?? throw new NotFoundException(nameof(Conference), request.ConferenceId.ToString());

            if (!conferenceAuthorizationService.Authorize(conference, ResourceOperation.Update))
                throw new ForbidException();

            conference.LogoUrl = await blobStorageService.UploadToBlobAsync(request.File, request.Filename);

            await conferencesRepository.SaveChangesAsync();
        }
    }
}
