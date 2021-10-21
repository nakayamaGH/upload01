using DG.Tweening;
using System;
using UnityEngine;
using Utility;
using Utility.Inputer;

namespace HellRoad.External
{
    public class ShowMapStateView : MonoBehaviour, IUpdate
    {
        [SerializeField] GameObject[] activeObjects = null;
        [SerializeField] CanvasGroup canvasGroup = null;
        [SerializeField] float fadeSpeed = 0.5f;

        private IInputer inputer = null;

        private Tween showTween;
        private Tween hideTween;

        private void Awake()
        {
            inputer = Locater<IInputer>.Resolve();
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.ShowMap);

            IAddEventOnChangeState eventListener = Locater<IAddEventOnChangeState>.Resolve();
            eventListener.OnBeginState += (state) =>
            {
                if (state == MapSceneState.ShowMap)
                {
                    Array.ForEach(activeObjects, x => x.gameObject.SetActive(true)); 
                    canvasGroup.alpha = 0;
                    if (showTween != null) return;
                    hideTween.Kill();
                    showTween = canvasGroup.DOFade(1, fadeSpeed);
                    showTween.onComplete += () =>
                    {
                        showTween = null;
                    };
                }
            };
            eventListener.OnEndState += (state) =>
            {
                if (state == MapSceneState.ShowMap)
                {
                    hideTween = canvasGroup.DOFade(0, fadeSpeed);
                    hideTween.onComplete += () => 
                    {
                        Array.ForEach(activeObjects, x => x.gameObject.SetActive(false));
                    };
                }
            };
        }

        public void ManagedFixedUpdate()
        {
        }

        public void ManagedUpdate()
        {
            if (inputer.ShowMapDown())
            {
                ChangeState();
            }
        }

        private void ChangeState()
        {
            Locater<IChangeMapState>.Resolve().ChangeState(MapSceneState.Map);
        }
    }
}
