using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.UsableSkills
{
    public class Wrestling : UsableSkill
    {
        public override UsableSkillID ID => UsableSkillID.Wrestling;
        public override int CD => 3;

        public override void Play(BattleActionArgs args)
        {
            Players players = args.MyFrontChara.Players;
            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();

            addAnim.Add(new PlaySEAnimation("Punch"));
            addAnim.Add(new BattleCharaAnimation(players, AnimName.Punch_1, 0.3f, 0.1f, true));
            addAnim.Add(new BattleCharaMoveToEnemyAnimation(players, 0.2f, false, DG.Tweening.Ease.Unset));
            addAnim.Add(new PlaySEAnimation("Bomb_1"));
            addAnim.Add(new PlayEffectToCharaAnimation(players.GetEnemy(), EffectID.Bomb, UnityEngine.Vector2.zero));
            addAnim.Add(new WeightAnimation(0.1f));
            args.MyFrontChara.Attack(PhysicOrMagic.Physic, 1.5f);
            addAnim.Add(new BattleCharaReturnAnimation(players, 0.2f));
        }
    }
}