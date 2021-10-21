using UnityEngine;
using Utility;
using Utility.PostEffect;

namespace HellRoad.External
{
	[DefaultExecutionOrder(2)]
	public class MapBackground : MonoBehaviour, IUpdate
	{
		[SerializeField] float magnificationX = 1;
		[SerializeField] float magnificationY = 1;

		private IPostEffectCamera postEffectCamera;
		private Vector2 firstPos = Vector2.zero;

		private void Awake()
		{
			Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
			postEffectCamera = Locater<IPostEffectCamera>.Resolve();
			firstPos = transform.position;
		}

		void IUpdate.ManagedFixedUpdate()
		{
			Vector3 pos = postEffectCamera.GetPosition();

			float x = Mathf.Lerp(pos.x, firstPos.x, magnificationX);
			float y = Mathf.Lerp(pos.y, firstPos.y, magnificationY);

			transform.position = new Vector3(x, y, transform.position.z);
		}

		void IUpdate.ManagedUpdate()
		{

		}

		private void OnDestroy()
		{
			Locater<IAddUpdateInMap>.Resolve().RemoveUpdate(this, MapSceneState.Map);
		}
	}
}