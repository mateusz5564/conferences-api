using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Conferences.Application.Conferences.Commands.DeleteConference
{
    public class DeleteConferenceHandler(ILogger<DeleteConferenceHandler> logger,
        IConferencesRepository conferencesRepository) : IRequestHandler<DeleteConferenceCommand>
    {
        public async Task Handle(DeleteConferenceCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting conference with id: {ConferenceId}", request.Id);

            var conference = await conferencesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Conference), request.Id.ToString());

            await conferencesRepository.DeleteAsync(conference);
        }
    }
}
