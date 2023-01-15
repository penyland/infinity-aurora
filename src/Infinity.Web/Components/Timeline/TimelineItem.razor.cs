using BlazorComponentUtilities;
using Infinity.Web.Components.Base;
using Microsoft.AspNetCore.Components;

namespace Infinity.Web.Components;

public partial class TimelineItem : InfinityComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public RenderFragment IconContent { get; set; }

    [CascadingParameter]
    protected internal InfinityBaseItemsControl<TimelineItem> Parent { get; set; }

    protected override Task OnInitializedAsync()
    {
        Logger.LogInformation("Adding TimelineItem");
        Parent?.AddItem(this);
        return base.OnInitializedAsync();
    }

    protected string ClassNames =>
    new CssBuilder("timeline-item")
        .AddClass("timeline-item-default")
        .Build();
}
