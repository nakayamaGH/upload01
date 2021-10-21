using UnityEngine;

namespace HellRoad.External
{
    public class LegsAbandonedPartsView : BaseAbandonedPartsView
    {
        [SerializeField] CharaLegsView legsView = null;

        [SerializeField] Transform rotate_1 = null;
        [SerializeField] Transform rotate_2 = null;

        protected override CharaPartsView partsView => legsView;

        protected override PartsType Type => PartsType.Legs;

        protected override void ViewInitalize(PartsInfo info)
        {
            LegsPartsInfo legsInfo = info as LegsPartsInfo;
            legsView.Initaize(legsInfo.LeftLegs, legsView.transform.localPosition, legsInfo.LeftLegs_2_Seams, legsInfo.LeftLegs_3_Seams, 0);

            rotate_1.localEulerAngles = new Vector3(0, 0, Random.Range(160, 200));
            rotate_2.localEulerAngles = new Vector3(0, 0, Random.Range(-20, 20));
        }
    }
}