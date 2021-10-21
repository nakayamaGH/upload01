using System.Collections.Generic;

namespace HellRoad.External.Animation
{
	public interface IAddGameAnimation
	{
		void Add(IGameAnimation anim);
		void Add(params IGameAnimation[] anims);
	}
}