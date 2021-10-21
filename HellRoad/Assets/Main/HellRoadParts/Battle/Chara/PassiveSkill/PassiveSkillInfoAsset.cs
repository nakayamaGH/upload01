using UnityEngine;

namespace HellRoad
{
    [CreateAssetMenu(menuName = "Skill/Passive")]
    public class PassiveSkillInfoAsset : ScriptableObject
    {
        [SerializeField] PassiveSkillInfo info = null;

        public PassiveSkillInfo Info => info;
    }
}