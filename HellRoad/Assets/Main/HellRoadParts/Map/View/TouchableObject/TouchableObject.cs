using UnityEngine;

namespace HellRoad.External
{
	public abstract class TouchableObject : MonoBehaviour, ITouchable
	{
		public abstract void Touch();
		public abstract void Exit();
	}
}