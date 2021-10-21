using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace HellRoad
{
    public class PassiveSkillInfoDB : Preload, IGetPassiveSkillInfoFromDB
    {
        private List<PassiveSkillInfo> infos = new List<PassiveSkillInfo>();

        public async override UniTask Load()
        {
            await Addressables.LoadAssetsAsync<PassiveSkillInfoAsset>("PassiveSkill", (asset) => { infos.Add(asset.Info); });
        }

        PassiveSkillInfo IGetPassiveSkillInfoFromDB.Get(PassiveSkillID id)
        {
            return infos.Find(x => x.ID == id);
        }
    }
}