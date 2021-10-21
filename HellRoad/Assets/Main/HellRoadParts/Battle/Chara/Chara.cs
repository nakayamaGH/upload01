using HellRoad.External.Animation;
using System;
using Utility;

namespace HellRoad
{
    public class Chara : IChara
    {
        private Status status = new Status(true, false);
        private WholeBody wholeBody;
        private UsableSkillBar usableSkillBar = new UsableSkillBar();
        private HavePassiveSkills passiveSkills;
        private HaveBuffs buffs;

        private bool isAlive = true;

        public event Func<IBattleChara> GetFrontEnemyChara;
        public event Func<Players, BattleActionArgs> GetBattleArgs;
        public event Action<IBattleChara> OnDeadAction;
        public event Action<PartsID, PartsID, PartsID, PartsID> ValidateWholeBodyAction;
        public event Action<long, StatusParam, IBattleChara> OnChangeParamAction;
		public event Action<int> OnSortedAction;

		private IAddGameAnimation addAnim;
        private Status additiveStatus = new Status();

        public Players Players { get; private set; }
        public event Action OnAddPartyAction;

		public int PartyIdx { get; private set; }

		public Chara(Players players, int idx, Status additionalStatus, PartsID head, PartsID body, PartsID arms, PartsID legs)
        {
            Players = players;
            PartyIdx = idx;

            wholeBody = new WholeBody();

            addAnim = Locater<IAddGameAnimation>.Resolve();

            passiveSkills = new HavePassiveSkills((id) => new AddPassiveSkillBattleActionArgs(getBattleArgs(), id), (id) => new RemovePassiveSkillBattleActionArgs(getBattleArgs(), id));
            buffs = new HaveBuffs(Players, (buff) => new AddPassiveSkillBattleActionArgs(getBattleArgs(), buff), (id) => new RemovePassiveSkillBattleActionArgs(getBattleArgs(), id), () => PartyIdx);

            //ステータスパラメータが変更されたとき
            status.OnChangeParam += (beforeValue, param) =>
            {
                OnChangeParamAction?.Invoke(beforeValue, param, this);
            };

            //パーティ参加時
            OnAddPartyAction += () =>
            {
                StickParts(head);
                StickParts(body);
                StickParts(arms);
                StickParts(legs);
                if (additionalStatus != null)
                    status += additionalStatus;
                status.SetValue(StatusParamType.HP, status.GetValue(StatusParamType.MaxHP));
            };
        }
        public Chara(Players players, int idx, CharaTemplate template) : this(players, idx,template.AdditionalStatus, template.Head, template.Body, template.Arms, template.Legs){}
        public Chara(Players players, int idx) : this(players, idx, null, PartsID.Skull_Head, PartsID.Skeleton_Body, PartsID.Skeleton_Arms, PartsID.Skeleton_Legs){}
        //public Chara(Players players, int idx) : this(players, idx, null, PartsID.Oni_Head, PartsID.Oni_Body, PartsID.Oni_Arms, PartsID.Oni_Legs){}

        void IOnTurnEvent.OnStartBattle()
        {
            passiveSkills.InBattle(true);
        }

        void IOnTurnEvent.OnTurnEnd()
        {
            CheckPassiveSkill(When.OnTurnEnd, getBattleArgs());
            buffs.TurnEnd();
        }

        void IOnTurnEvent.OnEndBattle()
        {
            buffs.RemoveBuff();
            passiveSkills.InBattle(false);
            usableSkillBar.ResetCD();
        }

        void IBattleChara.BaseAttack(PhysicOrMagic physicOrMagic)
        {
            addAnim.Add(BaseBattleCharaAnimationDatas.BeforeAttackAnimations(Players, physicOrMagic));
            ((IBattleChara)this).Attack(physicOrMagic);
            addAnim.Add(BaseBattleCharaAnimationDatas.AfterAttackAnimations(Players, physicOrMagic));
        }

        void IBattleChara.Attack(PhysicOrMagic dependence, PhysicOrMagic physicOrMagic, float magnification)
        {
            IBattleChara enemy = GetFrontEnemyChara();
            long atk = (long)(GetPower(dependence) * magnification);
            long def = enemy.GetDefense(dependence);
            long value = atk - def;
            if (value < 0) value = 0;

            CheckPassiveSkill(When.OnAttackTo, new AttackCharaBattleActionArgs(getBattleArgs(), physicOrMagic, magnification));
            enemy.AttackedBy(physicOrMagic, magnification);
            enemy.Damage(value);
        }

        void IBattleChara.Attack(PhysicOrMagic physicOrMagic, float magnification)
        {
            ((IBattleChara)this).Attack(physicOrMagic, physicOrMagic, magnification);
        }

        void IBattleChara.Attack(PhysicOrMagic physicOrMagic)
        {
            ((IBattleChara)this).Attack(physicOrMagic, 1);
        }

        void IBattleChara.AttackedBy(PhysicOrMagic physicOrMagic, float magnification)
        {
            CheckPassiveSkill(When.OnAttackedBy, new AttackCharaBattleActionArgs(getBattleArgs(), physicOrMagic, magnification));
        }

        void IBattleChara.BaseAttack()
        {
            IBattleChara enemy = GetFrontEnemyChara();

            long patk = this.GetPower(PhysicOrMagic.Physic);
            long pdef = enemy.GetDefense(PhysicOrMagic.Physic);
            long pvalue = patk - pdef;

            long matk = this.GetPower(PhysicOrMagic.Magic);
            long mdef = enemy.GetDefense(PhysicOrMagic.Magic);
            long mvalue = matk - mdef;

            if (pvalue > mvalue)
                ((IBattleChara)this).BaseAttack(PhysicOrMagic.Physic);
            else
                ((IBattleChara)this).BaseAttack(PhysicOrMagic.Magic);
        }

        public void AddBuff(Buff buff)
        {
            buffs.AddBuff(buff);
        }

        public void AddBuff(PassiveSkillID id, int duration)
        {
            buffs.AddBuff(new Buff(id, duration));
        }

        public void AddStatus(Status status)
        {
            this.status += status;
            additiveStatus += status;
        }

        public void ReduceStatus(Status status)
        {
            this.status -= status;
            additiveStatus -= status;
        }

        public void ResetAdditiveStatus()
        {
            status -= additiveStatus;
            additiveStatus = new Status();
        }

        public void Damage(long value)
        {
            if (!isAlive) return;

            status.SubValue(StatusParamType.HP, value);
            long hp = status.GetValue(StatusParamType.HP);
            addAnim.Add(BaseBattleCharaAnimationDatas.DamageEffectAnimation(Players, value));
            if (hp > 0)
            {
                addAnim.Add(BaseBattleCharaAnimationDatas.DamageAnimation(Players, value));
            }
            CheckPassiveSkill(When.OnDamage, new DamagedCharaBattleActionArgs(getBattleArgs(), value));
            if (hp <= 0)
            {
                Dead();
            }
        }

        public void Heal(long value)
        {
            long hp = status.GetValue(StatusParamType.HP);
            long maxHP = status.GetValue(StatusParamType.MaxHP);
            long result;
            if (hp < maxHP)
            {
                CheckPassiveSkill(When.OnHealed, new HealedCharaBattleActionArgs(getBattleArgs(), value));
                result = maxHP - hp;
                if(value < result)
                {
                    result = value;
                }
                addAnim.Add(BaseBattleCharaAnimationDatas.HealAnimation(Players));
                status.AddValue(StatusParamType.HP, result);
            }
        }

        public void Dead()
        {
            if (!isAlive) return;

            CheckPassiveSkill(When.OnDead, getBattleArgs());
            buffs.RemoveBuff();
            isAlive = false;
            OnDeadAction?.Invoke(this);
        }

        public bool IsAlive()
        {
            return isAlive;
        }

        void IDoActionByType.DoAction(TurnActionType type)
        {
            switch(type)
            {
                case TurnActionType.Attack:
                    ((IBattleChara)this).BaseAttack();
                    break;
            }
            if(type >= TurnActionType.UseSkill_0 && type <= TurnActionType.UseSkill_7)
            {
                BattleActionArgs args = getBattleArgs();
                int idx = (int)type - (int)TurnActionType.UseSkill_0;
                usableSkillBar.UseSkill(idx, args);
            }
        }

        public void CheckPassiveSkill(When when, Who who, BattleActionArgs args)
        {
            passiveSkills.CheckUseSkill(when, who, args);
            buffs.CheckPlay(when, who, args);
        }

        private void CheckPassiveSkill(When when, BattleActionArgs args)
        {
            CheckPassiveSkill(when, Who.Me, args);
            GetFrontEnemyChara?.Invoke().CheckPassiveSkill(when, Who.Enemy, args);
        }

        public long GetValue(StatusParamType type)          => status.GetValue(type);
        public long GetPower(PhysicOrMagic physicOrMagic)   => status.GetPower(physicOrMagic);
        public long GetDefense(PhysicOrMagic physicOrMagic) => status.GetDefense(physicOrMagic);

        public CharaInfo GetInfo()
        {
            return new CharaInfo(status, wholeBody, passiveSkills, usableSkillBar, buffs);
        }

        public void StickParts(PartsID parts)
        {
            IGetPartsInfoFromDB getParts = Locater<IGetPartsInfoFromDB>.Resolve();

            PartsID beforeParts = wholeBody.GetParts(parts.ToPartsType());
            float beforeHealthPer;

            if(beforeParts != PartsID.None)
            {
                beforeHealthPer = (float)status.GetValue(StatusParamType.HP) / status.GetValue(StatusParamType.MaxHP);
                PartsInfo beforeInfo = getParts.Get(beforeParts);
                status -= beforeInfo.Status;
                usableSkillBar.RemovePartsSkills(beforeInfo);
                passiveSkills.RemovePartsSkills(beforeInfo);
            }
            else
            {
                beforeHealthPer = 1;
            }
            
            wholeBody.Stick(parts);
            ValidateWholeBodyAction?.Invoke(wholeBody.Head, wholeBody.Body, wholeBody.Arms, wholeBody.Legs);

            PartsInfo afterInfo = getParts.Get(parts);
            status += afterInfo.Status;
            float nowHealth = status.GetValue(StatusParamType.MaxHP) * beforeHealthPer;
            status.SetValue(StatusParamType.HP, (long)Math.Floor(nowHealth));
            usableSkillBar.AddPartsSkills(afterInfo);
            passiveSkills.AddPartsSkills(afterInfo);
        }

        PartsID IGetStickedParts.GetParts(PartsType type)
        {
            return wholeBody.GetParts(type);
        }

		void ILiveInParty.OnSorted(int idx)
		{
            buffs.OnSortParty(PartyIdx, idx);
            PartyIdx = idx;
            OnSortedAction?.Invoke(idx);
		}

		void ILiveInParty.OnAddParty()
		{
            OnAddPartyAction?.Invoke();
		}

        private BattleActionArgs getBattleArgs() => GetBattleArgs(Players);
    }
}