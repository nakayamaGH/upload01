using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility;
using Utility.Audio;

namespace HellRoad.External
{
    public class BattleView : MonoBehaviour, IBattleView
    {
        [SerializeField] BattleCharaAnimator playerAnimator = null;
        [SerializeField] BattleCharaAnimator enemyAnimator = null;
        [SerializeField] PlayerActionsView playerActionsView = null;
        [SerializeField] EnemyStatusView enemyStatusView = null;
        [SerializeField] BattleBackground background = null;
        [SerializeField] Transform cameraTracker = null;

        [SerializeField] float timeToMove = 1;

		private void Awake()
        {
            Locater<BattleCharaAnimator>.Register(playerAnimator, (int)Players.Me);
            Locater<BattleCharaAnimator>.Register(enemyAnimator, (int)Players.Enemy);
        }

		public async void StartBattleAnimation(WholeBodyView playerView, WholeBodyView enemyView, Action onEndAnimation)
        {
            Locater<IChangeMapState>.Resolve().ChangeState(MapSceneState.Battle);

            Vector2 cameraPos = cameraTracker.position;
            transform.position = new Vector3(cameraPos.x, cameraPos.y, transform.position.z);

            playerAnimator.SetChara(playerView);
            enemyAnimator.SetChara(enemyView);

            playerAnimator.DOReturnPosition(timeToMove);
            enemyAnimator.DOReturnPosition(timeToMove);

            Locater<IPlayAudio>.Resolve().PlaySE("BattleStart");
            background.Show();
            playerActionsView.Show(timeToMove);
            enemyStatusView.Show(timeToMove);

            await UniTask.Delay((int)(timeToMove * 1000));

            playerAnimator.ChangeAnimation(AnimName.FightingPose_1, 0.5f);
            enemyAnimator.ChangeAnimation(AnimName.FightingPose_1, 0.5f);

            onEndAnimation();
        }

        public async void WinBattleAnimation(Action onEndAnimation)
        {
            Locater<IChangeMapState>.Resolve().ChangeState(MapSceneState.Map);

            Vector2 cameraPos = cameraTracker.position;
            transform.position = new Vector3(cameraPos.x, cameraPos.y, transform.position.z);

            background.Hide();
            playerActionsView.Hide(timeToMove);
            enemyStatusView.Hide(timeToMove);

            await UniTask.Delay((int)(timeToMove * 1000));
            onEndAnimation();
        }
    }

    public interface IBattleView
    {
        void StartBattleAnimation(WholeBodyView playerView, WholeBodyView enemyView, Action onEndAnimation);
        void WinBattleAnimation(Action onEndAnimation);

    }
}