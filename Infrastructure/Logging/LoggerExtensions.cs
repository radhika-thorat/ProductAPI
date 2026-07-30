using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Infrastructure.Logging;

/// <summary>
/// Provides extension methods for configuring Serilog logging.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Configures Serilog for the application.
    /// Registers Console and File sinks and integrates Serilog
    /// with the ASP.NET Core logging pipeline.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="WebApplicationBuilder"/> instance.
    /// </param>
    /// <returns>
    /// The configured <see cref="WebApplicationBuilder"/> instance.
    /// </returns>
    public static WebApplicationBuilder ConfigureSerilog(
        this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            // Write logs to the console
            .WriteTo.Console()

            // Write logs to a rolling file (daily)
            .WriteTo.File(
                "Logs/log-.txt",
                rollingInterval: RollingInterval.Day)

            .CreateLogger();

        // Register Serilog as the application's logging provider
        builder.Host.UseSerilog();

        return builder;
    }
}