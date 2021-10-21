using UnityEngine;

namespace HellRoad
{
    [CreateAssetMenu(menuName = "CharaParts/Head")]
    public class HeadPartsInfoAsset : ScriptableObject
    {
        [SerializeField] HeadPartsInfo info = null;

        public HeadPartsInfo Info => info;
    }
}