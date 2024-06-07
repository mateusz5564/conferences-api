using Microsoft.AspNetCore.Identity;

namespace Conferences.Domain.Entities
{
    public class User : IdentityUser
    {
        public List<Conference> Conferences { get; set; } = [];
    }
}
