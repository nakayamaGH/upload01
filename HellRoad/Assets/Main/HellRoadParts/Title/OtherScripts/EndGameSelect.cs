using Cysharp.Threading.Tasks;
using Menu;
using System;
using UnityEngine;
using Utility.PostEffect;

namespace HellRoad.External
{
    public class EndGameSelect : MonoBehaviour
    {
        [SerializeField] MenuChild select = null;

        private void Awake()
        {
            select.DecidedActionAddListener(EndGameEvent);
        }

        private void EndGameEvent()
        {
            EndGameAsync();
        }

        private async void EndGameAsync()
        {
            new PostEffector().Fade(PostEffectType.SimpleFade, 0.5f, Color.black, PostEffector.FadeType.Out);
            await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
            Quit();
        }

        private void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
        }
    }
}