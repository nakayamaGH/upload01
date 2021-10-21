using DG.Tweening;
using Utility;

namespace HellRoad.External.Animation
{
    public class BattleCharaMoveToEnemyAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private Players players;
        private float duration;
        private bool skipDuration;
        private Ease ease;

        public BattleCharaMoveToEnemyAnimation(Players players, float duration, bool skipDuration = false, Ease ease = Ease.InCubic)
        {
            this.players = players;
            this.duration = duration;
            this.skipDuration = skipDuration;
            this.ease = ease;
        }

        void IGameAnimation.DoAnimation()
        {
            if (skipDuration) EndAnimation = true;
            Locater<BattleCharaAnimator>.Resolve((int)players).DOMoveToEnemy(duration).SetEase(ease).onComplete += () =>
            {
                EndAnimation = true;
            };
        }
    }
}