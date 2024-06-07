using Conferences.Application.ImportantDates.Dtos;
using MediatR;

namespace Conferences.Application.ImportantDates.Queries.GetImportantDateForConferenceById
{
    public class GetImportantDateForConferenceByIdQuery(int conferenceId,
        int importantDateId)
        : IRequest<ImportantDateDto>
    {
        public int ConferenceId { get; } = conferenceId;
        public int ImportantDateId { get; } = importantDateId;
    }
}
