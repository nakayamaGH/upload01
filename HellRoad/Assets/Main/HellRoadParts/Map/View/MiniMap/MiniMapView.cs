using DG.Tweening;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class MiniMapView : MonoBehaviour
    {
        [SerializeField] CanvasGroup parent = null;
        [SerializeField] float mapScale = 3;
        [SerializeField] float tileSize = 32;

        private Tween showTween = null;

        public void Awake()
        {
            IAddEventOnChangeState addEventOnChangeState = Locater<IAddEventOnChangeState>.Resolve();

            addEventOnChangeState.OnBeginState += (state) =>
            {
                if (state == MapSceneState.Map)
                {
                    parent.gameObject.SetActive(true);
                    if (showTween != null) showTween.Kill();
                    showTween = parent.DOFade(1, 1);
                }
            };

            addEventOnChangeState.OnEndState += (state) =>
            {
                if (state == MapSceneState.Map)
                {
                    if (showTween != null) showTween.Kill();
                    showTween = parent.DOFade(0, 1);
                    showTween.onComplete += () => parent.gameObject.SetActive(false);
                }
            };
        }
    }

    public interface ISetMiniMapPosition
    {
        void SetPosition(Vector2 pos);
    }
}