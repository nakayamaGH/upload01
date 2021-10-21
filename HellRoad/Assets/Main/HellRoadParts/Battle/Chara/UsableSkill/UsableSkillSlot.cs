using Utility;

namespace HellRoad
{
    public class UsableSkillSlot
    {
        public readonly UsableSkillID ID;
        private int CD = 0;
        private UsableSkill skill = null;

        public UsableSkillSlot(UsableSkillID skillID)
        {
            ID = skillID;
            skill = Locater<IGetUsableSkillFromDB>.Resolve().Get(skillID);
        }

        public bool CanUseSKill()
        {
            return CD <= 0;
        }

        public void UseSkill(BattleActionArgs args)
        {
            skill.Play(args);
            CD = skill.CD + 1;
        }

        public void SubtractCD()
        {
            CD--;
        }

        public void ResetCD()
        {
            CD = 0;
        }
    }
}