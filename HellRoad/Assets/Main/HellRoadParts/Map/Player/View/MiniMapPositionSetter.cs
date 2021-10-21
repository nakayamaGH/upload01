using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class MiniMapPositionSetter : MonoBehaviour, IUpdate
    {
        private ISetMiniMapPosition setMiniMapPosition;

        private void Awake()
        {
            setMiniMapPosition = Locater<ISetMiniMapPosition>.Resolve();
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
        }

        public void ManagedFixedUpdate()
        {
        }

        public void ManagedUpdate()
        {
            setMiniMapPosition.SetPosition(transform.position);
        }

        private void OnDestroy()
        {
            Locater<IAddUpdateInMap>.Resolve().RemoveUpdate(this, MapSceneState.Map);
        }
    }
}