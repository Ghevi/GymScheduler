using GymScheduler.Entities.CQRS.Queries;
using Microsoft.AspNetCore.Components;
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

    public class CreateTrainingViewModel
    {
        public required MemberViewModel Member { get; init; }
        public DateTimeOffset? Start { get; set; }
        public List<TrainingViewModel> Trainings { get; set; } = [ new TrainingViewModel() ];
        public String TrainingsHeightInCm
        {
            get
            {
                var height = Trainings.Count switch
                {
                    <= 10 => 4,
                    > 10 => 3,
                };
                return $"{height}cm";
            }
        }
    }

    public class TrainingViewModel
    {
        public Boolean Started { get; set; }
        public String Training1 { get; set; } = String.Empty;
        public String Training2 { get; set; } = String.Empty;
        public String Training3 { get; set; } = String.Empty;
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
        };
        await InvokeAsync(StateHasChanged);
    }

    private async Task StartTraining()
    {
        _createTraining!.Trainings[0].Started = true;
        _createTraining.Trainings.Add(new TrainingViewModel());
        await InvokeAsync(StateHasChanged);
    }

    private async Task ShowPopupBlockedMessage()
    {
        await Js.InvokeVoidAsync("alert", "Popup is blocked!");
    }

    private async Task Print()
    {
        var fileName = "my file name";
        var cssFileLocations = new List<string>();
        var jsFileLocations = new List<string>();
        try
        {
            var base64Results = await BlazorPdf.GetBlazorInPdfBase64<Training>(
                x =>
                {
                    x.Add(y => y.Fullname, _member.Member.Fullname);
                    x.Add(y => y.Start, _createTraining.Start);
                    x.Add(y => y.Trainings, _createTraining.Trainings);
                },
                fileName, 
                cssFileLocations, 
                jsFileLocations);

        }
        catch (Exception ex)
        {

            throw;
        }
    }
}