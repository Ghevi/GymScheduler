using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GymScheduler.Components.Shared.Interactables;
public partial class InteractableRow
{
    [Inject] private IJSRuntime Js { get; set; } = null!;

    [Parameter] public String Height { get; set; } = String.Empty;
    [Parameter] public Edges? Edges { get; set; }
    [Parameter] public Edges? LeftCellEdges { get; set; }
    [Parameter] public RenderFragment? LeftCell { get; set; }
    [Parameter] public RenderFragment? RightCell { get; set; }
    [Parameter] public String Id { get; set; } = String.Empty;

    String LeftCellId => $"{Id}-{nameof(LeftCell)}";
    String RightCellId => $"{Id}-{nameof(RightCell)}";

    protected override async Task OnAfterRenderAsync(Boolean firstRender)
    {
        if (firstRender)
        {
            if (Edges is not null)
            {
                var module = await Js.InvokeAsync<IJSObjectReference>("import", "./js/bundle.js");
                await module.InvokeVoidAsync("resize", Id, Edges);

                if (LeftCellEdges is not null)
                {
                    await module.InvokeVoidAsync("resize", LeftCellId, LeftCellEdges);
                }
            }
        }
        await base.OnAfterRenderAsync(firstRender);
    }
}