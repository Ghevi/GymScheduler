using GymScheduler.Entities.Entities;

namespace GymScheduler.Components.Pages.Members;

public static class MemberExtensions
{
    public static IEnumerable<MemberViewModel> ToViewModel(IEnumerable<Member> members)
    {
        foreach (var member in members)
        {
            var vm = new MemberViewModel(member.Fullname with { });
            yield return vm;
        }
    }
}
