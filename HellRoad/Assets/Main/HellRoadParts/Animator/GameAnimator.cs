using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace HellRoad.External.Animation
{
    public class GameAnimator : MonoBehaviour, IAddGameAnimation
    {
        private List<IGameAnimation> animations = new List<IGameAnimation>();

        private bool playingAnimation = false;

		public void Add(IGameAnimation anim)
        {
            animations.Add(anim);
        }

		public void Add(params IGameAnimation[] anims)
		{
			foreach(IGameAnimation anim in anims)
                animations.Add(anim);
        }

		private void Update()
		{
			if(animations.Count != 0)
            {
                if(!playingAnimation)
                {
                    animations[0].DoAnimation();
                    playingAnimation = true;
                }

                if (animations[0].EndAnimation)
                {
                    animations.RemoveAt(0);
                    playingAnimation = false;
                }
			}
		}
	}
}