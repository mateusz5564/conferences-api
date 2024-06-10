
namespace Conferences.Infrastructure.Persistence
{
    public interface IDatabaseMigrator
    {
        Task ApplyMigrations();
    }
}