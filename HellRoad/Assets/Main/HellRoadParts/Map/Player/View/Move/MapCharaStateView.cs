using UnityEngine;

namespace HellRoad.External
{
    public abstract class MapCharaStateView : MonoBehaviour
    {
        public abstract MapCharaState ThisState { get; }
        public abstract bool CheckChangeState(IMapCharaCore core);
        public abstract void OnStart(IMapCharaCore core);
        public abstract void OnUpdate(IMapCharaCore core);
        public abstract void OnFixedUpdate(IMapCharaCore core);
        public abstract void OnEnd(IMapCharaCore core);
    }
}