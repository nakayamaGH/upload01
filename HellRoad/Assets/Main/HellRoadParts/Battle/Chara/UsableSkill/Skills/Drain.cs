using HellRoad.External;
using HellRoad.External.Animation;
using UnityEngine;
using Utility;

namespace HellRoad.UsableSkills
{
    public class Drain : UsableSkill
    {
        public override UsableSkillID ID => UsableSkillID.Drain;
        public override int CD => 3;

        public override void Play(BattleActionArgs args)
        {
            Players players = args.MyFrontChara.Players;
            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();

            addAnim.Add(new BattleCharaAnimation(players, AnimName.BaseMagic, 0.5f, 0.1f, true));
            addAnim.Add(new WeightAnimation(0.3f));
            addAnim.Add(new PlaySEAnimation("Drain"));
            addAnim.Add(new PlayEffectToCharaAnimation(players.GetEnemy(), EffectID.Drain, Vector2.zero));
            addAnim.Add(new WeightAnimation(0.1f));

            args.MyFrontChara.Attack(PhysicOrMagic.Physic, 0.9f);

            addAnim.Add(new WeightAnimation(0.2f));
            
            args.MyFrontChara.Heal(4);

        }
    }
}