using BlazorComponentUtilities;
using Infinity.Web.Components.Base;
using Microsoft.AspNetCore.Components;

namespace Infinity.Web.Components;

public partial class TabItem : InfinityComponentBase
{
    [Parameter]
    public RenderFragment Header { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public RenderFragment? Content { get; set; }

    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public string Icon { get; set; }

    [Parameter]
    public bool Enabled { get; set; } = true;

    [CascadingParameter]
    protected internal InfinityBaseItemsControl<TabItem> Parent { get; set; }

    protected override void OnInitialized()
    {
        if (Parent is null)
        {
            throw new ArgumentNullException(nameof(Parent), "TabItem must exist within a TabControl");
        }

        Parent.AddItem(this);

        base.OnInitialized();
    }

    private void ActivateTab()
    {
        ((TabControl)Parent).ActivateTab(this);
    }

    private string ClassNames =>
        new CssBuilder("timeline")
            .AddClass("timeline-vertical")
            .AddClass("timeline-align-default")
            .AddClass("timeline-position-alternate")
            .Build();
}
