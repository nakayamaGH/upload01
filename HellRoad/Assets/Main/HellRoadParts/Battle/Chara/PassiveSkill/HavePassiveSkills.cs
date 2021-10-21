using Cysharp.Threading.Tasks;
using HellRoad.External;
using HellRoad.External.Animation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Utility;

namespace HellRoad
{
    public class HavePassiveSkills : IGetHavePassiveSkillsProperty
    {
        private List<PassiveSkill> skills = new List<PassiveSkill>();

        private Func<PassiveSkillID, AddPassiveSkillBattleActionArgs> OnAddSkillArgs;
        private Func<PassiveSkillID, RemovePassiveSkillBattleActionArgs> OnRemoveSkillArgs;

        private bool inBattle = false;

        public HavePassiveSkills(Func<PassiveSkillID, AddPassiveSkillBattleActionArgs> OnAddSkillArgs, Func<PassiveSkillID, RemovePassiveSkillBattleActionArgs> OnRemoveSkillArgs)
        {
            this.OnAddSkillArgs = OnAddSkillArgs;
            this.OnRemoveSkillArgs = OnRemoveSkillArgs;
        }

        public void CheckUseSkill(When when, Who who, BattleActionArgs args)
        {
            foreach(PassiveSkill skill in skills)
            {
                if(skill.CanPlay(when, who))
                {
                    skill.Play(args);
                }
            }
        }

        public void AddPartsSkills(PartsInfo info)
        {
            AddRangeSkill(info.PassiveSkills);
        }

        private void AddSkill(PassiveSkillID id)
        {
            PassiveSkill skill = Locater<IGetPassiveSkillFromDB>.Resolve().Get(id);
            if (inBattle)
            {
                AddPassiveSkillBattleActionArgs args = OnAddSkillArgs(id);
                CheckUseSkill(When.OnAddSkill, Who.Me, args);
            }
            skills.Add(skill);
        }

        private void AddRangeSkill(List<PassiveSkillID> idList)
        {
            foreach (PassiveSkillID id in idList)
            {
                AddSkill(id);
            }
        }

        public void RemovePartsSkills(PartsInfo info)
        {
            RemoveRangeSkill(info.PassiveSkills);
        }

        private void RemoveSkill(PassiveSkillID id)
        {
            PassiveSkill skill = skills.Find(x => x.ID == id);
            if (skill != null)
            {
                if (inBattle) skill.OnRemoveSkill(OnRemoveSkillArgs(id));
                skills.Remove(skill);
            }
        }

        private void RemoveRangeSkill(List<PassiveSkillID> idList)
        {
            foreach (PassiveSkillID id in idList)
            {
                RemoveSkill(id);
            }
        }

        public bool ContainSkill(PassiveSkillID id)
        {
            PassiveSkill skill = skills.Find(x => x.ID == id);
            return skill != null;
        }

        public int SkillCount()
        {
            return skills.Count;
        }

        public PassiveSkillID GetID(int idx)
        {
            return skills[idx].ID;
        }

        public ReadOnlyCollection<PassiveSkillID> GetAllID()
        {
            return skills.Select(x => x.ID).ToList().AsReadOnly();
        }

        public void InBattle(bool b)
        {
            inBattle = b;
        }
    }
}