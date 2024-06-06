using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace Conferences.API.Controllers.Tests
{
    public class ConferencesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ConferencesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
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
    }
}