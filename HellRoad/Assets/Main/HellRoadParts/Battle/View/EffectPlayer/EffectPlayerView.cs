using UnityEngine;

namespace HellRoad.External
{
    public class EffectPlayerView : MonoBehaviour, IPlayEffect
    {
        [SerializeField] EffectDatasAsset datasAsset = null;

        public EffectView Play(EffectID id, Vector2 position)
        {
            EffectView prefab = datasAsset.GetData(id).Prefab;
            EffectView effect = Instantiate(prefab, position, Quaternion.identity, transform);
            effect.Play();
            return effect;
        }
    }

    public interface IPlayEffect
    {
        EffectView Play(EffectID id, Vector2 position);
    }
}