using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.UsableSkills
{
    public class FlyingKick : UsableSkill
    {
        public override UsableSkillID ID => UsableSkillID.FlyingKick;
        public override int CD => 3;

        public override void Play(BattleActionArgs args)
        {
            Players players = args.MyFrontChara.Players;

            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
            addAnim.Add(new BattleCharaAnimation(players, AnimName.StartJump, 0.3f, 0.1f, true));
            addAnim.Add(new PlaySEAnimation("Punch"));
            addAnim.Add(new BattleCharaMoveAnimation(players, new UnityEngine.Vector2(0, 100), 0.3f, false, DG.Tweening.Ease.Unset));

            addAnim.Add(new BattleCharaAnimation(players, AnimName.Kick, 0.6f, 0.1f, true));
            addAnim.Add(new PlaySEAnimation("Punch"));
            addAnim.Add(new BattleCharaMoveToEnemyAnimation(players, 0.2f, false, DG.Tweening.Ease.Unset));
            addAnim.Add(new PlaySEAnimation("Hit_3"));

            args.MyFrontChara.Attack(PhysicOrMagic.Physic, 0.9f);
            args.EnemyFrontChara.AddBuff(new Buff(PassiveSkillID.ReducePhysicalDefense, 3));
            addAnim.Add(new BattleCharaReturnAnimation(players, 0.2f));
        }
    }
}