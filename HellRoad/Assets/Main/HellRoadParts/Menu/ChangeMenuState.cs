using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility;
using Utility.Inputer;

namespace HellRoad.External
{
    public class ChangeMenuState : MonoBehaviour, IUpdate
    {
        private IInputer inputer = null;

        private bool canChangeState = true;

        private void Awake()
        {
            inputer = Locater<IInputer>.Resolve();
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
            IAddEventOnChangeState eventListener = Locater<IAddEventOnChangeState>.Resolve();
            eventListener.OnBeginState += (state) =>
            {
                if (state == MapSceneState.Map)
                {
                    WaitAnim();
                }
            };
        }

        void IUpdate.ManagedFixedUpdate()
        {
        }

        void IUpdate.ManagedUpdate()
        {
            if(inputer.MenuDown() || inputer.CancelDown())
            {
                ChangeState();
            }
        }

        private async void WaitAnim()
        {
            canChangeState = false;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            canChangeState = true;
        }

        private void ChangeState()
        {
            if (canChangeState) 
                Locater<IChangeMapState>.Resolve().ChangeState(MapSceneState.Menu);
        }
    }
}