using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace HellRoad
{
    [CreateAssetMenu(menuName = "EnemyGroup")]
    public class EnemyGroupAsset : ScriptableObject
    {
        [SerializeField] CharaTemplate[] group = null;

        public ReadOnlyCollection<CharaTemplate> Group => group.ToList().AsReadOnly();
    }
}