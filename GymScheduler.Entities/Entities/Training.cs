using GymScheduler.Entities.ValueObjects;

namespace GymScheduler.Entities.Entities;

public class Training : EntityBase
{
    public required TrainingId Id { get; init; }
    public required Duration Duration { get; init; }
    public required MemberId MemberId { get; init; }
}
