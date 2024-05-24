using AutoMapper;
using Conferences.Application.ImportantDates.Dtos;
using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Conferences.Application.ImportantDates.Queries.GetAllImportantDatesForConference
{
    public class GetImportantDatesForConferenceHandler(
        ILogger<GetImportantDatesForConferenceHandler> logger,
        IMapper mapper,
        IConferencesRepository conferencesRepository)
        : IRequestHandler<GetImportantDatesForConferenceQuery, IEnumerable<ImportantDateDto>>
    {
        public async Task<IEnumerable<ImportantDateDto>> Handle(
            GetImportantDatesForConferenceQuery request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all important dates for conference with id: " +
                "{conferenceId}", request.ConferenceId);

            var conference = await conferencesRepository.GetByIdAsync(request.ConferenceId)
                ?? throw new NotFoundException(nameof(Conference), request.ConferenceId.ToString());

            var importantDatesDtos = mapper
                .Map<IEnumerable<ImportantDateDto>>(conference.ImportantDates);

            return importantDatesDtos;
        }
    }
}
