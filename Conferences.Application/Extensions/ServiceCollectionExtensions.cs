using Conferences.Application.Conferences;
using Microsoft.Extensions.DependencyInjection;

namespace Conferences.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IConferencesService, ConferencesService>();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
