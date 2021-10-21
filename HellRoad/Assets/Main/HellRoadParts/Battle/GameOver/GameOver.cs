using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility;
using Utility.LoadScene;
using Utility.PostEffect;
using HellRoad.External.Animation;

namespace HellRoad
{
    public class GameOver
    {
        public GameOver()
        {
            
        }

        public void Play()
        {
            Locater<IAddGameAnimation>.Resolve().Add(
            new OriginalAnimation(async (anim) =>
            {
                new PostEffector().Fade(PostEffectType.GameOver, 1, Color.white, PostEffector.FadeType.Out, DG.Tweening.Ease.InQuad);
                await UniTask.Delay(TimeSpan.FromSeconds(1.1f));
                SceneLoader.LoadSceneAsync(SceneName.Title, (sc, md) =>
                {
                    new PostEffector().Fade(PostEffectType.SimpleFade, 1f, Color.white, PostEffector.FadeType.In);
                });
            }));
        }
    }
}