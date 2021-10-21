using System.Collections.Generic;
using Utility;

namespace HellRoad.External.Animation
{
    public class StartBattleAnimation : IGameAnimation
	{
		public bool EndAnimation { get; set; } = false;

		private WholeBodyView playerView, enemyView;
		private EnemyGroupAsset enemyCharas;

		public StartBattleAnimation(WholeBodyView playerView, WholeBodyView enemyView,  EnemyGroupAsset enemyCharas)
		{
			this.playerView = playerView;
			this.enemyView = enemyView;
			this.enemyCharas = enemyCharas;
		}

		void IGameAnimation.DoAnimation()
		{
			IBattleParty playerParty = Locater<IBattleParty>.Resolve();
			Party enemyParty = new Party(enemyCharas);

			ICharaViewCreater enemyEventListener = Locater<ICharaViewCreater>.Resolve();
			enemyEventListener.SetEventByChara(Players.Enemy, enemyParty, enemyParty, enemyView);
			
			Battle battle = new Battle(playerParty, enemyParty);

			Locater<IBattleView>.Resolve().StartBattleAnimation(playerView, enemyView, () =>
			{
				battle.Start();
				EndAnimation = true;
			});
		}
	}
}