using Conferences.Infrastructure.Extensions;
using Conferences.Infrastructure.Seeders;
using Conferences.Application.Extensions;
using Serilog;
using Conferences.API.Middlewares;
using Conferences.Domain.Entities;
using Conferences.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

var scope = app.Services.CreateScope();
var categorySeeder = scope.ServiceProvider.GetRequiredService<ICategorySeeder>();
await categorySeeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
