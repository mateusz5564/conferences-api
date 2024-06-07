using Conferences.Domain.Constants;

namespace Conferences.API.Tests
{
    internal class FakePolicyConfig {
        public string Id { get; set; } = "1";
        public string Email { get; set; } = "test@interia.pl";
        public string[] Roles { get; set; } = new[] { UserRoles.Admin };
        public bool IsAuthenticated { get; set; } = true;
        public bool IsAuthorized { get; set; } = true;
    }
}
