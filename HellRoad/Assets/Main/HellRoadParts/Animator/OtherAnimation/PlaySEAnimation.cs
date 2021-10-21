using Utility;
using Utility.Audio;

namespace HellRoad.External.Animation
{
    public class PlaySEAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private string audioID;

        public PlaySEAnimation(string audioID)
        {
            this.audioID = audioID;
        }

        void IGameAnimation.DoAnimation()
        {
            Locater<IPlayAudio>.Resolve().PlaySE(audioID);
            EndAnimation = true;
        }
    }
}