using DG.Tweening;
using UnityEngine;
using Utility;

namespace HellRoad.External.Animation
{
    public class BattleCharaMoveAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private Players players;
        private Vector2 position;
        private float duration;
        private bool skipDuration;
        private Ease ease;

        public BattleCharaMoveAnimation(Players players, Vector2 position, float duration, bool skipDuration = false, Ease ease = Ease.InCubic)
        {
            this.players = players;
            this.position = position;
            this.duration = duration;
            this.skipDuration = skipDuration;
            this.ease = ease;
		}

        void IGameAnimation.DoAnimation()
        {
            if (skipDuration) EndAnimation = true;
            Locater<BattleCharaAnimator>.Resolve((int)players).DOLocalMove(position, duration).SetEase(ease).onComplete += () => 
            {
                EndAnimation = true;
            };
        }
    }
}