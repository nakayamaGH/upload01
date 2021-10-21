using UnityEngine;

namespace HellRoad
{
    [CreateAssetMenu(menuName = "Skill/Usable")]
    public class UsableSkillInfoAsset : ScriptableObject
    {
        [SerializeField] UsableSkillInfo info = null;

        public UsableSkillInfo Info => info;
    }
}