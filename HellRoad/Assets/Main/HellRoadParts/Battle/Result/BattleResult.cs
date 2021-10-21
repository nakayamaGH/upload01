using System;
using Utility;

namespace HellRoad
{
	public class BattleResult
	{
		private IStickPartsInParty myPartyStickParts;

		public BattleResult(IStickPartsInParty myPartyStickParts)
		{
			this.myPartyStickParts = myPartyStickParts;
		}

		public void GetResult(int meatPiece, int soulFragment, IGetStickedParts enemyParts, Action onEndTakeAway)
		{
			TakeAwayParts takeAwayParts = new TakeAwayParts();

			takeAwayParts.StartTakeAway(myPartyStickParts, enemyParts, onEndTakeAway);

			Locater<IAddReduceMeatPiece>.Resolve().Add(meatPiece);
			Locater<IAddReduceSoulFragment>.Resolve().Add(soulFragment);
		}
	}
}