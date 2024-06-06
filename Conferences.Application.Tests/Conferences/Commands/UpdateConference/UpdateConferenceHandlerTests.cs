using AutoMapper;
using Conferences.Domain.Constants;
using Conferences.Domain.Entities;
using Conferences.Domain.Exceptions;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Conferences.Application.Conferences.Commands.UpdateConference.Tests
{
    public class UpdateConferenceHandlerTests
    {
        private Mock<ILogger<UpdateConferenceHandler>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IConferencesRepository> _conferencesRepositoryMock;
        private Mock<IConferenceAuthorizationService> _authorizationServiceMock;

        private UpdateConferenceHandler _handler;
        private int _conferenceId = 1;

        public UpdateConferenceHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateConferenceHandler>>();
            _mapperMock = new Mock<IMapper>();
            _conferencesRepositoryMock = new Mock<IConferencesRepository>();
            _authorizationServiceMock = new Mock<IConferenceAuthorizationService>();

            _handler = new UpdateConferenceHandler(_loggerMock.Object, _mapperMock.Object,
                _conferencesRepositoryMock.Object, _authorizationServiceMock.Object);
        }

        [Fact()]
        public async Task Handler_ForValidCommandAndAuthorizedUser_UpdatesConference()
        {
            var command = new UpdateConferenceCommand()
            {
                Id = _conferenceId,
            };

            var conference = new Conference()
            {
                Id = _conferenceId
            };

            _conferencesRepositoryMock.Setup(c => c.GetByIdAsync(_conferenceId)).ReturnsAsync(conference);

            _authorizationServiceMock.Setup(a => a.Authorize(conference, ResourceOperation.Update)).Returns(true);

            _mapperMock.Setup(m => m.Map(command, conference)).Returns(conference);

            await _handler.Handle(command, CancellationToken.None);

            _conferencesRepositoryMock.Verify(c => c.SaveChangesAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map(command, conference), Times.Once);
        }

        [Fact()]
        public async Task Handler_ForNotFoundConference_ThrowsNotFoundException()
        {
            var command = new UpdateConferenceCommand()
            {
                Id = _conferenceId,
            };

            _conferencesRepositoryMock.Setup(c => c.GetByIdAsync(1)).ReturnsAsync((Conference)null);

            Func<Task> func = async () => await _handler.Handle(command, CancellationToken.None);

            await func.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Conference with id: {_conferenceId} not found.");
        }

        [Fact()]
        public async Task Handler_ForUnauthorizedUser_ThrowsForbidException()
        {
            var command = new UpdateConferenceCommand()
            {
                Id = _conferenceId,
            };

            var conference = new Conference()
            {
                Id = _conferenceId
            };

            _conferencesRepositoryMock.Setup(c => c.GetByIdAsync(_conferenceId)).ReturnsAsync(conference);

            _authorizationServiceMock.Setup(a => a.Authorize(conference, ResourceOperation.Update)).Returns(false);

            Func<Task> func = async () => await _handler.Handle(command, CancellationToken.None);

            await func.Should().ThrowAsync<ForbidException>();
        }
    }
}