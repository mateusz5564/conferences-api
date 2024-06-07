using Conferences.Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Conferences.API.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsync_Always_ShouldCallNextDelegate()
        {
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var nextMock = new Mock<RequestDelegate>();

            await middleware.InvokeAsync(context, nextMock.Object);

            nextMock.Verify(n => n.Invoke(context), Times.Once);
        } 
        
        [Fact()]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldReturnStatusCode404()
        {
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var nextMock = new Mock<RequestDelegate>();

            var notFoundExpection = new NotFoundException("Conference", "1");

            nextMock.Setup(n => n.Invoke(context)).Throws(notFoundExpection);

            await middleware.InvokeAsync(context, nextMock.Object);

            context.Response.StatusCode.Should().Be(404);
        }     
        
        [Fact()]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldReturnStatusCode403()
        {
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var nextMock = new Mock<RequestDelegate>();

            var forbidException = new ForbidException();

            nextMock.Setup(n => n.Invoke(context)).Throws(forbidException);

            await middleware.InvokeAsync(context, nextMock.Object);

            context.Response.StatusCode.Should().Be(403);
        }

        [Fact()]
        public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldReturnStatusCode500()
        {
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var nextMock = new Mock<RequestDelegate>();

            var exception = new Exception();

            nextMock.Setup(n => n.Invoke(context)).Throws(exception);

            await middleware.InvokeAsync(context, nextMock.Object);

            context.Response.StatusCode.Should().Be(500);
        }
    }
}