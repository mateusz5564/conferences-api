using AutoMapper;
using Conferences.Application.ImportantDates.Dtos;
using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.ImportantDates.Queries.GetImportantDateForConferenceById
{
    public class GetImportantDateForConferenceByIdHandler(
        ILogger<GetImportantDateForConferenceByIdHandler> logger,
        IMapper mapper,
        IConferencesRepository conferencesRepository)
        : IRequestHandler<GetImportantDateForConferenceByIdQuery, ImportantDateDto>
    {
        public async Task<ImportantDateDto> Handle(GetImportantDateForConferenceByIdQuery request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting important date with id: {importantDateId}, " +
                "for conference with id {conferenceId}",
                request.ImportantDateId.ToString(),
                request.ConferenceId.ToString());

            var conference = await conferencesRepository.GetByIdAsync(request.ConferenceId)
                ?? throw new NotFoundException(nameof(Conference), request.ConferenceId.ToString());

            var importantDate = conference.ImportantDates
                .FirstOrDefault(i => i.Id == request.ImportantDateId)
                ?? throw new NotFoundException(nameof(ImportantDate),
                request.ImportantDateId.ToString()); ;

            var importantDatesDto = mapper.Map<ImportantDateDto>(importantDate);

            return importantDatesDto;
        }
    }
}
