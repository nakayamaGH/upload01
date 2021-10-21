using Cysharp.Threading.Tasks;
using Menu;
using System;
using UnityEngine;
using Utility;
using Utility.LoadScene;
using Utility.PostEffect;

namespace HellRoad.External
{
    public class NewGameSelect : MonoBehaviour
    {
        [SerializeField] MenuChild select = null;

        private void Awake()
        {
            select.DecidedActionAddListener(NewGame);
        }

        private void NewGame()
        {
            NewGameAsync();
        }

        private async void NewGameAsync()
        {
            new PostEffector().Fade(PostEffectType.SimpleFade, 1f, Color.black, PostEffector.FadeType.Out);
            await UniTask.Delay(TimeSpan.FromSeconds(1.1f));
            SceneLoader.LoadSceneAsync(SceneName.Map, (sc, md) =>
            {
                new PostEffector().Fade(PostEffectType.SimpleFade, 1f, Color.black, PostEffector.FadeType.In);
            });
        }
    }
}