using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogWarning("Handled exception {Message}, time: {time}", exception.Message, DateTime.Now);

            (string Details, string Title, int StatusCode) = exception switch
            {
                TaskCanceledException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest
                ),

                OperationCanceledException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest
                ),

                TopicNotFoundException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound
                ),

                _ =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };
                
            var problemDetails = new ProblemDetails
            {
                Title = Title,
                Detail = Details,
                Status = StatusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            await httpContext
                    .Response
                    .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
