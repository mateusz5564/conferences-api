
namespace Conferences.Application.Conferences.Dtos
{
    public class CreateConferenceDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LocationDto Location { get; set; } = default!;
        public string? WebsiteUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
