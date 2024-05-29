using AutoMapper;
using Conferences.Domain.Constants;
using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Commands.UpdateConference
{
    public class UpdateConferenceHandler(ILogger<UpdateConferenceHandler> logger, IMapper mapper,
        IConferencesRepository conferencesRepository,
        IConferenceAuthorizationService conferenceAuthorizationService)
        : IRequestHandler<UpdateConferenceCommand>
    {
        public async Task Handle(UpdateConferenceCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating conference with id: {ConferenceId}, body: {@UpdateBody}",
                request.Id, request);

            var conference = await conferencesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Conference), request.Id.ToString());

            if (!conferenceAuthorizationService.Authorize(conference, ResourceOperation.Update))
                throw new ForbidException();

            mapper.Map(request, conference);
            await conferencesRepository.SaveChangesAsync();
        }
    }
}
