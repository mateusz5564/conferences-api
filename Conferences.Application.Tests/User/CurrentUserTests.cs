using Conferences.Domain.Constants;
using FluentAssertions;
using Xunit;

namespace Conferences.Application.User.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            var user = new CurrentUser("1", "test@interia.pl", [UserRoles.Admin]);

            var isInRole = user.IsInRole(UserRoles.Admin);

            isInRole.Should().BeTrue();
        }  
        
        [Fact()]
        public void IsInRole_WithNotMatchingRole_ShouldReturnFalse()
        {
            var user = new CurrentUser("1", "test@interia.pl", []);

            var isInRole = user.IsInRole(UserRoles.Admin);

            isInRole.Should().BeFalse();
        }
    }
}