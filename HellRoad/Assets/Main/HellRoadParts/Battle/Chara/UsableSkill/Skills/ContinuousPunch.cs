using DG.Tweening;
using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.UsableSkills
{
    public class ContinuousPunch : UsableSkill
    {
        public override UsableSkillID ID => UsableSkillID.ContinuousPunch;
        public override int CD => 3;

        public override void Play(BattleActionArgs args)
        {
            Players players = args.MyFrontChara.Players;

            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
            addAnim.Add(new BattleCharaMoveToEnemyAnimation(players, 0.2f, true, Ease.InOutCubic));
            AnimName[] animNames = new[] { AnimName.Punch_1, AnimName.Kick_2, AnimName.Punch_3, };
            float[] times = new[] { 0.3f, 0.4f, 0.3f, };
            string[] hits = new[] { "Hit_1", "Hit_2", "Hit_3" };

            addAnim.Add(new PlaySEAnimation("Punch"));
            for (int i = 0; i < 3;i++)
            {
                addAnim.Add(new BattleCharaAnimation(players, animNames[i], times[i], 0.1f));
                addAnim.Add(new PlaySEAnimation(hits[i]));
                args.MyFrontChara.Attack(PhysicOrMagic.Physic, 0.65f);
            }
            addAnim.Add(new BattleCharaReturnAnimation(players, 0.2f, false, Ease.InOutCubic));
        }
    }
}