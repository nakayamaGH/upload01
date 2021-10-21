using System.Collections.Generic;
using UnityEngine;

namespace HellRoad.External
{
    [CreateAssetMenu(menuName = "EffectDatas")]
    public class EffectDatasAsset : ScriptableObject
    {
        [SerializeField] private List<EffectData> datas = new List<EffectData>();
        public EffectData GetData(EffectID id) => datas.Find(x => x.ID == id);
    }

    [System.Serializable]
    public class EffectData
    {
        [SerializeField] private EffectView prefab = null;
        [SerializeField] private EffectID id;

        public EffectView Prefab => prefab;
        public EffectID ID => id;
    }

    public enum EffectID
    {
        Bomb,
        Damage_1,
        Damage_2,
        Blood,
        PurpleCharaAura,
        RedImpact,
        BaseMagic,
        Ho,
        Ren,
        So,
		Namuamidabutsu,
        PowerUp,
        PowerDown,
        Heal,
        Drain,
        DamagePointText,
	}
}
