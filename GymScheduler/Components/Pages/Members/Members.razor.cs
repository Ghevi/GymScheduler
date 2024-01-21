using GymScheduler.Entities.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace GymScheduler.Components.Pages.Members;

public partial class Members
{
    [Inject] protected IMediator Mediator { get; set; } = null!;

    IQueryable<MemberViewModel>? _members;
    String _nameFilter = String.Empty;

    protected override async Task OnInitializedAsync()
    {
        var request = new GetAllMembersQuery();
        _members = await Mediator.Send(request);
        await base.OnInitializedAsync();
    }
}