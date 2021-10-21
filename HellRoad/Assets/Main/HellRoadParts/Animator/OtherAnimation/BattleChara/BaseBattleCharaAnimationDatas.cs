using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Audio;
using Random = UnityEngine.Random;

namespace HellRoad.External.Animation
{
    public class BaseBattleCharaAnimationDatas
    {
        public static IGameAnimation[] BeforeAttackAnimations(Players players, PhysicOrMagic physicOrMagic)
        {
            switch (physicOrMagic)
            {
                case PhysicOrMagic.Physic:
                    return BeforePhysicalAttackAnimations(players);
                case PhysicOrMagic.Magic:
                    return BeforeMagicalAttackAnimations(players);
            }
            return null;
        }

        public static IGameAnimation[] AfterAttackAnimations(Players players, PhysicOrMagic physicOrMagic)
        {
            switch (physicOrMagic)
            {
                case PhysicOrMagic.Physic:
                    return AfterPhysicalAttackAnimations(players);
                case PhysicOrMagic.Magic:
                    return AfterMagicalAttackAnimations(players);
            }
            return null;
        }

        public static IGameAnimation[] BeforePhysicalAttackAnimations(Players players)
        {
            List<IGameAnimation> anims = new List<IGameAnimation>();
            anims.Add(new PlaySEAnimation("Punch"));
            anims.Add(new BattleCharaMoveToEnemyAnimation(players, 0.2f, true, Ease.InOutCubic));
            anims.Add(new BattleCharaAnimation(players, AnimName.Punch_1, 0.3f, 0.1f, true));
            anims.Add(new WeightAnimation(0.25f));
            return anims.ToArray();
        }

        public static IGameAnimation[] AfterPhysicalAttackAnimations(Players players)
        {
            List<IGameAnimation> anims = new List<IGameAnimation>();
            anims.Add(new BattleCharaReturnAnimation(players, 0.2f, false, Ease.InOutCubic));
            return anims.ToArray();
        }

        public static IGameAnimation[] BeforeMagicalAttackAnimations(Players players)
        {
            List<IGameAnimation> anims = new List<IGameAnimation>();
            anims.Add(new BattleCharaAnimation(players, AnimName.BaseMagic, 0.5f, 0.1f, true));
            anims.Add(new WeightAnimation(0.3f));
            anims.Add(new PlaySEAnimation("MagicWave"));
            anims.Add(new PlayEffectToCharaAnimation(players.GetEnemy(), EffectID.BaseMagic, Vector2.zero));
            anims.Add(new WeightAnimation(0.1f));
            return anims.ToArray();
        }

        public static IGameAnimation[] AfterMagicalAttackAnimations(Players players)
        {
            List<IGameAnimation> anims = new List<IGameAnimation>();
            anims.Add(new WeightAnimation(0.2f));
            return anims.ToArray();
        }

        public static IGameAnimation[] DamageEffectAnimation(Players players, long value)
        {
            List<IGameAnimation> anims = new List<IGameAnimation>();
            PlayEffectToCharaAnimation playEffectAnim = new PlayEffectToCharaAnimation(players, EffectID.DamagePointText, Vector2.zero);
            OriginalAnimation effectProcessingAnim = new OriginalAnimation((anim) =>
            {
                playEffectAnim.GetView.GetComponent<DamagePointText>().Initalize(value);
                anim.EndAnimation = true;
            });
            anims.Add(new PlaySEAnimation("Damage"));
            anims.Add(playEffectAnim);
            anims.Add(effectProcessingAnim);
            return anims.ToArray();
        }

        public static IGameAnimation[] DamageAnimation(Players players, long value)
        {
            int rand = Random.Range(0, 3);
            AnimName animName = AnimName.Damage_1;
            switch (rand)
            {
                case 0:
                    animName = AnimName.Damage_1;
                    break;
                case 1:
                    animName = AnimName.Damage_2;
                    break;
                case 2:
                    animName = AnimName.Damage_3;
                    break;
            }
            List<IGameAnimation> anims = new List<IGameAnimation>();
            anims.Add(new PlayEffectToCharaAnimation(players, EffectID.Blood, Vector2.zero));
            anims.Add(new BattleCharaAnimation(players, animName, 0.5f, 0.1f));

            return anims.ToArray();
        }

        public static IGameAnimation[] HealAnimation(Players players)
        {
            List<IGameAnimation> anims = new List<IGameAnimation>();
            anims.Add(new PlayEffectToCharaAnimation(players, EffectID.Heal, Vector2.zero));
            anims.Add(new PlaySEAnimation("Heal"));
            anims.Add(new WeightAnimation(0.5f));
            return anims.ToArray();
        }

        public static IGameAnimation[] DeadAnimation(Players players, bool containsOtherChara)
        {
            List<IGameAnimation> anims = new List<IGameAnimation>();

            anims.Add(new BattleCharaAnimation(players, AnimName.Dead, 0, 0.1f, true, true));
            OriginalAnimation blowAnimation = new OriginalAnimation(async (_this) =>
            {
                Locater<IPlayAudio>.Resolve().PlaySE("Damage");

                IBattleCamera battleCamera = Locater<IBattleCamera>.Resolve();
                battleCamera.Offset = new Vector2(0, 32);
                battleCamera.Ratio = (int)players;
                battleCamera.CameraSize = 80;
                battleCamera.TrackingSpeed = 450;

                ITimeScaler timeScaler = Locater<ITimeScaler>.Resolve();
                timeScaler.DOFadeScale(0.01f, 0.1f, Ease.Unset);

                BattleCharaAnimator chara = Locater<BattleCharaAnimator>.Resolve((int)players);
                if(containsOtherChara || players == Players.Me)
                    chara.CharaView.BlowAway(100, 720, 0.3f);
                else
                    chara.CharaView.BlowAway(5, 720, 0.3f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), true);

                timeScaler.DOFadeScale(1f, 0.1f, Ease.OutSine);
                Locater<IPlayAudio>.Resolve().PlaySE("BlowAway");
                battleCamera.Offset = battleCamera.DefaultOffset;
                battleCamera.Ratio = battleCamera.DefaultRatio;
                battleCamera.CameraSize = battleCamera.DefaultCameraSize;

                await UniTask.Delay(TimeSpan.FromSeconds(0.6f));
                battleCamera.TrackingSpeed = battleCamera.DefaultTrackingSpeed;
                _this.EndAnimation = true;
            });
            anims.Add(blowAnimation);
            if(containsOtherChara)
            {
                anims.Add(new BattleCharaMoveAnimation(players, new Vector2(100, 0), 0, true));
                anims.Add(new BattleCharaReturnAnimation(players, 0.5f, true, Ease.Linear));
                anims.Add(new BattleCharaAnimation(players, AnimName.Run, 0.5f, 0.1f, true));
            }
            return anims.ToArray();
        }
    }
}