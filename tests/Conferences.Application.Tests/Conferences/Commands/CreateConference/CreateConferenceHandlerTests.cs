using AutoMapper;
using Conferences.Application.User;
using Conferences.Domain.Entities;
using Conferences.Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Conferences.Application.Conferences.Commands.CreateConference.Tests
{
    public class CreateConferenceHandlerTests
    {
        [Fact()]
        public async Task Hanlder_ForValidCommand_ReturnsCreatedConferenceId()
        {
            var loggerMock = new Mock<ILogger<CreateConferenceHandler>>();
            var conferencesRepository = new Mock<IConferencesRepository>();
            var mapperMock = new Mock<IMapper>();
            var userContextMock = new Mock<IUserContext>();

            var currentUser = new CurrentUser("ownerId", "test@interia.pl", []);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var command = new CreateConferenceCommand();
            var conference = new Conference();

            mapperMock.Setup(m => m.Map<Conference>(command)).Returns(conference);

            conferencesRepository.Setup(c => c.CreateAsync(conference)).ReturnsAsync(1);

            var handler = new CreateConferenceHandler(loggerMock.Object, mapperMock.Object,
                conferencesRepository.Object, userContextMock.Object);

            var id = await handler.Handle(command, CancellationToken.None);

            id.Should().Be(1);
            conference.OwnerId.Should().Be("ownerId");
        }
    }
}