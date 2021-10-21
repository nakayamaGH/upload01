using UnityEngine;

namespace HellRoad.External
{
    public class HeadAbandonedPartsView : BaseAbandonedPartsView
    {
        [SerializeField] CharaHeadView headView = null;

        protected override CharaPartsView partsView => headView;

        protected override PartsType Type => PartsType.Head;

        protected override void ViewInitalize(PartsInfo info)
        {
            HeadPartsInfo headInfo = info as HeadPartsInfo;
            headView.Initalize(headInfo.Head, headView.transform.localPosition);
        }
    }
}