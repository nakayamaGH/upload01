using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility;
using Utility.PostEffect;

namespace HellRoad.External
{
	public class GoalView : MonoBehaviour
	{
		[SerializeField] MapID mapID = MapID.Underground;
		[SerializeField] GoalViewCollider viewCol = null;
		[SerializeField] Ornament ornament = null;
		[SerializeField] Sprite miniMapIcon = null;

		public Ornament Ornament => ornament;

		private ShowMiniMapIcon icon;

		private void Awake()
		{
			viewCol.DecideEvent += OnDecide;
			Wait();
		}

		private async void Wait()
        {
			await UniTask.WaitForFixedUpdate();
			icon = new ShowMiniMapIcon();
			icon.AddIcon(miniMapIcon, transform.position);
		}

		private async void OnDecide()
		{
			viewCol.DecideEvent -= OnDecide;
			new PostEffector().Fade(PostEffectType.SimpleFade, 0.75f, Color.black, PostEffector.FadeType.Out);
			await UniTask.Delay(TimeSpan.FromSeconds(1f));
			IMapInitalize initaliser = Locater<IMapInitalize>.Resolve();
			initaliser.Initalize(mapID);
			await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
			new PostEffector().Fade(PostEffectType.SimpleFade, 0.75f, Color.black, PostEffector.FadeType.In);
		}

        private void OnDestroy()
        {
			icon.RemoveIcon();

		}
    }
}