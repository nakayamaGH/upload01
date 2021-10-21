using System;

namespace HellRoad
{
    public interface IChara : IBattleChara, IMapChara
    {
    }

    public interface IMapChara : IDamageable, IHealable, IDeadable, IStickParts, IGetCharaInfo, ILiveInParty
    {
    }

    public interface IBattleChara : IDamageable, IHealable, IDeadable, ICheckPassiveSkill, IOnTurnEvent, IDoActionByType, IGetCharaInfo, IGetStickedParts, IStickParts, ILiveInParty
    {
        void BaseAttack(PhysicOrMagic physicOrMagic);
        void Attack(PhysicOrMagic physicOrMagic, float magnification);
        void Attack(PhysicOrMagic dependence, PhysicOrMagic physicOrMagic, float magnification);
        void Attack(PhysicOrMagic physicOrMagic);
        void AttackedBy(PhysicOrMagic physicOrMagic, float magnification);
        void BaseAttack();

        void AddBuff(Buff buff);
        void AddBuff(PassiveSkillID id, int duration);

        void AddStatus(Status status);
        void ReduceStatus(Status status);
        void ResetAdditiveStatus();

        public event Func<Players, BattleActionArgs> GetBattleArgs;
        public event Func<IBattleChara> GetFrontEnemyChara;
        public event Action<IBattleChara> OnDeadAction;
        public event Action<long , StatusParam, IBattleChara> OnChangeParamAction;
    }

    public interface IDamageable
    {
        void Damage(long value);
    }

    public interface IHealable
    {
        void Heal(long value);
    }

    public interface IDeadable
    {
        bool IsAlive();
        void Dead();
    }

    public interface ICheckPassiveSkill
    {
        void CheckPassiveSkill(When when, Who who, BattleActionArgs args);
    }

    public interface IOnTurnEvent
    {
        void OnStartBattle();
        void OnTurnEnd();
        void OnEndBattle();
    }

    public interface IStickParts : IGetStickedParts
    {
        void StickParts(PartsID parts);
    }

    public interface ILiveInParty
    {
        Players Players { get; }

        int PartyIdx { get; }

        public event Action<int> OnSortedAction;
        public event Action OnAddPartyAction;

        public void OnSorted(int idx);
        public void OnAddParty();
    }
}