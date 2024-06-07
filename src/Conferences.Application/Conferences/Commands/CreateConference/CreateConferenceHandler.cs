using AutoMapper;
using Conferences.Application.User;
using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Commands.CreateConference
{
    public class CreateConferenceHandler(ILogger<CreateConferenceHandler> logger,
        IMapper mapper, IConferencesRepository conferencesRepository,
        IUserContext userContext) 
        : IRequestHandler<CreateConferenceCommand, int>
    {
        public async Task<int> Handle(CreateConferenceCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{Email} [{id}] is creating a new conference {@Conference}",
                currentUser.Email, currentUser.Id, request);

            var conference = mapper.Map<Conference>(request);
            conference.OwnerId = currentUser.Id;

            var id = await conferencesRepository.CreateAsync(conference);

            return id;
        }
    }
}
