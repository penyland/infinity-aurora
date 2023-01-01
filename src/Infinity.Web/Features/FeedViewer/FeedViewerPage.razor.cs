using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Infinity.Web.Features.FeedViewer;

public partial class FeedViewerPage
{
    public string Body => """
        {
            "Message": "Test"
        }
        """;

    [Inject]
    private IJSRuntime JsRuntime { get; set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JsRuntime.InvokeVoidAsync("Prism.highlightAll");
    }
}
