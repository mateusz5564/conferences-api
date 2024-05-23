using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Commands.DeleteConference
{
    public class DeleteConferenceHandler(ILogger<DeleteConferenceHandler> logger,
        IConferencesRepository conferencesRepository) : IRequestHandler<DeleteConferenceCommand, bool>
    {
        public async Task<bool> Handle(DeleteConferenceCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting conference with id: {ConferenceId}", request.Id);

            var conference = await conferencesRepository.GetByIdAsync(request.Id);

            if (conference is null) return false;

            await conferencesRepository.DeleteAsync(conference);

            return true;
        }
    }
}
