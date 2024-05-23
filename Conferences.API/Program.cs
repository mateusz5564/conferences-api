using Conferences.Infrastructure.Extensions;
using Conferences.Infrastructure.Seeders;
using Conferences.Application.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);

});

var app = builder.Build();

var scope = app.Services.CreateScope();
var categorySeeder = scope.ServiceProvider.GetRequiredService<ICategorySeeder>();
await categorySeeder.Seed();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
