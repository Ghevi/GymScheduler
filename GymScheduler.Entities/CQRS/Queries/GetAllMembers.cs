using GymScheduler.Entities.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymScheduler.Entities.CQRS.Queries;

public record GetAllMembers : IRequest<IQueryable<MemberViewModel>>;
public record MemberViewModel(Fullname Fullname);

public class GetAllMembersHandler(IDbContextFactory<AppDbContext> dbContextFactory) : IRequestHandler<GetAllMembers, IQueryable<MemberViewModel>>
{
    public async Task<IQueryable<MemberViewModel>> Handle(GetAllMembers request, CancellationToken cancellationToken)
    {
        var dbc = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return dbc.Members.AsNoTracking().Select(x => new MemberViewModel(x.Fullname));
    }
}
