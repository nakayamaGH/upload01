using Utility;

namespace HellRoad.External.Animation
{
	public class ValidateStatusParamViewAnimation : IGameAnimation
	{
		public bool EndAnimation { get; set; } = false;

		private StatusParamType paramType;
		private long value;
		private Players players;

		public ValidateStatusParamViewAnimation(StatusParamType paramType, long value, Players players)
		{
			this.paramType = paramType;
			this.value = value;
			this.players = players;
		}

		public void DoAnimation()
		{
			ICharaStatusView statusView = Locater<ICharaStatusView>.Resolve((int)players);
			statusView.SetValue(paramType, value);
			EndAnimation = true;
		}
	}
}