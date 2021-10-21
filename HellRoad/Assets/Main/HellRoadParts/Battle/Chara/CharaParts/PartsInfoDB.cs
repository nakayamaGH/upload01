using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace HellRoad
{
    public class PartsInfoDB : Preload, IGetPartsInfoFromDB
    {
        List<PartsInfo> partsInfos = new List<PartsInfo>();

        PartsInfo IGetPartsInfoFromDB.Get(PartsID parts)
        {
            return partsInfos.Find(x => x.Kind == parts);
        }

        public async override UniTask Load()
        {
            await Addressables.LoadAssetsAsync<HeadPartsInfoAsset>("Head", (asset) => { partsInfos.Add(asset.Info); });
            await Addressables.LoadAssetsAsync<BodyPartsInfoAsset>("Body", (asset) => { partsInfos.Add(asset.Info); });
            await Addressables.LoadAssetsAsync<ArmsPartsInfoAsset>("Arms", (asset) => { partsInfos.Add(asset.Info); });
            await Addressables.LoadAssetsAsync<LegsPartsInfoAsset>("Legs", (asset) => { partsInfos.Add(asset.Info); });
        }
    }
}