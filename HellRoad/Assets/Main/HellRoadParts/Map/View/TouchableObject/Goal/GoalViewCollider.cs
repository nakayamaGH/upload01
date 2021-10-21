using System;

namespace HellRoad.External
{
    public class GoalViewCollider : DecideableObject
	{
		public event Action DecideEvent;

		public override void Decide()
		{
			DecideEvent();
		}
	}
}