using GymScheduler.Entities.Entities;
using GymScheduler.Queries.Members;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace GymScheduler.Components.Pages.Members;

public partial class Members
{
    [Inject] protected IMediator Mediator { get; set; } = null!;

    private IQueryable<Member>? _members;

    protected override async Task OnInitializedAsync()
    {
        var request = new GetAllMembers();
        _members = await Mediator.Send(request);

        await base.OnInitializedAsync();
    }
}