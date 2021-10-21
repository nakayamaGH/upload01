using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
	public class Updater : MonoBehaviour
	{
		private List<IUpdate> updates = new List<IUpdate>();

		private void Update()
		{
			foreach (IUpdate update in updates)
			{
				update.ManagedUpdate();
			}
		}

		private void FixedUpdate()
		{
			foreach (IUpdate update in updates)
			{
				update.ManagedFixedUpdate();
			}
		}

		public void AddUpdate(IUpdate update)
		{
			updates.Add(update);
		}

		public void RemoveUpdate(IUpdate update)
		{
			updates.Remove(update);
		}
	}
}