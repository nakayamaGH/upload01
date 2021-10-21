using System.Collections;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class MapCameraTracker : MonoBehaviour, IUpdate
    {
        [SerializeField] private Transform cameraTracker = null;
        [SerializeField] private float moveSpeed = 500;
        [SerializeField] private float runningDistance = 96;
        [SerializeField] private float attackingDistance = 96;
        [SerializeField] private float jumpingDistance = 64;
        [SerializeField] private float climbingLadderDistance = 64;

        private MapCharaViewCore target = null;

        private void Awake()
        {
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
            Locater<IAddEventOnChangeState>.Resolve().OnBeginState += OnBeginState;
        }

        private void OnBeginState(MapSceneState state)
        {
            if(state == MapSceneState.Map)
            {
                StartCoroutine(GetTarget());
            }
		}

		void IUpdate.ManagedFixedUpdate()
		{
        }

		void IUpdate.ManagedUpdate()
        {
            if (target == null) return;
            float x = target.transform.position.x;
            float y = target.transform.position.y;
            switch(target.NowState)
            {
                case MapCharaState.RunningAndJump:
                    if(Mathf.Abs(target.Velocity.x) > 5)
                    {
                        x += target.Direction * runningDistance;
                    }
                    break;
                case MapCharaState.Attacking:
                    x += target.Direction * attackingDistance;
                    break;
                case MapCharaState.ClimbLadder:
                    y += Mathf.Clamp(target.Velocity.y, -1, 1) * climbingLadderDistance;
                    break;
            }
            if(!target.OnGround && target.NowState != MapCharaState.ClimbLadder)
            {
                y += Mathf.Clamp(target.Velocity.y, -1, 1) * jumpingDistance;
            }
            cameraTracker.position = Vector3.Lerp(cameraTracker.position, new Vector3(x, y, cameraTracker.position.z), Time.deltaTime * moveSpeed);
        }

        private IEnumerator GetTarget()
        {
            while (Locater<IMapPlayerView>.Resolve() == null) yield return null;
            target = Locater<IMapPlayerView>.Resolve().MapCharaViewCore;

        }
    }
}