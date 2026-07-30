using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging;

/// <summary>
/// Middleware for logging incoming HTTP requests.
/// Logs the HTTP method and request path for each request.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestLoggingMiddleware"/> class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the request pipeline.
    /// </param>
    /// <param name="logger">
    /// Logger used to record request information.
    /// </param>
    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Processes the incoming HTTP request and logs its details.
    /// </summary>
    /// <param name="context">
    /// The current HTTP context.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous middleware operation.
    /// </returns>
    public async Task Invoke(HttpContext context)
    {
        // Log the HTTP request method and request path
        _logger.LogInformation(
            "{Method} {Path}",
            context.Request.Method,
            context.Request.Path);

        // Pass the request to the next middleware component
        await _next(context);
    }
}