using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class MapPlayerView : MonoBehaviour, IMapPlayerView
	{
        [SerializeField] MapCharaView mapCharaView = null;
		[SerializeField] MapCharaViewCore mapCharaCoreView = null;

		public MapCharaView MapCharaView => mapCharaView;
		public MapCharaViewCore MapCharaViewCore => mapCharaCoreView;
		public WholeBodyView WholeBody => mapCharaView.WholeBody;

		private void Awake()
        {
			IAddEventOnChangeState eventListener = Locater<IAddEventOnChangeState>.Resolve();
			eventListener.OnBeginState += OnBeginState;
			eventListener.OnEndState += OnEndState;
		}

		private void OnBeginState(MapSceneState state)
		{
			if (state == MapSceneState.Map)
			{
				Active();
			}
		}

		private void OnEndState(MapSceneState state)
		{
			WholeBody.ResetRotation();
			if (state == MapSceneState.Map)
			{
				gameObject.SetActive(false);
			}
		}

		private void Active()
		{
			gameObject.SetActive(true);
			mapCharaView.ReturnMove(0.25f);
			WholeBody.ResetRotation();
		}

		private void OnDestroy()
		{
			IAddEventOnChangeState eventListener = Locater<IAddEventOnChangeState>.Resolve();
			eventListener.OnBeginState -= OnBeginState;
			eventListener.OnEndState -= OnEndState;
		}
	}

	public interface IMapPlayerView
	{
		public MapCharaView MapCharaView { get; }
		public MapCharaViewCore MapCharaViewCore { get; }
		public WholeBodyView WholeBody { get; }
	}
}