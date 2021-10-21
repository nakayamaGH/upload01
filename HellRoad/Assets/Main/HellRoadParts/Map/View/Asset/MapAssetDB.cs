using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace HellRoad.External
{
	public class MapAssetDB : Preload, IGetMapAssetFromDB
	{
		private List<MapAsset> mapAssets = new List<MapAsset>();		

		public async override UniTask Load()
		{
			await Addressables.LoadAssetsAsync<MapAsset>("Map", (asset) => { mapAssets.Add(asset); });
		}

		public MapAsset Get(MapID id)
		{
			return mapAssets.Find(x => x.Status.ID == id);
		}
	}

	public interface IGetMapAssetFromDB
	{
		MapAsset Get(MapID id);
	}
}