using HellRoad.External;
using HellRoad.External.Animation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utility;

namespace HellRoad
{
    public class HaveBuffs : IGetHaveBuffsProperty
    {
        private Buff buff = null;
        private Func<PassiveSkillID, AddPassiveSkillBattleActionArgs> GetAddBuffArgs;
        private Func<PassiveSkillID, RemovePassiveSkillBattleActionArgs> GetRemoveBuffArgs;

        private Func<int> GetPartyIdx;

        private IPartyBuffView partyBuffView = null;
        private IAddGameAnimation addAnim = null;

        public HaveBuffs(Players players, Func<PassiveSkillID, AddPassiveSkillBattleActionArgs> GetAddBuffArgs, Func<PassiveSkillID, RemovePassiveSkillBattleActionArgs> GetRemoveBuffArgs, Func<int> GetPartyIdx)
        {
            this.GetAddBuffArgs = GetAddBuffArgs;
            this.GetRemoveBuffArgs = GetRemoveBuffArgs;
            this.GetPartyIdx = GetPartyIdx;

            addAnim = Locater<IAddGameAnimation>.Resolve();
            partyBuffView = Locater<IPartyBuffView>.Resolve((int)players);
        }

        public void OnSortParty(int idx_1, int idx_2)
        {
            if (buff == null) return;

            PassiveSkillInfo info = Locater<IGetPassiveSkillInfoFromDB>.Resolve().Get(buff.GetID());
            int duration = buff.GetDuration();
            string name = info.Name;

            addAnim.Add(new OriginalAnimation((anim) =>
            {
                partyBuffView.ShowBuff(idx_1, name, duration);

                partyBuffView.RemoveBuff(idx_1);
                partyBuffView.ShowBuff(idx_2, name, duration);
                anim.EndAnimation = true;
            }));
        }

        public void RemoveBuff()
        {
            if (buff == null) return;

            RemovePassiveSkillBattleActionArgs args = GetRemoveBuffArgs(buff.GetID());
            CheckPlay(When.OnRemoveBuff, Who.Me, args);
            buff.OnRemoveBuff(args);
            buff = null;

            {
                int partyIdx = GetPartyIdx();
                addAnim.Add(new OriginalAnimation((anim) =>
                {
                    partyBuffView.RemoveBuff(partyIdx);
                    anim.EndAnimation = true;
                }));
            }
            
        }

        public void AddBuff(Buff buff)
        {
            if (buff == null) return;

            RemoveBuff();

            {
                PassiveSkillInfo info = Locater<IGetPassiveSkillInfoFromDB>.Resolve().Get(buff.GetID());
                int partyIdx = GetPartyIdx();
                string name = info.Name;
                int duration = buff.GetDuration();
                addAnim.Add(new OriginalAnimation((anim) =>
                {
                    partyBuffView.ShowBuff(partyIdx, name, duration);
                    anim.EndAnimation = true;
                }));
            }

            this.buff = buff;
            AddPassiveSkillBattleActionArgs args = GetAddBuffArgs(buff.GetID());
            CheckPlay(When.OnAddSkill, Who.Me, args);
        }

        public void CheckPlay(When when, Who who, BattleActionArgs args)
        {
            if (buff == null) return;
            buff.CheckPlay(when, who, args);
        }

        public void TurnEnd()
        {
            if (buff == null) return;

            buff.TurnEnd();

            {
                int partyIdx = GetPartyIdx();
                int duration = buff.GetDuration();
                addAnim.Add(new OriginalAnimation((anim) =>
                {
                    partyBuffView.ValidateDuration(partyIdx, duration);
                    anim.EndAnimation = true;
                }));
            }
            
            
            if (buff.GetDuration() == 0)
            {
                RemoveBuff();
            }
        }

        ReadOnlyCollection<IGetBuffProperty> IGetHaveBuffsProperty.Get()
        {
            List<IGetBuffProperty> list = new List<IGetBuffProperty>();
            if (buff != null) list.Add(buff);
            return list.AsReadOnly();
        }
    }
}