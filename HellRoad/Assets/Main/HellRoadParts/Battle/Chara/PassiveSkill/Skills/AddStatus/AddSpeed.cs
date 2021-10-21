using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.PassiveSkills
{
    public class AddSpeed : AddStatusPassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.AddSpeed;
		public override When When => When.OnAddSkill;
		public override Who Who => Who.Me;

		public AddSpeed() : base(StatusParamType.Speed, 0.2f)
		{
		}

		protected override void PlayAction(BattleActionArgs args)
		{
			base.PlayAction(args);
		}

		public override void OnRemoveSkill(RemovePassiveSkillBattleActionArgs args)
		{
			base.OnRemoveSkill(args);
		}
	}
}