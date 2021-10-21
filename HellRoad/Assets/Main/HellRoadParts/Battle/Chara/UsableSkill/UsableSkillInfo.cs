using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public class UsableSkillInfo
    {
        [SerializeField] UsableSkillID id = UsableSkillID.ContinuousPunch;
        [SerializeField] string name = null;
        [SerializeField, TextArea(1, 5)] string about = null;
        [SerializeField] Sprite icon = null;

        public UsableSkillID ID => id;
        public string Name => name;
        public string About => about;
        public Sprite Icon => icon;
    }
}