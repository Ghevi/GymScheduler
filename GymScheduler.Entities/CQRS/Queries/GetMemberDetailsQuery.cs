using GymScheduler.Entities.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymScheduler.Entities.CQRS.Queries;

public record GetMemberDetailsQuery(MemberId Id) : IRequest<MemberDetailsViewModel>;
public record MemberDetailsViewModel(MemberViewModel Member, IQueryable<MemberTrainingViewModel> Trainings);
public record MemberTrainingViewModel(String Name, Duration Duration);
public class GetMemberDetailsQueryHandler(IDbContextFactory<AppDbContext> dbContextFactory) : IRequestHandler<GetMemberDetailsQuery, MemberDetailsViewModel>
{
    public async Task<MemberDetailsViewModel> Handle(GetMemberDetailsQuery request, CancellationToken cancellationToken)
    {
        var dbc = dbContextFactory.CreateDbContext();
        var member = await dbc.Members
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new MemberViewModel(x.Id, x.Fullname))
            .SingleAsync(cancellationToken);
        var trainings = dbc.Trainings
            .AsNoTracking()
            .Where(x => x.MemberId == member.Id)
            .Select(x => new MemberTrainingViewModel("abc", x.Duration));
        return new(member, trainings);
    }
}
