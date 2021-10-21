using Utility;

namespace HellRoad
{
    public class BagUpgrader
    {
        private IAddReduceSoulFragment reduceSoulFragment;
        private IUpgradePartsBag upgradePartsBag;

        public BagUpgrader(IAddReduceSoulFragment reduceSoulFragment, IUpgradePartsBag upgradePartsBag)
        {
            this.reduceSoulFragment = reduceSoulFragment;
            this.upgradePartsBag = upgradePartsBag;
        }

        public bool CanUpgrade()
        {
            int cost = upgradePartsBag.Level.UpgradeCost();
            return reduceSoulFragment.Contains(cost);
        }

        public void Upgrade()
        {
            upgradePartsBag.Upgrade();
        }
    }
}