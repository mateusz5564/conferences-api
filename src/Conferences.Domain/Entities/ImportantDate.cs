namespace Conferences.Domain.Entities
{
    public class ImportantDate
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }

        public int ConferenceId { get; set; }
        public Conference Conference { get; set; } = default!;
    }
}
