using Conferences.Domain.Constants;
using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.ImportantDates.Commands.DeleteImportantDateForConferenceById
{
    public class DeleteImportantDateForConferenceByIdHandler(
        ILogger<DeleteImportantDateForConferenceByIdHandler> logger,
        IConferencesRepository conferencesRepository,
        IImportantDatesRepository importantDatesRepository,
        IConferenceAuthorizationService conferenceAuthorizationService)
        : IRequestHandler<DeleteImportantDateForConferenceByIdCommand>
    {
        public async Task Handle(DeleteImportantDateForConferenceByIdCommand request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting important date with id: {importantDateId}, " +
                "for conference with id {conferenceId}",
                request.ImportantDateId.ToString(),
                request.ConferenceId.ToString());

            var conference = await conferencesRepository.GetByIdAsync(request.ConferenceId)
                ?? throw new NotFoundException(nameof(Conference), request.ConferenceId.ToString());

            if (!conferenceAuthorizationService.Authorize(conference, ResourceOperation.Update))
                throw new ForbidException();

            var importantDate = conference.ImportantDates
                .FirstOrDefault(i => i.Id == request.ImportantDateId)
                ?? throw new NotFoundException(nameof(ImportantDate),
                request.ImportantDateId.ToString()); ;

            await importantDatesRepository.DeleteAsync(importantDate);
        }
    }
}
