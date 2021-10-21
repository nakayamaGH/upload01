using DG.Tweening;
using UnityEngine;

namespace Utility
{
    public class TimeScaler : ITimeScaler
    {
        private static float nowScale = 0;

        public void DOFadeScale(float after, float duration, Ease ease = Ease.Linear)
        {
            float before = nowScale;
            DOVirtual.Float(nowScale, after, duration, (scale) => 
            {
                SetScale(scale);
            }).SetEase(ease).SetUpdate(true);
		}

        public void SetScale(float scale)
        {
            Time.timeScale = scale;
            nowScale = scale;
        }
    }

    public interface ITimeScaler
    {
        void DOFadeScale(float after, float duration, Ease ease = Ease.Linear);
    }
}