using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.PassiveSkills
{
	public class Counter : PassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.Counter;
		public override When When => When.OnAttackedBy;
		public override Who Who => Who.Me;

        protected override void PlayAction(BattleActionArgs args)
		{
			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
			addAnim.Add(new PlaySEAnimation("Shakin_1"));
			addAnim.Add(new BattleCharaAnimation(args.MyFrontChara.Players, AnimName.Uppercut, 0.5f, 0.05f, true));
			addAnim.Add(new WeightAnimation(0.2f));
			args.EnemyFrontChara.Damage(5);
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