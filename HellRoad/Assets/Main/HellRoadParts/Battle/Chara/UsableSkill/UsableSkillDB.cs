using HellRoad.UsableSkills;
using System.Collections.Generic;

namespace HellRoad
{
    public class UsableSkillDB : IGetUsableSkillFromDB
    {
        private List<UsableSkill> skills = new List<UsableSkill>();

        public UsableSkillDB()
        {
            Add(new ContinuousPunch());
            Add(new HoRenSo());
            Add(new FlyingKick());
            Add(new Namuamidabutsu());
            Add(new Wrestling());
            Add(new SelfDestruct());
            Add(new FireShot());
            Add(new Drain());
        }

        public void Add(UsableSkill skill)
        {
            skills.Add(skill);
        }

        UsableSkill IGetUsableSkillFromDB.Get(UsableSkillID id)
        {
            return skills.Find(x => x.ID == id);
        }
    }
}