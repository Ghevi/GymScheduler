using System.ComponentModel.DataAnnotations.Schema;

namespace GymScheduler.Entities.ValueObjects;

[ComplexType]
public record Duration
{
    public required Start Start { get; init; }
    public required End End { get; init; }
}
