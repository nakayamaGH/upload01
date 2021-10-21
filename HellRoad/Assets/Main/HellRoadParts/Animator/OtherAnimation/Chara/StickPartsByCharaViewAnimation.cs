namespace HellRoad.External.Animation
{
    public class StickPartsByCharaViewAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private WholeBodyView wholeBodyView;
        private PartsID id;

        public StickPartsByCharaViewAnimation(WholeBodyView wholeBodyView, PartsID id)
        {
            this.wholeBodyView = wholeBodyView;
            this.id = id;
        }

        public void DoAnimation()
        {
            wholeBodyView.LoadCharaAPartData(id);
            EndAnimation = true;
        }
    }
}