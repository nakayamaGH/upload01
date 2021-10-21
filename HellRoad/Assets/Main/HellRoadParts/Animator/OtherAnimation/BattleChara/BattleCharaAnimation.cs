using Cysharp.Threading.Tasks;
using Utility;

namespace HellRoad.External.Animation
{
	public class BattleCharaAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private Players players;
        private AnimName animName;
        private float duration;
        private float crossFadeTime;
        private bool skipDuration;
        private bool infinity;

        public BattleCharaAnimation(Players players, AnimName animName, float duration, float crossFadeTime, bool skipDuration = false, bool infinity = false)
		{
			this.players = players;
			this.animName = animName;
			this.duration = duration;
			this.crossFadeTime = crossFadeTime;
            this.skipDuration = skipDuration;
            this.infinity = infinity;
        }

		async void IGameAnimation.DoAnimation()
        {
            BattleCharaAnimator animator = Locater<BattleCharaAnimator>.Resolve((int)players);
            if(infinity)
            {
                animator.ChangeAnimation(animName, crossFadeTime);
            }
            else
            {
                animator.ChangeAnimation(animName, duration, crossFadeTime);
                if (!skipDuration) await UniTask.Delay((int)(duration * 1000));
            }
            EndAnimation = true;
        }
    }
}