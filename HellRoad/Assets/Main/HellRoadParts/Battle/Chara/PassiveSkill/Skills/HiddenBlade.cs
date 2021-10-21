using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.PassiveSkills
{
    public class HiddenBlade : PassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.HiddenBlade;
		public override When When => When.OnAttackedBy;
		public override Who Who => Who.Me;

		protected override void PlayAction(BattleActionArgs args)
		{
			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
			args.EnemyFrontChara.AddBuff(new Buff(PassiveSkillID.Poison, 3));
		}

		public override bool CanPlay(BattleActionArgs args)
		{
			AttackCharaBattleActionArgs atkArgs = args as AttackCharaBattleActionArgs;
			return atkArgs.PhysicOrMagic == PhysicOrMagic.Physic;
		}

		public override void OnRemoveSkill(RemovePassiveSkillBattleActionArgs args)
		{
		}
	}
}