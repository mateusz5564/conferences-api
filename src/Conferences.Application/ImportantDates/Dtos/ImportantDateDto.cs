namespace Conferences.Application.ImportantDates.Dtos
{
    public class ImportantDateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
