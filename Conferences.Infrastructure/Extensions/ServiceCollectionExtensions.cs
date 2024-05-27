
using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;
using Conferences.Infrastructure.Persistence;
using Conferences.Infrastructure.Repositories;
using Conferences.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Conferences.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConferencesDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Default"),
                    x => x.UseNetTopologySuite()
                ));

            services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<ConferencesDbContext>();

            services.AddScoped<ICategorySeeder, CategorySeeder>();
            services.AddScoped<IConferencesRepository, ConferencesRepository>();
            services.AddScoped<IImportantDatesRepository, ImportantDatesRepository>();
        }
    }
}
