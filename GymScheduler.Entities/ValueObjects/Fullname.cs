using System.ComponentModel.DataAnnotations.Schema;

namespace GymScheduler.Entities.ValueObjects;

[ComplexType]
public sealed record Fullname(string FirstName, string LastName) : NameBase
{
    public override String ToString()
    {
        return $"{FirstName} {LastName}";
    }
}