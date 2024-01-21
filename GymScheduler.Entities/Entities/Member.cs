using GymScheduler.Entities.ValueObjects;

namespace GymScheduler.Entities.Entities;

public class Member : EntityBase
{
    public required MemberId Id { get; init; }
    public required Fullname Fullname { get; init; } = null!;
    public ICollection<Training> Trainings { get; init; } = [];
}