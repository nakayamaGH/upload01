using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility;
using Utility.Inputer;

namespace HellRoad.External
{
    public class MenuStateView : MonoBehaviour, IUpdate
    {
        [SerializeField] MenuView menuView = null;
        [SerializeField] MenuBackground background = null;
        [SerializeField] MenuPlayerView menuPlayer = null;
        [SerializeField] MenuCamera menuCamera = null;

        private IInputer inputer = null;

        private bool canChangeState = true;

        public void Awake()
        {
            menuPlayer.Initalize();
            menuCamera.Initalize();

            inputer = Locater<IInputer>.Resolve();
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Menu);
            IAddEventOnChangeState eventListener = Locater<IAddEventOnChangeState>.Resolve();
            eventListener.OnBeginState += (state) =>
            {
                if (state == MapSceneState.Menu)
                {
                    menuView.Show();
                    background.Show();
                    WaitAnim();
                }
            };
            eventListener.OnEndState += (state) =>
            {
                if (state == MapSceneState.Menu)
                {
                    menuView.Hide(0.2f);
                    background.Hide();
                }
            };
        }

        private async void WaitAnim()
        {
            canChangeState = false;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            canChangeState = true;
        }

        public void EndMenuState()
        {
            if (canChangeState)
                Locater<IChangeMapState>.Resolve().ChangeState(MapSceneState.Map);
        }

        void IUpdate.ManagedFixedUpdate()
        {
        }

        void IUpdate.ManagedUpdate()
        {
            if (inputer.MenuDown())
            {
                EndMenuState();
            }
        }
    }
}