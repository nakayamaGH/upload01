namespace HellRoad.External.Animation
{
    public class ValidateWholeBodyViewAnimation : IGameAnimation
    {
        public bool EndAnimation { get; set; } = false;

        private WholeBodyView wholeBodyView;
        private IGetWholeBodyProperty wholeBody;

        public ValidateWholeBodyViewAnimation(WholeBodyView wholeBodyView, IGetWholeBodyProperty wholeBody)
        {
            this.wholeBodyView = wholeBodyView;
            this.wholeBody = wholeBody;
        }

        public void DoAnimation()
        {
            wholeBodyView.LoadCharaPartsData(
                       wholeBody.GetParts(PartsType.Head), wholeBody.GetParts(PartsType.Body),
                       wholeBody.GetParts(PartsType.Arms), wholeBody.GetParts(PartsType.Legs));
            EndAnimation = true;
        }
    }
}