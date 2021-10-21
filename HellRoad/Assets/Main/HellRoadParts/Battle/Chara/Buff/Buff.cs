using Utility;
using HellRoad.External;
using HellRoad.External.Animation;
using Cysharp.Threading.Tasks;
using System;

namespace HellRoad
{
    public class Buff : IGetBuffProperty
    {
        private readonly PassiveSkillID id;
        private int duration;

        private PassiveSkill passiveSkill;

        public Buff(PassiveSkillID id, int duration)
        {
            this.id = id;
            this.duration = duration;
            passiveSkill = Locater<IGetPassiveSkillFromDB>.Resolve().Get(id);
        }

        public void CheckPlay(When when, Who who, BattleActionArgs args)
        {
            if(passiveSkill.CanPlay(when, who))
            {
                passiveSkill.Play(args);
            }
        }

        public void TurnEnd()
        {
            duration--;
        }

        public PassiveSkillID GetID()
        {
            return id;
        }

        public int GetDuration()
        {
            return duration;
        }

        public void OnRemoveBuff(RemovePassiveSkillBattleActionArgs args)
        {
            passiveSkill.OnRemoveSkill(args);
        }
    }
}