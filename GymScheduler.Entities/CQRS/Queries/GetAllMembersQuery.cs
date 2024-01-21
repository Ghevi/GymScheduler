using GymScheduler.Entities.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymScheduler.Entities.CQRS.Queries;

public record GetAllMembersQuery : IRequest<IQueryable<MemberViewModel>>;
public record MemberViewModel(MemberId Id, Fullname Fullname);

public class GetAllMembersQueryHandler(IDbContextFactory<AppDbContext> dbContextFactory) : IRequestHandler<GetAllMembersQuery, IQueryable<MemberViewModel>>
{
    public async Task<IQueryable<MemberViewModel>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        var dbc = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return dbc.Members.AsNoTracking().Select(x => new MemberViewModel(x.Id, x.Fullname));
    }
}
