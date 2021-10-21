using UnityEngine;
using Utility;

namespace HellRoad.External.Animation
{
    public class PlayEffectToCharaAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private Players players;
        private EffectID id;
        private Vector2 offset;

        public EffectView GetView { get; private set; }

        public PlayEffectToCharaAnimation(Players players, EffectID id, Vector2 offset)
        {
            this.players = players;
            this.id = id;
            this.offset = offset;
        }

        void IGameAnimation.DoAnimation()
        {
            Vector2 position = Locater<BattleCharaAnimator>.Resolve((int)players).CharaView.GetBodyPosition + offset;
            GetView = Locater<IPlayEffect>.Resolve().Play(id, position);
            EndAnimation = true;
        }
    }
}