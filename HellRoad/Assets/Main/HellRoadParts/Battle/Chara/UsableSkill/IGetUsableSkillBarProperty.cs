using System.Collections.ObjectModel;

namespace HellRoad
{
    public interface IGetUsableSkillBarProperty
    {
        UsableSkillID GetID(int idx);
        ReadOnlyCollection<UsableSkillID> GetAllID();
        ReadOnlyCollection<int> CanUseSkills();
    }
}