using MediatR;
using Microsoft.EntityFrameworkCore;
using GymScheduler.Entities.Entities;

namespace GymScheduler.Entities.CQRS.Commands;


public record CreateMemberCommand(Member Member) : IRequest;
public class CreateMemberCommandHandler(IDbContextFactory<AppDbContext> dbContextFactory) : IRequestHandler<CreateMemberCommand>
{
    public async Task Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        using var dbc = dbContextFactory.CreateDbContext();
        dbc.Add(request.Member);
        await dbc.SaveChangesAsync(cancellationToken);
    }
}
