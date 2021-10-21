using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public class CharaTemplate
    {
        [SerializeField] Status additionalStatus = null;
        [SerializeField] PartsID head = PartsID.Skull_Head;
        [SerializeField] PartsID body = PartsID.Skeleton_Body;
        [SerializeField] PartsID arms = PartsID.Skeleton_Arms;
        [SerializeField] PartsID legs = PartsID.Skeleton_Legs;

        public Status AdditionalStatus => additionalStatus;
        public PartsID Head => head;
        public PartsID Body => body;
        public PartsID Arms => arms;
        public PartsID Legs => legs;

        public PartsID GetParts(PartsType type)
        {
            switch(type)
            {
                case PartsType.Head:
                    return Head;
                case PartsType.Body:
                    return Body;
                case PartsType.Arms:
                    return Arms;
                case PartsType.Legs:
                    return Legs;
            }
            return PartsID.None;
        }

        public CharaTemplate(Status additionalStatus, PartsID head, PartsID body, PartsID arms, PartsID legs)
        {
            this.additionalStatus = additionalStatus;
            this.head = head;
            this.body = body;
            this.arms = arms;
            this.legs = legs;
        }
    }
}