using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad
{
	public class Scream : PassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.Scream;
		public override When When => When.OnAttackedBy;
		public override Who Who => Who.Me;

		protected override void PlayAction(BattleActionArgs args)
		{
			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
			addAnim.Add(new PlaySEAnimation("Woman_Scream"));
			args.EnemyFrontChara.AddBuff(PassiveSkillID.ReduceMagicalPower, 3);
		}

		public override bool CanPlay(BattleActionArgs args)
		{
			AttackCharaBattleActionArgs atkArgs = args as AttackCharaBattleActionArgs;
			return atkArgs.PhysicOrMagic == PhysicOrMagic.Magic;
		}

		public override void OnRemoveSkill(RemovePassiveSkillBattleActionArgs args)
		{
		}
	}
}