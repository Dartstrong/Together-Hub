using Application.Security.Exceptions;
using Application.Topics.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

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

                NotValidUsernameException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),

                NotValidPassException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized
                ),

                NotValidEmailException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),

                NotValidOrganizerException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
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
