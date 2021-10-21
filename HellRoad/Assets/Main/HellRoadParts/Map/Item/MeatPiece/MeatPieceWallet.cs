using System;

namespace HellRoad
{
    public class MeatPieceWallet : IAddReduceMeatPiece, IAddReduceMeatPieceEventListener
    {
        public int HavePieces { get; private set; }

        public event Action<int> OnChangeValueAction;

        public MeatPieceWallet(int pieces)
        {
            Add(pieces);
        }

        public void Add(int pieces)
        {
            HavePieces += pieces;
            OnChangeValueAction?.Invoke(HavePieces);
        }

        public void Reduce(int pieces)
        {
            HavePieces -= pieces;
            OnChangeValueAction?.Invoke(HavePieces);
        }

        public bool Contains(int pieces)
        {
            return HavePieces >= pieces;
        }

        public int GetHavePieces()
        {
            return HavePieces;
        }
    }
}