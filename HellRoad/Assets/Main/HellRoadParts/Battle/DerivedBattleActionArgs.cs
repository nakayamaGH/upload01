namespace HellRoad
{
	public class DamagedCharaBattleActionArgs : BattleActionArgs
	{
		public readonly long Damage;

		public DamagedCharaBattleActionArgs(BattleActionArgs args, long damage) : base(args.MyField, args.EnemyField)
		{
			Damage = damage;
		}
	}

	public class HealedCharaBattleActionArgs : BattleActionArgs
	{
		public readonly long Point;

		public HealedCharaBattleActionArgs(BattleActionArgs args, long point) : base(args.MyField, args.EnemyField)
		{
			Point = point;
		}
	}

	public class AttackCharaBattleActionArgs : BattleActionArgs
	{
		public readonly PhysicOrMagic PhysicOrMagic;
		public readonly float Magnification;

		public AttackCharaBattleActionArgs(BattleActionArgs args, PhysicOrMagic physicOrMagic, float magnification) : base(args.MyField, args.EnemyField)
		{
			PhysicOrMagic = physicOrMagic;
			Magnification = magnification;
		}
	}

	public class AddPassiveSkillBattleActionArgs : BattleActionArgs
    {
		public readonly PassiveSkillID id;

		public AddPassiveSkillBattleActionArgs(BattleActionArgs args, PassiveSkillID id) : base(args.MyField, args.EnemyField)
		{
			this.id = id;
		}
	}

	public class RemovePassiveSkillBattleActionArgs : BattleActionArgs
	{
		public readonly PassiveSkillID id;

		public RemovePassiveSkillBattleActionArgs(BattleActionArgs args, PassiveSkillID id) : base(args.MyField, args.EnemyField)
		{
			this.id = id;
		}
	}
}