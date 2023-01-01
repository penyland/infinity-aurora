using Microsoft.ApplicationInsights.Extensibility;

namespace Infinity.Web.Extensions;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddLoggingServices(this WebApplicationBuilder builder)
    {
        if (builder.Configuration.GetValue<bool>("UseApplicationInsights") == true)
        {
            builder.Services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
            });

            // Adding the filter below to ensure logs of all severity from Program.cs is sent to ApplicationInsights.
            builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(typeof(Program).FullName, LogLevel.Trace);

            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Environment", context.HostingEnvironment)
                    .WriteTo
                        .ApplicationInsights(
                            services.GetRequiredService<TelemetryConfiguration>(),
                            TelemetryConverter.Traces);
            });
        }
        else
        {
            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration);
            });
        }

        return builder;
    }
}
