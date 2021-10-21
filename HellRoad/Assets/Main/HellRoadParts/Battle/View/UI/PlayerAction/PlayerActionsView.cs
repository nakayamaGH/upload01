using UnityEngine;

namespace HellRoad.External
{
    public class PlayerActionsView : MonoBehaviour
    {
        [SerializeField] UIMover uiMover = null;

        private Vector2 offset = new Vector2(1, -0.2f);

        public void Show(float duration)
        {
            gameObject.SetActive(true);
            uiMover.SetMaxAnchor(offset);
            uiMover.DOMoveToDefaultMaxAnchor(duration);
        }

        public void Hide(float duration)
        {
            uiMover.DOMoveToMaxAnchor(offset, duration).onComplete += () =>
            gameObject.SetActive(false);
        }
    }
}