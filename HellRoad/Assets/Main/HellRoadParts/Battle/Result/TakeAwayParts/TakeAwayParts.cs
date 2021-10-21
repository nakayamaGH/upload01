using Cysharp.Threading.Tasks;
using HellRoad.External.Animation;
using System;
using System.Collections.Generic;
using Utility;

namespace HellRoad
{
    public class TakeAwayParts
    {
        private List<PartsType> stickedPartsType = new List<PartsType>();

        private bool skipTakeAway = false;


        public async void StartTakeAway(IStickPartsInParty myPartyStickParts, IGetStickedParts enemyParts, Action onEndTakeAway)
        {
            ShowBattleResultAnimation showResultAnim = new ShowBattleResultAnimation(myPartyStickParts, enemyParts);
            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
            addAnim.Add(showResultAnim);

            await UniTask.WaitUntil(() => showResultAnim.CanTakeAway);
            while (CanTakeAway())
            {
                (int, PartsType, bool) tookAway = await showResultAnim.GetPartsTask();

                skipTakeAway = tookAway.Item3;
                if (!skipTakeAway && !ContainsPartsType(tookAway.Item2))
                {
                    myPartyStickParts.StickParts(tookAway.Item1, enemyParts.GetParts(tookAway.Item2));
                    stickedPartsType.Add(tookAway.Item2);
                    showResultAnim.OnTakeAwayParts(tookAway.Item2);
                }
            }
            showResultAnim.EndAnimationView();
            addAnim.Add(new HideBattleResultAnimation());
            onEndTakeAway();
        }

        private bool CanTakeAway()
        {
            return stickedPartsType.Count < 4 && !skipTakeAway;
		}

        private bool ContainsPartsType(PartsType type)
        {
            return stickedPartsType.Contains(type);
		}
    }
}