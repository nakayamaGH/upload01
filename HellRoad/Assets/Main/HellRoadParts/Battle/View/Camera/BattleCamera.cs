using DG.Tweening;
using UnityEngine;
using Utility;
using Utility.PostEffect;

namespace HellRoad.External
{
    public class BattleCamera : MonoBehaviour, IUpdate, IBattleCamera
	{
        [SerializeField] Transform cameraTracker = null;
		[SerializeField] Transform player = null;
		[SerializeField] Transform enemy = null;
		[SerializeField] Vector2 defaultOffset = new Vector2(0, 80);
		[SerializeField] float defaultRatio = 0.5f;
		[SerializeField] float defaultCameraSize = 135;
		[SerializeField] float defaultTrackingSpeed = 3;

		private IPostEffectCamera postEffectCamera;

		public Vector2 DefaultOffset => defaultOffset;
		public float DefaultRatio => defaultRatio;
		public float DefaultCameraSize => defaultCameraSize;
		public float DefaultTrackingSpeed => defaultTrackingSpeed;

		public Vector2 Offset { get; set; }
		public float Ratio { get; set; }
		public float CameraSize { get; set; }
		public float TrackingSpeed { get; set; }


		private void Awake()
		{
			postEffectCamera = Locater<IPostEffectCamera>.Resolve();
			Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Battle);
			Locater<IAddEventOnChangeState>.Resolve().OnBeginState += (state) =>
		   {
			   if (state == MapSceneState.Battle)
			   {
				   Offset = defaultOffset;
				   CameraSize = defaultCameraSize;
				   Ratio = defaultRatio;
				   TrackingSpeed = defaultTrackingSpeed;
			   }
		   };
		}

		void IUpdate.ManagedFixedUpdate()
		{
		}

		void IUpdate.ManagedUpdate()
		{
			//移動
			Vector2 target = Vector2.Lerp(player.position, enemy.position, Ratio) + Offset;
			Vector2 camPos = postEffectCamera.GetPosition();
			Vector2 lerpedPos = Vector2.Lerp(camPos, target, TrackingSpeed * Time.deltaTime);
			postEffectCamera.Move(lerpedPos);

			//カメラサイズ
			float nowSize = postEffectCamera.GetOrthograhicSize();
			postEffectCamera.SetOrthograhicSize(Mathf.Lerp(nowSize, CameraSize, TrackingSpeed * Time.deltaTime));
		}
	}

	public interface IBattleCamera
	{
		Vector2 DefaultOffset { get; }
		float DefaultRatio { get; }
		float DefaultCameraSize { get; }
		float DefaultTrackingSpeed { get; }

		Vector2 Offset { get; set; }
		float Ratio { get; set; }
		float CameraSize { get; set; }
		float TrackingSpeed { get; set; }
	}
}