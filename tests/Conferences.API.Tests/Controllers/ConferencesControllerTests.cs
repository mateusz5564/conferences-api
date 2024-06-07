using Conferences.API.Tests;
using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using Xunit;

namespace Conferences.API.Controllers.Tests
{
    public class ConferencesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IConferencesRepository> _conferencesRepositoryMock = new();

        public ConferencesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<FakePolicyConfig>();
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped(typeof(IConferencesRepository),
                                                        _ => _conferencesRepositoryMock.Object));
                });
            });
        }

        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            var client = _factory.CreateClient();

            var result = await client.GetAsync("api/conferences?pageNumber=1&pageSize=5");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
        {
            var client = _factory.CreateClient();

            var result = await client.GetAsync("api/conferences");

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact()]
        public async void Delete_AsAdmin_Returns204NoContent()
        {
            var conferenceId = 32;

            var conference = new Conference()
            {
                Id = conferenceId,
            };

            _conferencesRepositoryMock.Setup(c => c.GetByIdAsync(conferenceId)).ReturnsAsync(conference);
            _conferencesRepositoryMock.Setup(c => c.DeleteAsync(conference)).Returns(Task.CompletedTask);

            var client = _factory.CreateClient();

            var result = await client.DeleteAsync($"api/conferences/{conferenceId}");

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact()]
        public async void Delete_AsUnauthorized_Returns403Forbid()
        {
            var policyConfig = _factory.Services.GetRequiredService<FakePolicyConfig>();
            policyConfig.IsAuthenticated = false;
            policyConfig.IsAuthorized = false;

            var conferenceId = 32;

            var conference = new Conference()
            {
                Id = conferenceId,
            };

            _conferencesRepositoryMock.Setup(c => c.GetByIdAsync(conferenceId)).ReturnsAsync(conference);
            _conferencesRepositoryMock.Setup(c => c.DeleteAsync(conference)).Returns(Task.CompletedTask);

            var client = _factory.CreateClient();

            var result = await client.DeleteAsync($"api/conferences/{conferenceId}");

            result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact()]
        public async void Delete_AsOwner_Returns204NoContent()
        {
            var policyConfig = _factory.Services.GetRequiredService<FakePolicyConfig>();
            policyConfig.Roles = [];

            var conferenceId = 32;

            var conference = new Conference()
            {
                Id = conferenceId,
                OwnerId = "1",
            };

            _conferencesRepositoryMock.Setup(c => c.GetByIdAsync(conferenceId)).ReturnsAsync(conference);
            _conferencesRepositoryMock.Setup(c => c.DeleteAsync(conference)).Returns(Task.CompletedTask);

            var client = _factory.CreateClient();

            var result = await client.DeleteAsync($"api/conferences/{conferenceId}");

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact()]
        public async void Delete_AsAdminWithIncorrectConferenceId_Returns404NotFound()
        {
            var policyConfig = _factory.Services.GetRequiredService<FakePolicyConfig>();

            var conferenceId = 32;

            var conference = new Conference()
            {
                Id = conferenceId,
            };

            _conferencesRepositoryMock.Setup(c => c.GetByIdAsync(conferenceId)).ReturnsAsync((Conference)null);

            var client = _factory.CreateClient();

            var result = await client.DeleteAsync($"api/conferences/{conferenceId}");

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}