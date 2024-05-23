using AutoMapper;
using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Commands.CreateConference
{
    public class CreateConferenceHandler(ILogger<CreateConferenceHandler> logger, IMapper mapper, IConferencesRepository conferencesRepository) : IRequestHandler<CreateConferenceCommand, int>
    {
        public async Task<int> Handle(CreateConferenceCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new conference {@Conference}", request);

            var conference = mapper.Map<Conference>(request);

            var id = await conferencesRepository.CreateAsync(conference);

            return id;
        }
    }
}
