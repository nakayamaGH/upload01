using Utility;

namespace HellRoad.External
{
    public interface IAddUpdateInMap
    {
        void AddUpdate(IUpdate update, MapSceneState state);
        void RemoveUpdate(IUpdate update, MapSceneState state);
    }
}