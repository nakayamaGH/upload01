using System;

namespace HellRoad
{
    public interface IParty : IAddAndRemoveCharaInParty, IStickPartsInParty, ISortParty, IPartyEventListener, IBattleParty
    {
    }

    public interface IAddAndRemoveCharaInParty : IGetCharaInfoInParty
    {
        void AddChara(int idx);
        void AddChara(int idx, CharaTemplate template);
        void RemoveChara(int idx);
    }

    public interface IGetCharaInfoInParty
    {
        CharaInfo GetInfo(int i);
        bool ContainsChara(int i);
        bool ContainsCharaInParty();
    }

    public interface IStickPartsInParty : IGetCharaInfoInParty
    {
        void StickParts(int idx, PartsID parts);
        IGetStickedParts GetStickedParts(int idx);
    }

    public interface ISortParty
    {
        void Sort(int idx_1, int idx_2);
        void Shift(Direction dir);
    }

    public interface IPartyEventListener
    {
        event Action<int> OnAddChara;
        event Action<int> OnRemoveChara;
        event Action<int, PartsID> OnStickParts;
        event Action<int, int> OnSortingParty;
        event Action OnShiftedParty;
    }
}