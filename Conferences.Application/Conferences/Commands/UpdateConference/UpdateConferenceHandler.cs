using AutoMapper;
using Conferences.Domain.Repositories;
using MediatR;

namespace Conferences.Application.Conferences.Commands.UpdateConference
{
    public class UpdateConferenceHandler(IMapper mapper, IConferencesRepository conferencesRepository) : IRequestHandler<UpdateConferenceCommand, bool>
    {
        public async Task<bool> Handle(UpdateConferenceCommand request, CancellationToken cancellationToken)
        {
            var conference = await conferencesRepository.GetByIdAsync(request.Id);

            if (conference == null) return false;

            mapper.Map(request, conference);
            await conferencesRepository.SaveChangesAsync();

            return true;
        }
    }
}
