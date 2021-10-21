using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using Utility;

namespace HellRoad.External.Animation
{
    public class ThinkingMyBrainAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        public Task<TurnActionType> task { get; private set; } = null;

        public void DoAnimation()
        {
            IPlayerActionChooser actionChooser = Locater<IPlayerActionChooser>.Resolve();
            actionChooser.StartSelect();
            task = Task.Run(async () =>
            {
                await UniTask.WaitUntil(() => actionChooser.Selected);
                EndAnimation = true;
                return actionChooser.ActionType;
            });
        }
    }
}