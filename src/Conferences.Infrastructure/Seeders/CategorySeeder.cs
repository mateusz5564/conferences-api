
using Conferences.Domain.Entities;
using Conferences.Infrastructure.Persistence;

namespace Conferences.Infrastructure.Seeders
{
    internal class CategorySeeder(ConferencesDbContext dbContext) : ICategorySeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Categories.Any())
                {
                    var categories = getCategories();
                    dbContext.Categories.AddRange(categories);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private List<Category> getCategories()
        {
            var categories = new List<Category> {
                new Category { Name = "Astronomia"},
                new Category { Name = "Biliotekoznawstwo"},
                new Category { Name = "Biologia"},
                new Category { Name = "Chemia"},
                new Category { Name = "Ekonomia"},
                new Category { Name = "Energetyka"},
                new Category { Name = "Filozofia"},
                new Category { Name = "Finanse"},
                new Category { Name = "Informatyka"},
                new Category { Name = "Matematyka"},
                new Category { Name = "Socjologia"},
                new Category { Name = "Teologia"},
            };

            return categories;
        }
    }
}
