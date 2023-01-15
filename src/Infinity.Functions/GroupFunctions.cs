using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Infinity.Functions;

public class GroupFunctions
{
    private readonly ILogger logger;

    public GroupFunctions(ILoggerFactory loggerFactory)
    {
        logger = loggerFactory.CreateLogger("Group");
    }

    [Function("AddToGroup")]
    [SignalROutput(HubName = "Hub")]
    public SignalRGroupAction AddToGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "addtogroup/{groupName}/{connectionId}")] HttpRequestData req,
            string groupName,
            string connectionId)
    {
        logger.LogInformation($"HTTP trigger function processed a request to add to a group {groupName}.");

        var addToGroupAction = new SignalRGroupAction(SignalRGroupActionType.Add)
        {
            ConnectionId = connectionId,
            GroupName = groupName
        };

        return addToGroupAction;
    }
}

public class MyConnectionInfo
{
    public string Url { get; set; }

    public string AccessToken { get; set; }
}
