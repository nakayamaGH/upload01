using UnityEngine;
using Utility;
using Utility.Inputer;

namespace HellRoad.External
{
    public class ChangeShowMapState : MonoBehaviour, IUpdate
    {
        private IInputer inputer = null;

        private void Awake()
        {
            inputer = Locater<IInputer>.Resolve();
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
        }

        void IUpdate.ManagedFixedUpdate()
        {
        }

        void IUpdate.ManagedUpdate()
        {
            if (inputer.ShowMapDown())
            {
                ChangeState();
            }
        }

        private void ChangeState()
        {
            Locater<IChangeMapState>.Resolve().ChangeState(MapSceneState.ShowMap);
        }
    }
}