using AutoMapper;
using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.ImportantDates.Commands.CreateImportantDate
{
    public class CreateImportantDateHandler(IMapper mapper,
        ILogger<CreateImportantDateHandler> logger,
        IConferencesRepository conferencesRepository,
        IImportantDatesRepository importantDatesRepository)
        : IRequestHandler<CreateImportantDateCommand, int>
    {
        public async Task<int> Handle(CreateImportantDateCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new important date ({@ImportantDateRequest})" +
                " for conference with id {id}",
                request, request.ConferenceId);

            var conference = await conferencesRepository.GetByIdAsync(request.ConferenceId)
                ?? throw new NotFoundException(nameof(Conference), request.ConferenceId.ToString());

            var importantDate = mapper.Map<ImportantDate>(request);
            var id = await importantDatesRepository.CreateAsync(importantDate);

            return id;
        }
    }
}
