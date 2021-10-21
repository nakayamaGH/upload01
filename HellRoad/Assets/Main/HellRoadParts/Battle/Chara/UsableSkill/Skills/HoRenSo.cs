using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad.UsableSkills
{
    public class HoRenSo : UsableSkill
    {
        public override UsableSkillID ID => UsableSkillID.HoRenSo;
        public override int CD => 3;

        public override void Play(BattleActionArgs args)
        {
            Players players = args.MyFrontChara.Players;

            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
            EffectID[] effectNames = new[] { EffectID.Ho, EffectID.Ren, EffectID.So, };
            string[] hits = new[] { "MagicWave", "MagicWave", "MagicWave" };

            addAnim.Add(new PlaySEAnimation("Punch"));
            for (int i = 0; i < 3; i++)
            {
                addAnim.Add(new BattleCharaAnimation(players, AnimName.BaseMagic, 0.3f, 0.1f));
                addAnim.Add(new PlayEffectToCharaAnimation(players.GetEnemy(), effectNames[i], UnityEngine.Vector2.zero));
                addAnim.Add(new PlaySEAnimation(hits[i]));
                args.MyFrontChara.Attack(PhysicOrMagic.Magic, 0.65f);
            }
        }
    }
}