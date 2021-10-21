using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad
{
	public class ServiceOverTime : PassiveSkill
	{
		public override PassiveSkillID ID => PassiveSkillID.ServiceOvertime;
		public override When When => When.OnDead;
		public override Who Who => Who.Me;

        protected override void PlayAction(BattleActionArgs args)
		{
			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
			addAnim.Add(new PlayEffectToCharaAnimation(args.MyFrontChara.Players, EffectID.Bomb, UnityEngine.Vector2.zero));
			addAnim.Add(new PlaySEAnimation("Bomb_1"));
			args.EnemyFrontChara.Damage(10);
		}

        public override void OnRemoveSkill(RemovePassiveSkillBattleActionArgs args)
		{
		}
	}
}