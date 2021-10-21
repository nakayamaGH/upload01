using DG.Tweening;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class PlayerActionShowPartsInfo : MonoBehaviour
    {
        [SerializeField] WholeBodyInfoView partsInfoView = null;
        [SerializeField] Players players;
        [SerializeField] CanvasGroup group = null;
        [SerializeField] float fadeTime = 0.5f;
        [SerializeField] UIMover uiMover = null;
 
        private bool show = false;

		private void Awake()
		{
            Locater<IAddEventOnChangeState>.Resolve().OnEndState += (state) =>
            {
                if(state == MapSceneState.Battle)
                {
                    Hide();
				}
            };
		}

		public void ChangeShowOrHide()
        {
            if(show)
            {
                Hide();
			}
            else
            {
                Show();
			}
		}

        public void Show()
        {
            if (show) return;

            IGetWholeBodyProperty info = Locater<ICharasFieldGetCharaInfo>.Resolve((int)players).GetFrontCharaInfo().GetInfo().GetWholeBody;
            partsInfoView.ShowInfo(info.GetParts(PartsType.Head), info.GetParts(PartsType.Body), info.GetParts(PartsType.Arms), info.GetParts(PartsType.Legs));

            gameObject.SetActive(true);

            group.DOFade(1, fadeTime);
            uiMover.DOMoveToMaxAnchor(new Vector2(uiMover.MaxAnchor.x, 1), fadeTime);
            show = true;
        }

        public void Hide()
        {
            if (!show) return;
            uiMover.DOMoveToMaxAnchor(new Vector2(uiMover.MaxAnchor.x, 2), fadeTime);
            group.DOFade(0, fadeTime).onComplete += () => 
            {
                gameObject.SetActive(false);
            };
            show = false;
        }
    }
}