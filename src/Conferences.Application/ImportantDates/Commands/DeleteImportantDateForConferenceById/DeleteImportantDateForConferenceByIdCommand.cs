using MediatR;

namespace Conferences.Application.ImportantDates.Commands.DeleteImportantDateForConferenceById
{
    public class DeleteImportantDateForConferenceByIdCommand(int conferenceId, 
        int importantDateId)
        : IRequest
    {
        public int ConferenceId { get; } = conferenceId;
        public int ImportantDateId { get; } = importantDateId;
    }
}
