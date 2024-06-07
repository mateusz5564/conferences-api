using Conferences.Application.Categories.Dtos;
using Conferences.Application.ImportantDates.Dtos;

namespace Conferences.Application.Conferences.Dtos
{
    public class ConferenceDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LocationDto Location { get; set; } = default!;
        public bool IsAccepted { get; set; }
        public string? WebsiteUrl { get; set; }
        public CategoryDto Category { get; set; } = default!;
        public List<ImportantDateDto> ImportantDates { get; set; } = [];
    }
}
