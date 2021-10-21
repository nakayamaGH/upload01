namespace HellRoad
{
    public class StickPartsToChara
    {
        private IStickPartsInParty stickPartsInParty;
        private IAddDeleteToPartsBag addDeleteToPartsBag;

        public StickPartsToChara(IStickPartsInParty stickPartsInParty, IAddDeleteToPartsBag addDeleteToPartsBag)
        {
            this.stickPartsInParty = stickPartsInParty;
            this.addDeleteToPartsBag = addDeleteToPartsBag;
        }

        public void StickToChara(int partyIdx, int partsIdx, PartsID ID)
        {
            if(CanStick(partyIdx))
            {
                stickPartsInParty.StickParts(partyIdx, ID);
                addDeleteToPartsBag.Delete(ID.ToPartsType(), partsIdx);
            }
        }

        public bool CanStick(int partyIdx)
        {
            return stickPartsInParty.ContainsChara(partyIdx);
        }
    }
}