using BlazorComponentUtilities;
using Infinity.Web.Components.Base;
using Microsoft.AspNetCore.Components;

namespace Infinity.Web.Components;

public partial class TabControl : InfinityBaseItemsControl<TabItem>
{
    [Parameter]
    public EventCallback<TabItem> OnTabChanged { get; set; }

    public TabItem ActiveTab { get; set; }

    internal void AddTab(TabItem tabPage)
    {
        Items.Add(tabPage);

        if (Items.Count == 1)
        {
            ActiveTab = tabPage;
        }

        StateHasChanged();
    }

    internal string GetButtonClass(TabItem tab)
    {
        if (!tab.Enabled)
        {
            return "tab-disabled";
        }

        return tab == ActiveTab ? "tab-active" : "tab-inactive";
    }

    internal void ActivateTab(TabItem tab)
    {
        if (tab.Enabled)
        {
            ActiveTab = tab;
            OnTabChanged.InvokeAsync(tab);
        }
    }

    internal string ClassNames =>
        new CssBuilder("tab-control")
            .Build();
}
