using DG.Tweening;
using UnityEngine;
using Utility;
using Utility.PostEffect;

namespace HellRoad.External
{
    public class MenuCamera : MonoBehaviour
    {
        [SerializeField] Transform cameraTracker = null;
        [SerializeField] float zoomSize = 135;

        private IPostEffectCamera postEffectCamera;

        public void Initalize()
        {
            postEffectCamera = Locater<IPostEffectCamera>.Resolve();

            IAddEventOnChangeState eventListener = Locater<IAddEventOnChangeState>.Resolve();
            eventListener.OnBeginState += OnBeginState;
        }

        private void OnBeginState(MapSceneState state)
        {
            if(state == MapSceneState.Menu)
            {
                float nowSize = postEffectCamera.GetOrthograhicSize();
                DOVirtual.Float(nowSize, zoomSize, 0.25f, (value) =>
                {
                    postEffectCamera.SetOrthograhicSize(value);
                });
            }
        }
    }
}