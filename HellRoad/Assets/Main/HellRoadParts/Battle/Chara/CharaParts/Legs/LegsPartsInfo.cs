using System;
using System.Collections.Generic;
using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public class LegsPartsInfo : PartsInfo
    {
        [SerializeField] LegsType legsType = LegsType.SixParts;
        [SerializeField] List<Sprite> leftLegs = null;
        [SerializeField] List<Sprite> rightLegs = null;
        [SerializeField] Vector2 leftLegs_2_Seams = Vector2.zero;
        [SerializeField] Vector2 leftLegs_3_Seams = Vector2.zero;
        [SerializeField] Vector2 rightLegs_2_Seams = Vector2.zero;
        [SerializeField] Vector2 rightLegs_3_Seams = Vector2.zero;
        [SerializeField] float legLength = 16;


        public LegsType LegsType => legsType;
        public List<Sprite> LeftLegs => leftLegs;
        public List<Sprite> RightLegs => rightLegs;
        public Vector2 LeftLegs_2_Seams => leftLegs_2_Seams;
        public Vector2 LeftLegs_3_Seams => leftLegs_3_Seams;
        public Vector2 RightLegs_2_Seams => rightLegs_2_Seams;
        public Vector2 RightLegs_3_Seams => rightLegs_3_Seams;
        public float LegLength => legLength;

        public override Sprite Icon => leftLegs[0];

#if UNITY_EDITOR
		public void Setup(List<Sprite> leftLegs, List<Sprite> rightLegs, Vector2 leftLegs_2_Seams, Vector2 leftLegs_3_Seams, Vector2 rightLegs_2_Seams, Vector2 rightLegs_3_Seams)
        {
            this.leftLegs = leftLegs;
            this.rightLegs = rightLegs;
            this.leftLegs_2_Seams = leftLegs_2_Seams;
            this.leftLegs_3_Seams = leftLegs_3_Seams;
            this.rightLegs_2_Seams = rightLegs_2_Seams;
            this.rightLegs_3_Seams = rightLegs_3_Seams;
        }
#endif
    }

    public enum LegsType
    {
        SixParts,
        FourParts,
        TwoParts,
        APart,
    }
}