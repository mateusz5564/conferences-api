using Conferences.Domain.Repositories;
using MediatR;

namespace Conferences.Application.Conferences.Commands.DeleteConference
{
    public class DeleteConferenceHandler(IConferencesRepository conferencesRepository) : IRequestHandler<DeleteConferenceCommand, bool>
    {
        public async Task<bool> Handle(DeleteConferenceCommand request, CancellationToken cancellationToken)
        {
            var conference = await conferencesRepository.GetByIdAsync(request.Id);

            if (conference is null) return false;

            await conferencesRepository.DeleteAsync(conference);

            return true;
        }
    }
}
