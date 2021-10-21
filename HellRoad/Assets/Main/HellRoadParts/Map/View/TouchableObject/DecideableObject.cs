using UnityEngine;
using Utility;

namespace HellRoad.External
{
	public abstract class DecideableObject : MonoBehaviour, IDecideable, ITouchable
	{
		[SerializeField] Vector2 actionAboutViewOffset = new Vector2(0, 16);
		public abstract void Decide();

		public void Exit()
		{
			Locater<IDecidableObjectActionAboutView>.Resolve().Hide();
		}

		public void Touch()
		{
			Locater<IDecidableObjectActionAboutView>.Resolve().Show(actionAboutViewOffset + (Vector2)transform.position);
		}
	}
}