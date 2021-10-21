using UnityEngine;
using Utility;
using Utility.Inputer;

namespace HellRoad.External
{
    public class PlayerHitTouchableObject : MonoBehaviour, IUpdate
    {
		[SerializeField] MapCharaViewCore core = null;
		private IDecideable decidable = null;
		private IInputer inputer = null;

		private bool touching = false;

		private void Awake()
		{
			touching = false;
			inputer = Locater<IInputer>.Resolve();
			Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
		}

		public void ManagedFixedUpdate()
		{
		}

		public void ManagedUpdate()
		{
			if (decidable == null) return;

			if(inputer.DecideDown())
			{
				decidable.Decide();
			}
			else
			{
			}
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (touching) return;
			IDecideable decidable = col.GetComponent<IDecideable>();
			if (decidable != null)
			{
				this.decidable = decidable;
				touching = true;
				core.HittingTouchableObject = true;
			}
			ITouchable touchable = col.GetComponent<ITouchable>();
			if(touchable != null)
			{
				touchable.Touch();
				touching = true;
			}
		}

		private void OnTriggerExit2D(Collider2D col)
		{
			if (!touching) return;
			IDecideable decidable = col.GetComponent<IDecideable>();
			if (decidable != null)
			{
				this.decidable = null;
				touching = false;
				core.HittingTouchableObject = false;
			}
			ITouchable touchable = col.GetComponent<ITouchable>();
			if (touchable != null)
			{
				touchable.Exit();
				touching = false;
			}
		}
	}
}