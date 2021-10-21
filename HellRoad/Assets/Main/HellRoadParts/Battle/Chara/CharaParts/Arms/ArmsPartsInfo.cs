using System.Collections.Generic;
using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public class ArmsPartsInfo : PartsInfo
    {
        [SerializeField] ArmsType armsType = ArmsType.SixParts;
        [SerializeField] List<Sprite> leftArms = null;
        [SerializeField] List<Sprite> rightArms = null;
        [SerializeField] Vector2 leftArm_2_Seams = Vector2.zero;
        [SerializeField] Vector2 rightArm_2_Seams = Vector2.zero;

        public ArmsType ArmsType => armsType;
        public List<Sprite> LeftArms => leftArms;
        public List<Sprite> RightArms => rightArms;
        public Vector2 LeftArm_2_Seams => leftArm_2_Seams;
        public Vector2 RightArm_2_Seams => rightArm_2_Seams;

        public override Sprite Icon => leftArms[0];

#if UNITY_EDITOR
		public void Setup(List<Sprite> leftArms, List<Sprite> rightArms, Vector2 leftArm_2_Seams, Vector2 rightArm_2_Seams)
        {
            this.leftArms = leftArms;
            this.rightArms = rightArms;
            this.leftArm_2_Seams = leftArm_2_Seams;
            this.rightArm_2_Seams = rightArm_2_Seams;
        }
#endif
    }

    public enum ArmsType
    {
        SixParts,
        FourParts,
        TwoParts,
        APart,
    }
}