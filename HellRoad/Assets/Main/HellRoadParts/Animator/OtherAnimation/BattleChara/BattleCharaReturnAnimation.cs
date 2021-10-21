using DG.Tweening;
using Utility;

namespace HellRoad.External.Animation
{
    public class BattleCharaReturnAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private Players players;
        private float duration;
        private bool skipDuration;
        private Ease ease;

        public BattleCharaReturnAnimation(Players players, float duration, bool skipDuration = false, Ease ease = Ease.InCubic)
        {
            this.players = players;
            this.duration = duration;
            this.skipDuration = skipDuration;
            this.ease = ease;
        }

        void IGameAnimation.DoAnimation()
        {
            if (skipDuration) EndAnimation = true;
            Locater<BattleCharaAnimator>.Resolve((int)players).DOReturnPosition(duration).SetEase(ease).onComplete += () =>
            {
                EndAnimation = true;
            };
        }
    }
}