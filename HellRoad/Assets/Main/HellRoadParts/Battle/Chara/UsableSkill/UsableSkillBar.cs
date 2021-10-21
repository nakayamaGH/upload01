using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HellRoad
{
    public class UsableSkillBar : IGetUsableSkillBarProperty
    {
        private List<UsableSkillSlot> skillSlots = new List<UsableSkillSlot>();

        public void ResetCD()
        {
            skillSlots.ForEach(x => x.ResetCD());
        }

        public void UseSkill(int idx, BattleActionArgs args)
        {
            skillSlots[idx].UseSkill(args);
        }

        public void AddPartsSkills(PartsInfo info)
        {
            AddRangeSkill(info.UsableSkills);
        }

        private void AddSkill(UsableSkillID id)
        {
            skillSlots.Add(new UsableSkillSlot(id));
        }

        private void AddRangeSkill(List<UsableSkillID> idList)
        {
            idList.ForEach(x => AddSkill(x));
        }

        private void RemoveSkill(UsableSkillID id)
        {
            UsableSkillSlot skill = skillSlots.Find(x => x.ID == id);
            if(skill != null)
                skillSlots.Remove(skill);
        }

        public void RemovePartsSkills(PartsInfo info)
        {
            RemoveRangeSkill(info.UsableSkills);
        }

        private void RemoveRangeSkill(List<UsableSkillID> idList)
        {
            idList.ForEach(x => RemoveSkill(x));
        }

        private void RemoveSkill(int idx)
        {
            skillSlots.RemoveAt(idx);
        }

        public bool ContainSkill(UsableSkillID id)
        {
            UsableSkillSlot skill = skillSlots.Find(x => x.ID == id);
            return skill != null;
        }

        public bool CanUseSkill(int idx)
        {
            return skillSlots[idx].CanUseSKill();
        }

        public int SkillCount()
        {
            return skillSlots.Count;
        }

        public UsableSkillID GetID(int idx)
        {
            return skillSlots[idx].ID;
        }

        public ReadOnlyCollection<UsableSkillID> GetAllID()
        {
            return skillSlots.Select(x => x.ID).ToList().AsReadOnly();
        }

        public ReadOnlyCollection<int> CanUseSkills()
        {
            List<int> usables = new List<int>();
            for (int i = 0; i < SkillCount(); i++)
                if (CanUseSkill(i))
                    usables.Add(i);
            return usables.AsReadOnly();
        }
    }
}