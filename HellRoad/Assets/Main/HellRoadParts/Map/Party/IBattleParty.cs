using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HellRoad
{
    public interface IBattleParty : IStickPartsInParty, ISortParty
    {
        public Players Players { get; }
        ReadOnlyCollection<IBattleChara> GetCharas();
    }
}