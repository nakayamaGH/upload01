using HellRoad.External.Animation;
using Utility;

namespace HellRoad
{
    public class Temptation : PassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.Temptation;
		public override When When => When.OnAttackedBy;
		public override Who Who => Who.Me;

		protected override void PlayAction(BattleActionArgs args)
		{
			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
		}

		public override bool CanPlay(BattleActionArgs args)
		{
			return false;
		}

		public override void OnRemoveSkill(RemovePassiveSkillBattleActionArgs args)
		{
		}
	}
}