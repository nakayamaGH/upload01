using System;

namespace HellRoad
{
    public interface IAddReduceMeatPiece : IGetMeatPieces
    {
        void Add(int pieces);
        void Reduce(int pieces);
    }

    public interface IGetMeatPieces
    {
        int GetHavePieces();
        bool Contains(int pieces);
    }

    public interface IAddReduceMeatPieceEventListener
    {
        event Action<int> OnChangeValueAction;
    }
}