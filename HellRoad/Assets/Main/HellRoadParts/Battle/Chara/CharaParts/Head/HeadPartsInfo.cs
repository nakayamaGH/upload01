using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public class HeadPartsInfo : PartsInfo
    {
        [SerializeField] Sprite head = null;

        public Sprite Head => head;

        public override Sprite Icon => head;

#if UNITY_EDITOR
		public void Setup(Sprite head)
        {
            this.head = head;
        }
#endif
    }
}