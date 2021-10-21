using System.Collections.Generic;
using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public class PartsBag : IUpgradePartsBag, IAddDeleteToPartsBag
    {
        [SerializeField] private List<PartsID> headPartsList = new List<PartsID>();
        [SerializeField] private List<PartsID> bodyPartsList = new List<PartsID>();
        [SerializeField] private List<PartsID> armsPartsList = new List<PartsID>();
        [SerializeField] private List<PartsID> legsPartsList = new List<PartsID>();
        [SerializeField] private PartsBagLevel level = PartsBagLevel.Level_1;

        public PartsBagLevel Level => level;

        public PartsID GetParts(PartsType type, int idx)
        {
            return GetPartsList(type)[idx];
        }

        public int GetPartsCount(PartsType type)
        {
            return GetPartsList(type).Count;
        }

        public void Add(PartsID parts)
        {
            PartsType type = parts.ToPartsType();
            if (CanAdd(type))
            {
                GetPartsList(type).Add(parts);
            }
        }

        public void ChangeParts(PartsID parts, int idx)
        {
            PartsType type = parts.ToPartsType();
            GetPartsList(type)[idx] = parts;
        }

        public void Delete(PartsType type, int idx)
        {
            GetPartsList(type).RemoveAt(idx);
        }

        public bool CanAdd(PartsType type)
        {
            return level.HeldUpCount() > GetPartsList(type).Count;
        }

        public void Upgrade()
        {
            if (level != PartsBagLevel.Level_Max)
                level++;
        }

        private List<PartsID> GetPartsList(PartsType type)
        {
            switch (type)
            {
                case PartsType.Head:
                    return headPartsList;
                case PartsType.Body:
                    return bodyPartsList;
                case PartsType.Arms:
                    return armsPartsList;
                case PartsType.Legs:
                    return legsPartsList;
            }
            return null;
        }

        List<PartsID> IGetToPartsBag.GetPartsList(PartsType type)
        {
            return GetPartsList(type);
        }
    }
}