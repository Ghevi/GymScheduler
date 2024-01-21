using GymScheduler.Entities.CQRS.Queries;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GymScheduler.Components.Pages.Members.MemberDetails;
public partial class MemberDetails
{
    [Inject] protected IJSRuntime Js { get; set; } = null!;

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
}