using Conferences.Domain.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace Conferences.Application.User.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, "1"),
                new (ClaimTypes.Email, "test@interia.pl"),
                new (ClaimTypes.Role, UserRoles.Admin)

            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);

            var currentUser = userContext.GetCurrentUser();

            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@interia.pl");
            currentUser.Roles.Should().Contain(UserRoles.Admin);
        }

        [Fact()]
        public void GetCurrentUser_WithoutAuthenticatedUser_ShouldThrowException()
        {
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            Action action = () => userContext.GetCurrentUser();

            action
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }
    }
}