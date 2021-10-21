using UnityEngine;
using Utility;
using Utility.PostEffect;

namespace HellRoad.External
{
    public class FollowCamera : MonoBehaviour, IUpdate
    {
        [SerializeField] Transform target = null;

        private float trackingSpeedByShortDistance = 0;
        private float trackingSpeedByLongDistance = 5;
        private float rangeToTrack = 192;

        public float CameraSize { get; set; } = 236.25f;
        public float DefaultCameraSize => 236.25f;

        private IPostEffectCamera postEffectCamera;

        private float trackingSpeed;

        private void Awake()
        {
            postEffectCamera = Locater<IPostEffectCamera>.Resolve();
        }

        public void ManagedFixedUpdate()
        {
            SetPosition();
            SetCameraSize();
        }

        public void ManagedUpdate()
        {
        }


        private void SetPosition()
        {
            float distance = Vector2.Distance(postEffectCamera.GetPosition(), target.position);
            trackingSpeed = Mathf.Lerp(trackingSpeedByShortDistance, trackingSpeedByLongDistance, distance / rangeToTrack);
            postEffectCamera.Move(Vector2.Lerp(postEffectCamera.GetPosition(), target.position, Time.deltaTime * trackingSpeed));
        }

        private void SetCameraSize()
        {
            float nowSize = postEffectCamera.GetOrthograhicSize();
            postEffectCamera.SetOrthograhicSize(Mathf.Lerp(nowSize, CameraSize, trackingSpeed * Time.deltaTime));
        }
    }
}