using Microsoft.AspNetCore.Components;

namespace Infinity.Web.Components.Base;
public abstract class InfinityBaseItemsControl<T> : InfinityComponentBase
    where T : InfinityComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    public List<T> Items { get; } = new List<T>();

    public virtual void AddItem(T item) { }
}
