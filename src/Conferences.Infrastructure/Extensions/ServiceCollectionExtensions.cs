﻿using Conferences.Domain.Entities;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Repositories;
using Conferences.Infrastructure.Authorization.Services;
using Conferences.Infrastructure.Configuration;
using Conferences.Infrastructure.Persistence;
using Conferences.Infrastructure.Repositories;
using Conferences.Infrastructure.Seeders;
using Conferences.Infrastructure.Storage;
using Microsoft.AspNetCore.Identity;
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
                    builder =>
                    {
                        builder.UseNetTopologySuite();
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }
                ));

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ConferencesDbContext>();

            services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();
            services.AddScoped<ICategorySeeder, CategorySeeder>();
            services.AddScoped<IUserRoleSeeder, UserRoleSeeder>();
            services.AddScoped<IConferencesRepository, ConferencesRepository>();
            services.AddScoped<IImportantDatesRepository, ImportantDatesRepository>();
            services.AddScoped<IConferenceAuthorizationService, ConferenceAuthorizationService>();

            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
            services.AddScoped<IBlobStorageService, BlobStorageService>();
        }
    }
}
