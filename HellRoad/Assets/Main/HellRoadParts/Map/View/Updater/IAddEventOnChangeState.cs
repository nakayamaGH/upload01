using System;

namespace HellRoad.External
{
    public interface IAddEventOnChangeState
    {
        event Action<MapSceneState> OnBeginState;
        event Action<MapSceneState> OnEndState;
        event Action<MapSceneState, MapSceneState> OnChangeState;
    }
}