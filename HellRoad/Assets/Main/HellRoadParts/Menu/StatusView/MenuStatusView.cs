using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class MenuStatusView : MonoBehaviour
    {
        [SerializeField] ChangeCharaInMenuChild changeChara = null;
        [SerializeField] WholeBodyInfoView wholeBodyInfoView = null;

        public void Show()
        {
            ValidateStatus();
            changeChara.ValidateCharaIcon();
            changeChara.ChangedEvent += () =>
            {
                ValidateStatus();
            };
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ValidateStatus()
        {
            IGetWholeBodyProperty wholeBody = Locater<IGetCharaInfoInParty>.Resolve().GetInfo(0).GetWholeBody;
            wholeBodyInfoView.ShowInfo(
            wholeBody.GetParts(PartsType.Head), wholeBody.GetParts(PartsType.Body),
            wholeBody.GetParts(PartsType.Arms), wholeBody.GetParts(PartsType.Legs));
        }
    }
}