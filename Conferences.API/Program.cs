using Conferences.Infrastructure.Extensions;
using Conferences.Infrastructure.Seeders;
using Conferences.Application.Extensions;
using Serilog;
using Conferences.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);

});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
