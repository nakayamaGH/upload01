using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.UsableSkills
{
    public class SelfDestruct : UsableSkill
    {
        public override UsableSkillID ID => UsableSkillID.SelfDestruct;
        public override int CD => 3;

        public override void Play(BattleActionArgs args)
        {
            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
            //addAnim.Add(new BattleCharaMoveToEnemyAnimation(args.MyFrontChara.Players, 0.2f));
            addAnim.Add(new PlayEffectToCharaAnimation(args.MyFrontChara.Players, EffectID.Bomb, UnityEngine.Vector2.zero));
            addAnim.Add(new PlaySEAnimation("Bomb_2"));
            args.MyFrontChara.Attack(PhysicOrMagic.Magic, 1.8f);
            args.MyFrontChara.Dead();

            //addAnim.Add(new BattleCharaReturnAnimation(args.MyFrontChara.Players, 0f));
        }
    }
}