using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.PassiveSkills
{
	public class Angry : PassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.Angry;
		public override When When => When.OnAttackedBy;
		public override Who Who => Who.Me;

        protected override void PlayAction(BattleActionArgs args)
		{
			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
			addAnim.Add(new WeightAnimation(0.3f));
			args.MyFrontChara.AddBuff(PassiveSkillID.AddMagicalPower, 3);
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