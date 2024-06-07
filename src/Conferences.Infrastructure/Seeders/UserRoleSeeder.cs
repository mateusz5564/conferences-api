using Conferences.Domain.Constants;
using Conferences.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Conferences.Infrastructure.Seeders
{
    internal class UserRoleSeeder(ConferencesDbContext dbContext) : IUserRoleSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Roles.Any())
                {
                    var roles = GetUserRoles();
                    dbContext.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetUserRoles()
        {
            List<IdentityRole> roles = [
                    new(UserRoles.Admin) {
                        NormalizedName = UserRoles.Admin.ToUpper(),
                    }
                ];

            return roles;
        }
    }
}
