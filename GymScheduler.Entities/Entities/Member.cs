using GymScheduler.Entities.ValueObjects;

namespace GymScheduler.Entities.Entities;

public class Member : EntityBase
{
    public MemberId Id { get; private set; } = null!;
    public Fullname Fullname { get; private set; } = null!;
    public ICollection<Training> Trainings { get; private set; } = [];

    private Member() { }

    public static Member CreateNew(Fullname fullname)
    {
        return new Member()
        {
            Id = new(Guid.NewGuid()),
            Fullname = fullname
        };
    }

    public static Member CreateNew(Fullname fullname, IEnumerable<Training> trainings)
    {
        return new Member()
        {
            Id = new(Guid.NewGuid()),
            Fullname = fullname,
            Trainings = trainings.ToArray()
        };
    }
}