using GymScheduler.Entities.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymScheduler.Entities.CQRS.Queries;

public record GetAllMembers : IRequest<IQueryable<Member>>;
public class GetAllMembersHandler(IDbContextFactory<AppDbContext> dbContextFactory) : IRequestHandler<GetAllMembers, IQueryable<Member>>
{
    public async Task<IQueryable<Member>> Handle(GetAllMembers request, CancellationToken cancellationToken)
    {
        var dbc = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return dbc.Members.AsNoTracking();
    }
}
