using Conferences.Application.ImportantDates.Dtos;
using MediatR;

namespace Conferences.Application.ImportantDates.Queries.GetAllImportantDatesForConference
{
    public class GetImportantDatesForConferenceQuery(int conferenceId)
        : IRequest<IEnumerable<ImportantDateDto>>
    {
        public int ConferenceId { get; } = conferenceId;
    }
}
