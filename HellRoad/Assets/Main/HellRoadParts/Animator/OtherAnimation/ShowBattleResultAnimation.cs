using System.Threading.Tasks;
using UnityEngine;
using Utility;
using Utility.Audio;

namespace HellRoad.External.Animation
{
    public class ShowBattleResultAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

		private IStickPartsInParty myPartyStickParts; 
		private IGetStickedParts enemyParts;

		private ITakeAwayPartsView takeAwayPartsView;

		public bool CanTakeAway { get; private set; } = false;

		public ShowBattleResultAnimation(IStickPartsInParty myPartyStickParts, IGetStickedParts enemyParts)
		{
			this.myPartyStickParts = myPartyStickParts;
			this.enemyParts = enemyParts;
		}

		public void DoAnimation()
		{
			StartAnimationView();
			takeAwayPartsView = Locater<ITakeAwayPartsView>.Resolve();
			takeAwayPartsView.Show(myPartyStickParts, enemyParts);
			CanTakeAway = true;
			EndAnimation = true;
		}

		public Task<(int, PartsType, bool)> GetPartsTask()
		{
			if (!CanTakeAway) return null;
			return takeAwayPartsView.TakeAway();
		}

		public void OnTakeAwayParts(PartsType partsType)
		{
			//takeAwayPartsView.Show(myPartyStickParts, enemyParts);

			BattleCharaAnimator me = Locater<BattleCharaAnimator>.Resolve((int)Players.Me);
			BattleCharaAnimator enemy = Locater<BattleCharaAnimator>.Resolve((int)Players.Enemy);

			enemy.CharaView.DOFadeColor(partsType, new Color(1, 1, 1, 0), 0.25f);
			CharaPartsView[] eView = enemy.CharaView.GetInfoView(partsType);
			Locater<IPlayEffect>.Resolve().Play(EffectID.RedImpact, eView[0].GetPosition());

			CharaPartsView[] pView = me.CharaView.GetInfoView(partsType);
			Locater<IPlayEffect>.Resolve().Play(EffectID.RedImpact, pView[0].GetPosition());

			Locater<IPlayAudio>.Resolve().PlaySE("TakeAwayParts");
		}

		private void StartAnimationView()
		{
			BattleCharaAnimator me = Locater<BattleCharaAnimator>.Resolve((int)Players.Me);
			BattleCharaAnimator enemy = Locater<BattleCharaAnimator>.Resolve((int)Players.Enemy);
			me.CharaView.GenerateAura(EffectID.PurpleCharaAura);
			enemy.CharaView.GenerateAura(EffectID.PurpleCharaAura);

			IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
			addAnim.Add(new BattleCharaAnimation(Players.Me, AnimName.TakeAwayParts, 1, 1, true, true));
			addAnim.Add(new BattleCharaMoveAnimation(Players.Me, new UnityEngine.Vector2(0, 32), 1, true));

		}

		public void EndAnimationView()
		{
			BattleCharaAnimator me = Locater<BattleCharaAnimator>.Resolve((int)Players.Me);
			BattleCharaAnimator enemy = Locater<BattleCharaAnimator>.Resolve((int)Players.Enemy);
			me.CharaView.DestroyAura();
			enemy.CharaView.DestroyAura();

		}
	}
}