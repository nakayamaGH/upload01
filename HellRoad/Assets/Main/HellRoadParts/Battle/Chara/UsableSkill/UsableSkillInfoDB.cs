using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace HellRoad
{
    public class UsableSkillInfoDB : Preload, IGetUsableSkillInfoFromDB
    {
        private List<UsableSkillInfo> infos = new List<UsableSkillInfo>();

        public async override UniTask Load()
        {
            await Addressables.LoadAssetsAsync<UsableSkillInfoAsset>("UsableSkill", (asset) => { infos.Add(asset.Info); });
        }

        UsableSkillInfo IGetUsableSkillInfoFromDB.Get(UsableSkillID id)
        {
            return infos.Find(x => x.ID == id);
        }
    }
}