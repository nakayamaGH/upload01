using HellRoad.PassiveSkills;
using System.Collections.Generic;

namespace HellRoad
{
    public class PassiveSkillDB : IGetPassiveSkillFromDB
    {
        List<PassiveSkill> skills = new List<PassiveSkill>();

        public PassiveSkillDB()
        {
            Add(new Counter());
            Add(new Poison());
            Add(new AddPhysicalPower());
            Add(new AddPhysicalDefense());
            Add(new AddMagicalPower());
            Add(new AddMagicalDefense());
            Add(new AddSpeed());
            Add(new ReducePhysicalPower());
            Add(new ReducePhysicalDefense());
            Add(new ReduceMagicalPower());
            Add(new ReduceMagicalDefense());
            Add(new ReduceSpeed());
            Add(new ServiceOverTime());
            Add(new Angry());
            Add(new GoldenBrain());
            Add(new Scream());
            Add(new HiddenBlade());
            Add(new Temptation());
        }

        public void Add(PassiveSkill skill)
        {
            skills.Add(skill);
        }

        PassiveSkill IGetPassiveSkillFromDB.Get(PassiveSkillID id)
        {
            return skills.Find(x => x.ID == id);
        }
    }
}