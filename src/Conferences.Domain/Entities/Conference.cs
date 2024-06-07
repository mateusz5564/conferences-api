
using NetTopologySuite.Geometries;

namespace Conferences.Domain.Entities
{
    public class Conference
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? LogoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Point Location { get; set; } = default!;
        public bool IsAccepted { get; set; }
        public string? WebsiteUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public List<ImportantDate> ImportantDates { get; set; } = [];
        public User Owner { get; set; } = default!;
        public string OwnerId { get; set; } = default!;
    }
}
