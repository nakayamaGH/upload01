using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public class PassiveSkillInfo
    {
        [SerializeField] PassiveSkillID id = PassiveSkillID.Counter;
        [SerializeField] string name = null;
        [SerializeField, TextArea(1, 5)] string about = null;
        [SerializeField] Sprite icon = null;

        public PassiveSkillID ID => id;
        public string Name => name;
        public string About => about;
        public Sprite Icon => icon;
    }
}