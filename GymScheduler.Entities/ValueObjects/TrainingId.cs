using System.ComponentModel.DataAnnotations.Schema;

namespace GymScheduler.Entities.ValueObjects;

[ComplexType]
public record TrainingId(int Value);