
namespace Conferences.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<Conference> Conferences { get; set; } = [];
    }
}
