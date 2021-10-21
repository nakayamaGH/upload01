using UnityEngine;
using Utility;
using Utility.Audio;
using Utility.PostEffect;

namespace HellRoad.External
{
    public class TitleView : MonoBehaviour
    {
		private void Awake()
		{
			Locater<IPlayAudio>.Resolve().PlayBGMFade("Title", 0.5f);
			IPostEffectCamera cam = Locater<IPostEffectCamera>.Resolve();
			cam.Move(Vector2.zero);
			cam.SetOrthograhicSize(236.25f);
		}
	}
}