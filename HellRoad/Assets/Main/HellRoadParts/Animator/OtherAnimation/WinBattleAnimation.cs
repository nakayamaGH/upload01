using Utility;

namespace HellRoad.External.Animation
{
	public class WinBattleAnimation : IGameAnimation
	{
		public bool EndAnimation { get; set; } = false;

		void IGameAnimation.DoAnimation()
		{
			Locater<IBattleView>.Resolve().WinBattleAnimation(() =>
			{
				EndAnimation = true;
			});
		}
	}
}