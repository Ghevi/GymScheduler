using MediatR;
using Microsoft.AspNetCore.Components;

namespace GymScheduler.Components.Shared;

public class AppComponentBase : ComponentBase
{
    [Inject] protected IMediator Mediator { get; set; } = null!;
}
