using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.PassiveSkills
{
    public abstract class AddStatusPassiveSkill : PassiveSkill
    {
		private long addParam = 0;

		protected StatusParamType ParamType { get; private set; }
		protected float Magnification { get; private set; }

		public AddStatusPassiveSkill(StatusParamType paramType, float magnification)
        {
			this.ParamType = paramType;
			this.Magnification = magnification;
        }

		protected override void PlayAction(BattleActionArgs args)
		{
			Status status = new Status();
			addParam = (long)(args.MyFrontChara.GetValue(ParamType) * Magnification);
			status.AddValue(ParamType, addParam);
			args.MyFrontChara.AddStatus(status);
			AddAnimation(args.MyFrontChara.Players);
		}

		public override void OnRemoveSkill(RemovePassiveSkillBattleActionArgs args)
		{
			Status status = new Status();
			status.SubValue(ParamType, addParam);
			args.MyFrontChara.AddStatus(status);
		}

		private void AddAnimation(Players players)
		{
			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
			EffectID id = EffectID.PowerUp;
			if (Magnification < 0)
			{
				id = EffectID.PowerDown;
			}
			addAnim.Add(new PlayEffectToCharaAnimation(players, id, UnityEngine.Vector2.zero));
			addAnim.Add(new WeightAnimation(0.35f));
		}
	}
}