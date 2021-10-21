using System.Collections;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class EventStateCameraTracker : MonoBehaviour, IEventStateCameraTracker, IUpdate
    {
        [SerializeField] private Transform cameraTracker = null;
        [SerializeField] private FollowCamera followCamera = null;

        public Vector2 CameraOffset { get; private set; }
        public float CameraMoveSpeed { get; private set; }
        public float CameraSize { get; private set; }

        public Vector2 DefaultCameraOffset => Vector2.zero;
        public float DefaultCameraMoveSpeed => 500;
        public float DefaultCameraSize => 236.25f;

        private MapCharaViewCore target = null;

        private void Awake()
        {
            Locater<IAddEventOnChangeState>.Resolve().OnBeginState += OnBeginState;
        }

        private void OnBeginState(MapSceneState state)
        {
            if (state == MapSceneState.TakeAbandonedParts)
            {
                StartCoroutine(GetTarget());

                CameraOffset = DefaultCameraOffset;
                CameraMoveSpeed = DefaultCameraMoveSpeed;
                CameraSize = DefaultCameraSize;

                followCamera.CameraSize = CameraSize;
            }
        }

        public void SetCameraOffset(Vector2 offset)
        {
            CameraOffset = offset;
        }

        public void SetCameraMoveSpeed(float moveSpeed)
        {
            CameraMoveSpeed = moveSpeed;
        }

        public void SetCameraSize(float size)
        {
            CameraSize = size;

            followCamera.CameraSize = CameraSize;
        }

        private IEnumerator GetTarget()
        {
            while (Locater<IMapPlayerView>.Resolve() == null) yield return null;
            target = Locater<IMapPlayerView>.Resolve().MapCharaViewCore;

        }

        public void ManagedUpdate()
        {
            cameraTracker.transform.position = Vector3.Lerp(cameraTracker.transform.position, target.transform.position, CameraMoveSpeed * Time.deltaTime) + (Vector3)CameraOffset;
        }

        public void ManagedFixedUpdate()
        {
        }
    }

    public interface IEventStateCameraTracker
    {
        Vector2 CameraOffset { get;  }
        float CameraMoveSpeed { get;  }
        float CameraSize { get;  }

        Vector2 DefaultCameraOffset { get;}
        float DefaultCameraMoveSpeed { get; }
        float DefaultCameraSize { get; }
    }
}