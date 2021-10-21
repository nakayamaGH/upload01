using System.Collections.Generic;
using UnityEngine;

namespace HellRoad
{
    [System.Serializable]
    public abstract class PartsInfo
    {
        [SerializeField] private PartsID kind = PartsID.None;
        [SerializeField] string name = null;
        [SerializeField] string about = null;
        [SerializeField] Status status = null;
        [SerializeField] List<UsableSkillID> usableSkills = new List<UsableSkillID>();
        [SerializeField] List<PassiveSkillID> passiveSkills = new List<PassiveSkillID>();
        [SerializeField] int dropSoulAmount = 0;
        [SerializeField] int dropMeatAmount = 0;

        public PartsID Kind => kind;
        public string Name => name;
        public string About => about;
        public Status Status => status;
        public List<UsableSkillID> UsableSkills => usableSkills;
        public List<PassiveSkillID> PassiveSkills => passiveSkills;
        public int DropSoulAmount => dropSoulAmount;
        public int DropMeatAmount => dropMeatAmount;

        public abstract Sprite Icon { get; }
    }
}