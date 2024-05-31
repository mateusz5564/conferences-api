using Conferences.Application.Conferences.Dtos;
using Conferences.Application.ImportantDates.Dtos;
using MediatR;

namespace Conferences.Application.Conferences.Commands.CreateConference
{
    public class CreateConferenceCommand : IRequest<int>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LocationDto Location { get; set; } = default!;
        public IEnumerable<CreateImportantDateDto>? ImportantDates { get; set; } = [];
        public string? WebsiteUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
