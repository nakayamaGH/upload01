using Utility;

namespace HellRoad.External.Animation
{
    public class ValidateHpBarAnimation : IGameAnimation
	{
		public bool EndAnimation { get; set; } = false;

        private Players players = Players.NULL;
        private int maxHp;
        private int nowHp;
        private int beforeHp;

        public ValidateHpBarAnimation(Players players, int maxHp, int nowHp, int beforeHp)
        {
            this.players = players;
            this.maxHp = maxHp;
            this.nowHp = nowHp;
            this.beforeHp = beforeHp;
        }

        public void DoAnimation()
        {
            Locater<IHpBarView>.Resolve((int)players).Validate(maxHp, nowHp, beforeHp);
            EndAnimation = true;
        }
    }
}