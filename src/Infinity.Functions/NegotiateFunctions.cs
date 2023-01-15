using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Infinity.Functions;

public class NegotiateFunctions
{
    private readonly ILogger _logger;

    public NegotiateFunctions(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<NegotiateFunctions>();
    }

    [Function("Negotiate")]
    public SignalRConnectionInfo Negotiate(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "negotiate")] HttpRequestData req,
        [SignalRConnectionInfoInput(HubName = "component")] SignalRConnectionInfo connectionInfo,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("Negotiate");

        logger.LogInformation("HTTP trigger function processed a request to negotiate.");
        return connectionInfo;
    }
}
