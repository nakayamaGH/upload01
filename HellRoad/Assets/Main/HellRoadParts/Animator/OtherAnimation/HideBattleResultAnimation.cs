using Utility;

namespace HellRoad.External.Animation
{
    public class HideBattleResultAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

		public void DoAnimation()
		{
			ITakeAwayPartsView takeAwayPartsView = Locater<ITakeAwayPartsView>.Resolve();
			takeAwayPartsView.Hide();
			EndAnimation = true;
		}
    }
}