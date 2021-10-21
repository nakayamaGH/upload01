using UnityEngine;
using Utility;
using Utility.Audio;

namespace HellRoad.External
{
	public class MapSceneBGMPlayer : MonoBehaviour
	{
		[SerializeField] float fadeTime = 0.5f;

		public void Initalize(MapAsset mapAsset)
		{
			Locater<IAddEventOnChangeState>.Resolve().OnChangeState += (before, after) => BeginState_PlayBGM(before, after, mapAsset);
		}

		private void BeginState_PlayBGM(MapSceneState before, MapSceneState after, MapAsset mapAsset)
		{
			if (before == MapSceneState.Menu) return;
			switch(after)
			{
				case MapSceneState.Map:
					if (!string.IsNullOrEmpty(mapAsset.Status.MapBGM))
					{
						Locater<IPlayAudio>.Resolve().PlayBGMFade(mapAsset.Status.MapBGM, fadeTime);
					}
					break;
				case MapSceneState.Battle:
					if(!string.IsNullOrEmpty(mapAsset.Status.BattleBGM))
					{
						Locater<IPlayAudio>.Resolve().PlayBGMFade(mapAsset.Status.BattleBGM, fadeTime);
					}
					break;
			}
		}
	}
}