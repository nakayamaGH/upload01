using UnityEngine;
using Utility;
using Utility.Inputer;

namespace HellRoad.External
{
    public class ShowedMapPositionSetter : MonoBehaviour, IUpdate
    {
        [SerializeField] float moveSpeed = 100;
        private ISetMiniMapPosition setMiniMapPosition;
        private IInputer inputer = null;

        private Vector2 offset;

        private void Awake()
        {
            setMiniMapPosition = Locater<ISetMiniMapPosition>.Resolve(1);
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.ShowMap);
            inputer = Locater<IInputer>.Resolve();

            IAddEventOnChangeState addEventOnChangeState = Locater<IAddEventOnChangeState>.Resolve();
            addEventOnChangeState.OnBeginState += (state) =>
            {
                if (state == MapSceneState.Map)
                {
                    offset = Vector2.zero;
                }
            };
        }

        public void ManagedFixedUpdate()
        {
        }

        public void ManagedUpdate()
        {
            setMiniMapPosition.SetPosition((Vector2)transform.position + offset);

            offset += new Vector2(inputer.HoriMoveDir(), inputer.VertMoveDir()).normalized * moveSpeed * Time.deltaTime;
        }

        private void OnDestroy()
        {
            Locater<IAddUpdateInMap>.Resolve().RemoveUpdate(this, MapSceneState.ShowMap);
        }
    }
}