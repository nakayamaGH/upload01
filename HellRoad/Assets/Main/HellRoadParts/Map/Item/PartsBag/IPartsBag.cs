using System.Collections.Generic;

namespace HellRoad
{
    public interface IAddDeleteToPartsBag : IGetToPartsBag
    {
        void Add(PartsID parts);
        void ChangeParts(PartsID parts, int idx);
        void Delete(PartsType type, int idx);
    }

    public interface IUpgradePartsBag
    {
        PartsBagLevel Level { get; }
        void Upgrade();
    }

    public interface IGetToPartsBag
    {
        PartsID GetParts(PartsType type, int idx);
        List<PartsID> GetPartsList(PartsType type);
        int GetPartsCount(PartsType type);
        bool CanAdd(PartsType type);
    }
}