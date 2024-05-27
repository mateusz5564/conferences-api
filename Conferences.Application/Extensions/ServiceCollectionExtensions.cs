using Conferences.Application.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Conferences.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddScoped<IUserContext, UserContext>();

            services.AddHttpContextAccessor();
        }
    }
}
