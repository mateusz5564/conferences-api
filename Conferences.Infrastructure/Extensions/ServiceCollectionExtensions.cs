
using Conferences.Infrastructure.Persistence;
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
        }
    }
}
