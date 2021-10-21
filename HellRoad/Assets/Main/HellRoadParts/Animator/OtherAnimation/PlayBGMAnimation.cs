using Utility;
using Utility.Audio;

namespace HellRoad.External.Animation
{
    public class PlayBGMAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private string audioID;
        private float fadeTime = 0;

        public PlayBGMAnimation(string audioID, float fadeTime = 0)
        {
            this.audioID = audioID;
            this.fadeTime = fadeTime;
        }

        void IGameAnimation.DoAnimation()
        {
            Locater<IPlayAudio>.Resolve().PlayBGMFade(audioID, fadeTime);
            EndAnimation = true;
        }
    }
}