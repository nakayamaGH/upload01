using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.UsableSkills
{
    public class FireShot : UsableSkill
    {
        public override UsableSkillID ID => UsableSkillID.FireShot;
        public override int CD => 3;

        public override void Play(BattleActionArgs args)
        {
            Players players = args.MyFrontChara.Players;
            args.MyFrontChara.Attack(PhysicOrMagic.Magic, 1.5f);
            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
            
        }
    }
}