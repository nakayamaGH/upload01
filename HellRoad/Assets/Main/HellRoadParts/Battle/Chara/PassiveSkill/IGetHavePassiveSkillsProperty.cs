using System.Collections.ObjectModel;

namespace HellRoad
{
    public interface IGetHavePassiveSkillsProperty
    {
        PassiveSkillID GetID(int idx);
        ReadOnlyCollection<PassiveSkillID> GetAllID();
    }
}