namespace HellRoad.External.Animation
{
    public interface IGameAnimation
    {
        bool EndAnimation{ get; set; }
        void DoAnimation();
    }
}