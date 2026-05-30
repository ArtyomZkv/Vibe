using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (status, title) = exception switch
            {
                ValidationException => (400, "Ошибка валидации"),
                NotFoundException => (404, "Объект не найден"),
                ConflictException => (409, "Данные уже существуют"),
                _ => (500, "Внутренняя ошибка сервера")
            };

            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = exception is DomainException ? exception.Message : null
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            SendToLogs(_logger, exception, httpContext);

            return true;
        }
        private void SendToLogs(ILogger<GlobalExceptionHandler> logger, Exception exception, HttpContext httpContext)
        {
            if (exception is not DomainException)
                logger.LogError(exception, "Произошла непредвиденная ошибка для {Method} {Path}",
                                httpContext.Request.Method, httpContext.Request.Path);
            else
                logger.LogWarning(exception, exception.Message);
        }
    }
}
