using AutoMapper;
using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;
using MediatR;

namespace Conferences.Application.Conferences.Commands.CreateConference
{
    public class CreateConferenceHandler(IMapper mapper, IConferencesRepository conferencesRepository) : IRequestHandler<CreateConferenceCommand, int>
    {
        public async Task<int> Handle(CreateConferenceCommand request, CancellationToken cancellationToken)
        {
            var conference = mapper.Map<Conference>(request);

            var id = await conferencesRepository.CreateAsync(conference);

            return id;
        }
    }
}
