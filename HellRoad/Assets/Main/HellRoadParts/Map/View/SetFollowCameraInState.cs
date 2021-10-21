using UnityEngine;
using Utility;

namespace HellRoad.External
{
    [DefaultExecutionOrder(1)]
    public class SetFollowCameraInState : MonoBehaviour
    {
        [SerializeField] FollowCamera followPlayer = null;

        [SerializeField] MapSceneState targetState = MapSceneState.Map;

        private void Awake()
        {
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(followPlayer, targetState);
        }
    }
}