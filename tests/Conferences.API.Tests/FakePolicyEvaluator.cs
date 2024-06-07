using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Conferences.API.Tests
{
    internal class FakePolicyEvaluator : IPolicyEvaluator
    {
        private readonly FakePolicyConfig _fakePolicyConfig;

        public FakePolicyEvaluator(FakePolicyConfig fakePolicyConfig)
        {
            _fakePolicyConfig = fakePolicyConfig;
        }

        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            if (!_fakePolicyConfig.IsAuthenticated)
            {
                context.User = new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(AuthenticateResult.Fail("Unauthenticated"));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, _fakePolicyConfig.Id),
                new Claim(ClaimTypes.Email, _fakePolicyConfig.Email)
            };

            foreach (var role in _fakePolicyConfig.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            var ticket = new AuthenticationTicket(claimsPrincipal, "Test");
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            if (!_fakePolicyConfig.IsAuthorized)
            {
                return Task.FromResult(PolicyAuthorizationResult.Forbid());
            }

            var result = PolicyAuthorizationResult.Success();
            return Task.FromResult(result);
        }
    }
}
