using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.UsableSkills
{
    public class Namuamidabutsu : UsableSkill
    {
        public override UsableSkillID ID => UsableSkillID.Namuamidabutsu;
        public override int CD => 3;

        public override void Play(BattleActionArgs args)
        {
            Players players = args.MyFrontChara.Players;

            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
            addAnim.Add(new PlayEffectToCharaAnimation(players.GetEnemy(), EffectID.Namuamidabutsu, UnityEngine.Vector2.zero));
            addAnim.Add(new BattleCharaAnimation(players, AnimName.BaseMagic, 0.3f, 0.1f));
            addAnim.Add(new PlaySEAnimation("Byun"));
            args.MyFrontChara.Attack(PhysicOrMagic.Physic, PhysicOrMagic.Magic, 0.95f);
        }
    }
}