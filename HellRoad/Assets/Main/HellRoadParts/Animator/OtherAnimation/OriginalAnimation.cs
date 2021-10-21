using System;

namespace HellRoad.External.Animation
{
    public class OriginalAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private Action<OriginalAnimation> action;

        public OriginalAnimation(Action<OriginalAnimation> action)
        {
            this.action = action;
        }

        void IGameAnimation.DoAnimation()
        {
            action?.Invoke(this);
        }
    }
}