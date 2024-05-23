using Conferences.Application.Conferences.Dtos;
using MediatR;

namespace Conferences.Application.Conferences.Commands.UpdateConference
{
    public class UpdateConferenceCommand : IRequest
    {
        public int Id { get; set; }

        public string? Title { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public UpdateLocationDto? Location { get; set; } = default!;
        public string? WebsiteUrl { get; set; }
        public int? CategoryId { get; set; }
    }
}

