using UnityEngine;

namespace HellRoad
{
    [CreateAssetMenu(menuName = "CharaParts/Arms")]
    public class ArmsPartsInfoAsset : ScriptableObject
    {
        [SerializeField] ArmsPartsInfo info = null;

        public ArmsPartsInfo Info => info;
    }
}