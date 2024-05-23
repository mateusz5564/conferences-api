
using System.Diagnostics;

namespace Conferences.API.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await next.Invoke(context);
            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            if (elapsedMilliseconds > 4000)
            {
                logger.LogWarning("HTTP {Verb} {Path} executed in {Time} milliseconds.",
                    context.Request.Method,
                    context.Request.Path,
                    elapsedMilliseconds);
            }
        }
    }
}
