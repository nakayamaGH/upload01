using DG.Tweening;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class MapEnemyView : MonoBehaviour
	{
		[SerializeField] MapCharaView mapCharaView = null;

		private EnemyGroupAsset asset;

		public EnemyGroupAsset EnemyGroup => asset;
		public MapCharaView MapCharaView => mapCharaView;
		public WholeBodyView WholeBody => mapCharaView.WholeBody;

		private bool inCombat = false;

        public void Initalize(EnemyGroupAsset asset)
        {
			this.asset = asset;
			CharaTemplate lastChara = asset.Group[asset.Group.Count - 1];
			WholeBody.LoadCharaPartsData(lastChara.Head, lastChara.Body, lastChara.Arms, lastChara.Legs);
			Locater<IAddEventOnChangeState>.Resolve().OnBeginState += OnBeginState;
			Locater<IAddEventOnChangeState>.Resolve().OnEndState += OnEndState;
		}

		public void AddEndBattleAnim()
		{
			inCombat = true;
		}

		private void OnBeginState(MapSceneState state)
        {
			if(state == MapSceneState.Battle || state == MapSceneState.Menu)
            {
				gameObject.SetActive(false);
            }
        }

		private void OnEndState(MapSceneState state)
		{
			if(state == MapSceneState.Battle || state == MapSceneState.Menu)
            {
				if (inCombat)
				{
					mapCharaView.ReturnMove(0, () => Destroy(gameObject));
				}
				else
				{
					gameObject.SetActive(true);
				}
			}
		}

        private void OnDestroy()
		{
			Locater<IAddEventOnChangeState>.Resolve().OnBeginState -= OnBeginState;
			Locater<IAddEventOnChangeState>.Resolve().OnEndState -= OnEndState;
		}
    }
}