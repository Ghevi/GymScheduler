using GymScheduler.Components.Shared.Interactables;
using GymScheduler.Entities.CQRS.Queries;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Diagnostics.Metrics;
using webenology.blazor.components.BlazorPdfComponent;

namespace GymScheduler.Components.Pages.Members.MemberDetails;
public partial class MemberDetails
{
    [Inject] protected IJSRuntime Js { get; set; } = null!;
    [Inject] protected IBlazorPdf BlazorPdf { get; set; } = null!;

    [Parameter] public Guid MemberId { get; set; }

    MemberDetailsViewModel? _member;
    CreateTrainingViewModel? _createTraining;
    readonly Edges _mainRowEdges = new(Left: false, Top: false, Right: false, Bottom: true);
    readonly Edges _leftCellEdges = new(Left: false, Top: false, Right: true, Bottom: false);
    public class CreateTrainingViewModel
    {
        public required MemberViewModel Member { get; init; }
        public DateTimeOffset? Start { get; set; }
        public List<TrainingViewModel> Trainings { get; set; } = [new TrainingViewModel()];
    }

    public class Exercise
    {
        public Stream Image { get; set; } = Stream.Null;
        public String Value { get; set; } = String.Empty;
    }

    public class TrainingViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Dictionary<Int32, Exercise> Exercises { get; } = new()
        {
            { 1, new() }
        };
    }

    protected async override Task OnInitializedAsync()
    {
        var request = new GetMemberDetailsQuery(new(MemberId));
        _member = await Mediator.Send(request);
        await base.OnInitializedAsync();
    }

    private async Task StartCreateTraining()
    {
        _createTraining = new CreateTrainingViewModel()
        {
            Member = _member!.Member with { },
            Trainings = Enumerable.Range(0, 5).Select(x => new TrainingViewModel() { }).ToList(),
        };
        await InvokeAsync(StateHasChanged);
        //await Js.InvokeVoidAsync("initializeInteract");
    }

    private async Task UploadFile(InputFileChangeEventArgs e, TrainingViewModel training, Int32 key)
    {
        training.Exercises[key].Image = e.File.OpenReadStream(Int64.MaxValue);

        if (training.Exercises.Count >= 3) return;

        training.Exercises.Add(key + 1, new());
        await InvokeAsync(StateHasChanged);
    }
}