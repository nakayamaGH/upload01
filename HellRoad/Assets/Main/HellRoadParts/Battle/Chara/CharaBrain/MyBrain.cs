using Cysharp.Threading.Tasks;
using HellRoad.External.Animation;
using System.Threading.Tasks;
using Utility;

namespace HellRoad
{
    public class MyBrain : CharaBrain
    {
        public override Task<TurnActionType> GetResult(ICharasFieldGetCharaInfo field)
        {
            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();

            async Task<TurnActionType> task()
            {
                ThinkingMyBrainAnimation thinkAnim = new ThinkingMyBrainAnimation();
                addAnim.Add(thinkAnim);
                await UniTask.WaitUntil(() => thinkAnim.task != null);
                TurnActionType act = await thinkAnim.task;
                return act;
            }

            return task();
        }
    }
}