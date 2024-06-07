namespace Conferences.Application.ImportantDates.Dtos
{
    public class CreateImportantDateDto
    {
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
