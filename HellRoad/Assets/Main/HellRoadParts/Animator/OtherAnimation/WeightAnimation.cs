using System.Threading.Tasks;

namespace HellRoad.External.Animation
{
	public class WeightAnimation : IGameAnimation
	{
		public bool EndAnimation { get; set; } = false;

		private int milliSec = 0;

		public WeightAnimation(float sec)
		{
			milliSec = (int)(sec * 1000);
		}

		public async void DoAnimation()
		{
			await Task.Delay(milliSec);
			EndAnimation = true;
		}
	}
}