using Microsoft.AspNetCore.Components;

namespace Infinity.Web.Components.Base;
public class InfinityComponentBase : ComponentBase
{
    [Inject]
    private ILoggerFactory LoggerFactory { get; set; }

    private ILogger logger;

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public string Style { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

    protected ILogger Logger => logger ??= LoggerFactory.CreateLogger(GetType());
}
