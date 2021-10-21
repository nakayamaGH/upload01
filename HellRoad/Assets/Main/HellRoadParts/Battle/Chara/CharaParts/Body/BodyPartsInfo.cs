using System;
using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public class BodyPartsInfo : PartsInfo
    {
        [SerializeField] Sprite body = null;
        [SerializeField] Vector2 headSeams = Vector2.zero;
        [SerializeField] Vector2 leftArmSeams = Vector2.zero;
        [SerializeField] Vector2 rightArm_Seams = Vector2.zero;
        [SerializeField] Vector2 leftLeg_Seams = Vector2.zero;
        [SerializeField] Vector2 rightLeg_Seams = Vector2.zero;
        [SerializeField] ZIndex zIndex = ZIndex.Back;
        [SerializeField] Sprite backBody = null;

        public Sprite Body => body;
        public Vector2 HeadSeams => headSeams;
        public Vector2 LeftArmSeams => leftArmSeams;
        public Vector2 RightArmSeams => rightArm_Seams;
        public Vector2 LeftLegSeams => leftLeg_Seams;
        public Vector2 RightLegSeams => rightLeg_Seams;
        public ZIndex ZIndex => zIndex;
        public Sprite BackBody => backBody;

        public override Sprite Icon => body;

#if UNITY_EDITOR
		public void Setup(Sprite body, Vector2 headSeams, Vector2 leftArmSeams, Vector2 rightArm_Seams, Vector2 leftLeg_Seams, Vector2 rightLeg_Seams, Sprite backBody)
        {
            this.body = body;
            this.headSeams = headSeams;
            this.leftArmSeams = leftArmSeams;
            this.rightArm_Seams = rightArm_Seams;
            this.leftLeg_Seams = leftLeg_Seams;
            this.rightLeg_Seams = rightLeg_Seams;
            this.backBody = backBody;
        }
#endif
    }
    public enum ZIndex
    {
        Back    = 0,
        Front   = -2,
    }
}