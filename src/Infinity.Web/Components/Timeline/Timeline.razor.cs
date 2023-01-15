using BlazorComponentUtilities;
using Infinity.Web.Components.Base;
using Microsoft.AspNetCore.Components;

namespace Infinity.Web.Components;
public partial class Timeline : InfinityBaseItemsControl<TimelineItem>
{
    [Parameter]
    public TimelineOrientation Orientation { get; set; } = TimelineOrientation.Vertical;

    protected string ClassNames =>
        new CssBuilder("timeline")
            .AddClass("timeline-vertical", Orientation == TimelineOrientation.Vertical)
            .AddClass("timeline-align-default")
            .AddClass("timeline-position-alternate")
            .Build();
}
