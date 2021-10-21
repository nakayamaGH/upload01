using DG.Tweening;
using UnityEngine;
using Utility;
using Utility.PostEffect;

namespace HellRoad.External
{
    public class MenuPlayerView : MonoBehaviour
    {
        private WholeBodyView wholeBody = null;
        
        public void Initalize()
        {
            Locater<IAddEventOnChangeState>.Resolve().OnBeginState += OnBeginState;
        }

        private void OnBeginState(MapSceneState state)
        {
            wholeBody = Locater<IMapPlayerView>.Resolve().WholeBody;
            if (state == MapSceneState.Menu)
            {
                transform.position = Locater<IPostEffectCamera>.Resolve().GetPosition();
                wholeBody.transform.SetParent(transform);
                MovePlayerToMe(0.25f);
            }
        }

        private void MovePlayerToMe(float duration)
        {
            wholeBody.transform.DOLocalMove(Vector3.zero, duration);
        }
    }
}