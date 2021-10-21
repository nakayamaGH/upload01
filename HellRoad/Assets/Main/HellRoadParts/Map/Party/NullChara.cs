using System;

namespace HellRoad
{
	public class NullChara : IChara
	{
		public Players Players => Players.NULL;

		public int PartyIdx { get; private set; }

		public event Func<Players, BattleActionArgs> GetBattleArgs;
		public event Func<IBattleChara> GetFrontEnemyChara;
		public event Action<IBattleChara> OnDeadAction;
		public event Action<PartsID, PartsID, PartsID, PartsID> ValidateWholeBody;
        public event Action<long, StatusParam, IBattleChara> OnChangeParamAction;
		public event Action<int> OnSortedAction;
		public event Action OnAddPartyAction;

		public NullChara(int partyIdx)
		{
			PartyIdx = partyIdx;
		}

		public void BaseAttack(PhysicOrMagic physicOrMagic)
		{
		}

		public void Attack(PhysicOrMagic physicOrMagic, float magnification)
		{

		}

		public void Attack(PhysicOrMagic physicOrMagic)
		{

		}

		public void BaseAttack()
		{

		}

		public void CheckPassiveSkill(When when, Who who, BattleActionArgs args)
		{

		}

		public void Damage(long value)
		{

		}

		public void Dead()
		{

		}

		public void DoAction(TurnActionType type)
		{

		}

		public long GetDefense(PhysicOrMagic physicOrMagic)
		{
			return 0;
		}

		public CharaInfo GetInfo()
		{
			return null;
		}

		public PartsID GetParts(PartsType type)
		{
			return PartsID.None;
		}

		public long GetPower(PhysicOrMagic physicOrMagic)
		{
			return 0;
		}

		public long GetValue(StatusParamType type)
		{
			return 0;
		}

		public void Heal(long value)
		{

		}

		public bool IsAlive()
		{
			return false;
		}

		public void OnAddParty()
		{
			throw new NotImplementedException();
		}

		public void OnEndBattle()
		{

		}

		public void OnSorted(int idx)
		{
			PartyIdx = idx;
		}

		public void OnStartBattle()
		{

		}

		public void OnTurnEnd()
		{

		}

		public void StickParts(PartsID parts)
		{

		}

		public void AttackedBy(PhysicOrMagic physicOrMagic, float magnification)
		{
		}

		public void Attack(PhysicOrMagic dependence, PhysicOrMagic physicOrMagic, float magnification)
		{
			throw new NotImplementedException();
		}

        public void AddBuff(Buff buff)
        {
        }

        public void AddBuff(PassiveSkillID id, int duration)
        {
        }

        public void AddStatus(Status status)
        {
        }

        public void ReduceStatus(Status status)
        {
        }

        public void ResetAdditiveStatus()
        {
        }
    }
}