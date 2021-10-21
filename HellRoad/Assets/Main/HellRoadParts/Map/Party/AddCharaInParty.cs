namespace HellRoad
{
    public class AddCharaInParty
    {
        private IAddAndRemoveCharaInParty addAndRemoveCharaInParty;
        private IAddReduceSoulFragment addReduceSoulFragment;

        private int consumeSoul = 20;

        public AddCharaInParty(IAddAndRemoveCharaInParty addAndRemoveCharaInParty, IAddReduceSoulFragment addReduceSoulFragment)
        {
            this.addAndRemoveCharaInParty = addAndRemoveCharaInParty;
            this.addReduceSoulFragment = addReduceSoulFragment;
        }

        const float DECREASE_MAGNIFICATION = 1.5f;

        public void AddChara()
        {
            if (!HaveEnoughMoney() || MaximumNumberOfParty()) return;

            int idx;
            for(idx = 0; idx < PartyConstantValiable.MAX_PARTY_CHARA_LIMIT; idx++)
            {
                if (!addAndRemoveCharaInParty.ContainsChara(idx))
                {
                    break;
                }
            }

            addAndRemoveCharaInParty.AddChara(idx);
            addReduceSoulFragment.Reduce(consumeSoul);
            consumeSoul = (int)(consumeSoul * DECREASE_MAGNIFICATION);
        }

        public bool HaveEnoughMoney()
        {
            return addReduceSoulFragment.Contains(consumeSoul);
        }

        public bool MaximumNumberOfParty()
        {
            for(int i = 0; i < PartyConstantValiable.MAX_PARTY_CHARA_LIMIT; i++)
            {
                if(!addAndRemoveCharaInParty.ContainsChara(i))
                    return false;
            }
            return true;
        }

        public int ConsumeSoulFragments()
        {
            return consumeSoul;
        }
    }
}