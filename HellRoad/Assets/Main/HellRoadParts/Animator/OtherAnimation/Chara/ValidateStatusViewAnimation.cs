using Utility;

namespace HellRoad.External.Animation
{
	public class ValidateStatusViewAnimation : IGameAnimation
	{
		public bool EndAnimation { get; set; } = false;

		private IGetParamValue getParamValue;
		private Players players;

		public ValidateStatusViewAnimation(IGetParamValue getParamValue, Players players)
		{
			this.getParamValue = getParamValue;
			this.players = players;
		}

		public void DoAnimation()
		{
			if (getParamValue == null)
			{
				EndAnimation = true;
				return;
			}
			ICharaStatusView statusView = Locater<ICharaStatusView>.Resolve((int)players);
			statusView.SetValues(getParamValue);
			EndAnimation = true;
		}
	}
}