using Conferences.Infrastructure.Extensions;
using Conferences.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var categorySeeder = scope.ServiceProvider.GetRequiredService<ICategorySeeder>();
await categorySeeder.Seed();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
