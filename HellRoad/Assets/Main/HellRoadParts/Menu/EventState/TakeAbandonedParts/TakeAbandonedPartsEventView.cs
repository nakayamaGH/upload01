using Cysharp.Threading.Tasks;
using HellRoad.External.Animation;
using System;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class TakeAbandonedPartsEventView : MonoBehaviour, ITakeAbandonedPartsEvent
    {
        [SerializeField] TakeAbandonedPartsPlayerView playerView = null;
        [SerializeField] EventStateCameraTracker cameraTracker = null;

        private void Awake()
        {
            IAddEventOnChangeState stateEv = Locater<IAddEventOnChangeState>.Resolve();
            
            stateEv.OnBeginState += (state) =>
            {

            };
        }

        public async void Take(WholeBodyView playerCharaView, BaseAbandonedPartsView enemyCharaView, EnemyGroupAsset enemyGroup, bool isEnemy)
        {
            Locater<IChangeMapState>.Resolve().ChangeState(MapSceneState.TakeAbandonedParts);

            playerView.SetPlayer(playerCharaView);
            playerView.TakeAnimation();

            cameraTracker.SetCameraSize(10);
            cameraTracker.SetCameraOffset(Vector2.up * 64);

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            cameraTracker.SetCameraSize(cameraTracker.DefaultCameraSize + 300);
            cameraTracker.SetCameraOffset(Vector2.up * 100);
            enemyCharaView.PullUp();

            await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
            enemyCharaView.WholeBodyView.transform.eulerAngles = new Vector3(0, 0, 0);

            cameraTracker.SetCameraSize(cameraTracker.DefaultCameraSize);
            cameraTracker.SetCameraOffset(cameraTracker.DefaultCameraOffset);

            if (isEnemy)
            {
                IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
                WholeBodyView playerView = Locater<IMapPlayerView>.Resolve().WholeBody;
                addAnim.Add(new StartBattleAnimation(playerView, enemyCharaView.WholeBodyView, enemyGroup));
            }
            else
            {
                //ÉpÅ[Écì¸éË
            }
        }
    }

    public interface ITakeAbandonedPartsEvent
    {
        void Take(WholeBodyView playerCharaView, BaseAbandonedPartsView enemyCharaView, EnemyGroupAsset enemyGroup, bool isEnemy);
    }
}