using System;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
	[DefaultExecutionOrder(-10)]
	public class MapSceneInitalizer : MonoBehaviour, IMapInitalize
	{
        [SerializeField] MapGenerator generator = null;
        [SerializeField] MapSceneBGMPlayer bgmPlayer = null;
		[SerializeField] MappingInMapScene mappings = null;

		private bool initalized = false;

		private void Awake()
		{
			generator.OnEndGenerate += (tiles) =>
			{
				Locater<IChangeMapState>.Resolve().ChangeState(MapSceneState.Map);
			};

			Initalize(MapID.Innermost, null);
		}

		public void Initalize(MapID id, Action endAct = null)
		{
			generator.OnEndGenerate += (tiles) => OnGenerate(endAct);
			if (!initalized)
			{
				mappings.Register();
				initalized = true;
			}

			MapAsset asset = Locater<IGetMapAssetFromDB>.Resolve().Get(id);
			generator.Generate(asset);
			bgmPlayer.Initalize(asset);
		}

		private void OnGenerate(Action act)
		{
			generator.OnEndGenerate -= (tiles) => OnGenerate(act);
			act?.Invoke();
		}
	}

	public interface IMapInitalize
	{
		void Initalize(MapID asset, Action endAct = null);
	}
}