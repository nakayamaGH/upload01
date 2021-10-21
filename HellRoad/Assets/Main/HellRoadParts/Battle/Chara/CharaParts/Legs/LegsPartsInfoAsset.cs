using UnityEngine;

namespace HellRoad
{
    [CreateAssetMenu(menuName = "CharaParts/Legs")]
    public class LegsPartsInfoAsset : ScriptableObject
    {
        [SerializeField] LegsPartsInfo info = null;

        public LegsPartsInfo Info => info;
    }
}