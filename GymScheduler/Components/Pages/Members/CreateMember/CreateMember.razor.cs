using GymScheduler.Entities.CQRS.Commands;
using GymScheduler.Entities.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace GymScheduler.Components.Pages.Members.CreateMember;
public partial class CreateMember
{
    [Inject] protected IMediator Mediator { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    readonly MemberViewModel _member = new();

    async Task OnValidSubmit()
    {
        _member.IsSaving = true;
        var member = new Member()
        {
            Id = new(Guid.NewGuid()),
            Fullname = new(_member.FirstName, _member.LastName)
        };
        var request = new CreateMemberCommand(member);
        await Mediator.Send(request);
        await Task.Delay(2000);
        NavigationManager.NavigateTo("members");
    }
}