using UnityEngine;
using UnityEngine.UI;

namespace HellRoad.External
{
    public class CharaIconView : MonoBehaviour
    {
        [SerializeField] Image iconView = null;
        [SerializeField] ParticleSystem addedEffect = null;

        public void SetIcon(Sprite iconSprite)
        {
            iconView.sprite = iconSprite;
            iconView.SetNativeSize();
            iconView.rectTransform.sizeDelta *= 2;
        }

        public void PlayEffect()
        {
            addedEffect.Play();
        }
    }
}