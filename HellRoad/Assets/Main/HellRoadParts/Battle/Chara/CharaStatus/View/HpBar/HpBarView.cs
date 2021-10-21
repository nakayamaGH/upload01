using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HellRoad.External.Animation
{
    public class HpBarView : MonoBehaviour, IHpBarView
    {
        [SerializeField] Slider slider = null;
        [SerializeField] TMP_Text text = null;

        private Sequence sequence;

        public void Validate(int maxHp, int nowHp, int beforeHp)
        {
            float beforeValue = slider.value;
            float value = (float)nowHp / maxHp;

            const float DURATION = 0.1f;

            if (sequence != null) sequence.Kill();
            
            sequence = DOTween.Sequence();

            sequence.Append(DOVirtual.Int(beforeHp, nowHp, DURATION, (updateValue) =>
            {
                text.text = updateValue.ToString() + "/" + maxHp;
            }));
            sequence.Join(slider.DOValue(value, DURATION));
        }
    }

    public interface IHpBarView
    {
        void Validate(int maxHp, int nowHp, int beforeHp);
    }
}