using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.PassiveSkills
{
    public class AddMagicalPower : AddStatusPassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.AddMagicalPower;
		public override When When => When.OnAddSkill;
		public override Who Who => Who.Me;

		public AddMagicalPower() : base(StatusParamType.MPow, 0.2f)
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