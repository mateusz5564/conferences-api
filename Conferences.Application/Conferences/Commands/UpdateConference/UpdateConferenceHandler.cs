using AutoMapper;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.Conferences.Commands.UpdateConference
{
    public class UpdateConferenceHandler(ILogger<UpdateConferenceHandler> logger, IMapper mapper, 
        IConferencesRepository conferencesRepository) : IRequestHandler<UpdateConferenceCommand, bool>
    {
        public async Task<bool> Handle(UpdateConferenceCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating conference with id: {ConferenceId}, body: {@UpdateBody}", request.Id, request);

            var conference = await conferencesRepository.GetByIdAsync(request.Id);

            if (conference == null) return false;

            mapper.Map(request, conference);
            await conferencesRepository.SaveChangesAsync();

            return true;
        }
    }
}
