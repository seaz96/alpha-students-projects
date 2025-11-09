using Serilog;
using Serilog.Events;

namespace StudentProjects.API.Utility;

public static class SerilogHelper
{
    public static void GetConfiguration(this LoggerConfiguration loggerConfiguration)
    {
        const string logFormat =
            "[{Timestamp:yyyy.MM.dd HH:mm:ss:ms}] [{Level}] [T-{TraceId}] {Message}{NewLine}{Exception}";

        loggerConfiguration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .MinimumLevel.Is(LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Async(option => { option.Console(LogEventLevel.Information, logFormat); });
    }
}