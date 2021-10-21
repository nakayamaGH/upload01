using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
	public class MapCharaView : MonoBehaviour, IUpdate
	{
		[SerializeField] WholeBodyView chara = null;
		[SerializeField] bool isPlayer = false;

		private MapCharaViewCore core;
		private Rigidbody2D rb;

		public WholeBodyView WholeBody => chara;

		[SerializeField] private MapCharaStateView[] stateViews = null;
		[SerializeField] private List<MapCharaStateView> nowStateViews = new List<MapCharaStateView>();
		[SerializeField] private MapCharaOnGroundView onGroundView = null;

		private void Start()
		{
			core = GetComponent<MapCharaViewCore>();
			rb = GetComponent<Rigidbody2D>();

			Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
		}

		void IUpdate.ManagedFixedUpdate()
		{
			core.Velocity = rb.velocity;

			onGroundView.OnFixedUpdate();
			if (nowStateViews.Count != 0)
				foreach (MapCharaStateView v in nowStateViews) 
					v.OnFixedUpdate(core);
		}

		void IUpdate.ManagedUpdate()
		{
			onGroundView.OnUpdate();
			foreach (MapCharaStateView stateView in stateViews)
            {
				if (stateView.CheckChangeState(core))
                {
					if (core.NowState == stateView.ThisState) break;
					//Debug.Log("ChangeState:" + core.NowState + "/" + stateView.ThisState);
					core.NowState = stateView.ThisState;

					if (nowStateViews.Count != 0)
					{
						foreach (MapCharaStateView v in nowStateViews)
						{
							v.OnEnd(core);
						}
						nowStateViews.Clear();
					}
					foreach(MapCharaStateView v in stateViews)
                    {
						if(v.ThisState == stateView.ThisState)
                        {
							nowStateViews.Add(v);
							v.OnStart(core);
						}
                    }
					break;
				}
					
			}
			if (nowStateViews.Count != 0)
				foreach(MapCharaStateView v in nowStateViews)
					v.OnUpdate(core);
		}

		public void ReturnMove(float duration, Action endAction = null)
		{
			chara.transform.SetParent(transform);
			if (rb == null) return;
			rb.simulated = false;
			chara.transform.DOLocalMove(Vector3.zero, duration).onComplete += () =>
			{
				rb.simulated = true;
				endAction?.Invoke();
			};
		}

		private void OnDestroy()
		{
			Locater<IAddUpdateInMap>.Resolve().RemoveUpdate(this, MapSceneState.Map);
		}
	}
}