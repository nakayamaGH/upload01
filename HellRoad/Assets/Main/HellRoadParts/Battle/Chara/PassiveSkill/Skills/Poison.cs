using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.PassiveSkills
{
    public class Poison : PassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.Poison;
		public override When When => When.OnTurnEnd;
		public override Who Who => Who.Me;

		protected override void PlayAction(BattleActionArgs args)
		{
			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
			args.MyFrontChara.Damage(5);
		}

		public override void OnRemoveSkill(RemovePassiveSkillBattleActionArgs args)
		{
		}
	}
}