using UnityEngine;

namespace HellRoad.External
{
    public class ArmsAbandonedPartsView : BaseAbandonedPartsView
    {
        [SerializeField] CharaArmsView armsView = null;

        [SerializeField] Transform rotate_1 = null;
        [SerializeField] Transform rotate_2 = null;

        protected override PartsType Type => PartsType.Arms;

        protected override CharaPartsView partsView => armsView;

        protected override void ViewInitalize(PartsInfo info)
        {
            ArmsPartsInfo armsInfo = info as ArmsPartsInfo;
            armsView.Initaize(armsInfo.LeftArms, armsView.transform.localPosition, armsInfo.LeftArm_2_Seams);

            rotate_1.localEulerAngles = new Vector3(0, 0, Random.Range(160, 200));
            rotate_2.localEulerAngles = new Vector3(0, 0, Random.Range(-20, 20));
        }
    }
}