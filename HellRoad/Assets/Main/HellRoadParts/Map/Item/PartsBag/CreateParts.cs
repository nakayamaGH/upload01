using Utility;

namespace HellRoad
{
    public class CreateParts
    {
        private IAddDeleteToPartsBag addDeleteToPartsBag;
        private IAddReduceMeatPiece addReduceMeatPiece;
        private IGetPartsInfoFromDB getPartsInfoFromDB;

        public CreateParts(IAddDeleteToPartsBag addDeleteToPartsBag, IAddReduceMeatPiece addReduceMeatPiece)
        {
            this.addDeleteToPartsBag = addDeleteToPartsBag;
            this.addReduceMeatPiece = addReduceMeatPiece;
            getPartsInfoFromDB = Locater<IGetPartsInfoFromDB>.Resolve();
        }

        public bool CanAddByBagIsFull(PartsID partsID)
        {
            return addDeleteToPartsBag.CanAdd(partsID.ToPartsType());
        }

        public bool CanAddByLackOfMeat(PartsID partsID)
        {
            return addReduceMeatPiece.Contains(getPartsInfoFromDB.Get(partsID).DropMeatAmount * 4);
        }

        public void Add(PartsID partsID)
        {
            if(CanAddByBagIsFull(partsID) && CanAddByLackOfMeat(partsID))
                addDeleteToPartsBag.Add(partsID);
        }
    }
}