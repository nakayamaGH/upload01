using UnityEngine;

namespace HellRoad.External
{
    public class BodyAbandonedPartsView : BaseAbandonedPartsView
    {
        [SerializeField] CharaBodyView bodyView = null;

        protected override CharaPartsView partsView => bodyView;

        protected override PartsType Type => PartsType.Body;

        protected override void ViewInitalize(PartsInfo info)
        {
            BodyPartsInfo bodyInfo = info as BodyPartsInfo;
            bodyView.Initalize(bodyInfo.Body, bodyInfo.BackBody);
        }
    }
}