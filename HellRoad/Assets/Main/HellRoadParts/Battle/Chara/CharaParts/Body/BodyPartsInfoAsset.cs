using UnityEngine;

namespace HellRoad
{
    [CreateAssetMenu(menuName = "CharaParts/Body")]
    public class BodyPartsInfoAsset : ScriptableObject
    {
        [SerializeField] BodyPartsInfo info = null;

        public BodyPartsInfo Info => info;
    }
}