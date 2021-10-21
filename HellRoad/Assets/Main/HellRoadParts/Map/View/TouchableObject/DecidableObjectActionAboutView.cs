using DG.Tweening;
using UnityEngine;

namespace HellRoad.External
{
    public class DecidableObjectActionAboutView : MonoBehaviour, IDecidableObjectActionAboutView
    {
        [SerializeField] CanvasGroup group = null;
        [SerializeField] float fadeDuration = 1;

        private Tween hideTween = null;

        public void Show(Vector2 position)
        {
            if (hideTween != null) hideTween.Kill();

            transform.position = new Vector3(position.x, position.y, transform.position.z);

            gameObject.SetActive(true);
            group.DOFade(1, fadeDuration);
        }

        public void Hide()
        {
            hideTween = group.DOFade(0, fadeDuration);
            hideTween.onComplete += () => 
            {
                gameObject.SetActive(false);
            };
        }
    }

    public interface IDecidableObjectActionAboutView
    {
        void Show(Vector2 position);
        void Hide();
    }
}