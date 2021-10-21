using System;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
	public class MapUpdater : MonoBehaviour, IAddUpdateInMap, IChangeMapState, IAddEventOnChangeState
	{
		[SerializeField] Updater mapUpdater = null;
		[SerializeField] Updater battleUpdater = null;
		[SerializeField] Updater menuUpdater = null;
		[SerializeField] Updater takeAbandonedPartsUpdater = null;
		[SerializeField] Updater showMapUpdater = null;

		private MapSceneState nowState = MapSceneState.First;

		public event Action<MapSceneState> OnBeginState;
		public event Action<MapSceneState> OnEndState;
		public event Action<MapSceneState, MapSceneState> OnChangeState;

		public void AddUpdate(IUpdate update, MapSceneState state)
		{
			if (state == MapSceneState.First) return;
			GetUpdateByState(state).AddUpdate(update);
		}

		public void RemoveUpdate(IUpdate update, MapSceneState state)
		{
			if (state == MapSceneState.First) return;
			GetUpdateByState(state).RemoveUpdate(update);
		}

		public void ChangeState(MapSceneState state)
		{
			OnChangeState.Invoke(nowState, state);
			EndState(nowState);
			BeginState(state);
		}

		private void BeginState(MapSceneState state)
		{
			if (state != MapSceneState.First) GetUpdateByState(state).enabled = true;
			nowState = state;
			OnBeginState?.Invoke(state);
		}

		private void EndState(MapSceneState state)
		{
			OnEndState?.Invoke(state);
			if (state == MapSceneState.First) return;
			GetUpdateByState(state).enabled = false;
		}

		private Updater GetUpdateByState(MapSceneState state)
		{
			switch (state)
			{
				case MapSceneState.Map:
					return mapUpdater;
				case MapSceneState.Battle:
					return battleUpdater;
				case MapSceneState.Menu:
					return menuUpdater;
				case MapSceneState.TakeAbandonedParts:
					return takeAbandonedPartsUpdater;
				case MapSceneState.ShowMap:
					return showMapUpdater;
			}
			return null;
		}
	}

	public enum MapSceneState
	{
		First,
		Map,
		Battle,
		Menu,
		TakeAbandonedParts,
		ShowMap,
	}
}