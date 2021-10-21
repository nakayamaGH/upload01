using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    public class ScreenCapture : MonoBehaviour
    {
        public void Run(Action<Texture2D> _onComplete, float _quality = 1.0f)
        {
            if(gameObject.activeSelf)
                StartCoroutine(this.Take(_onComplete, _quality));
        }

        private IEnumerator Take(Action<Texture2D> _onComplete, float _quality)
        {
            yield return new WaitForEndOfFrame();

            int capture_width = (int)((float)Screen.width * _quality);
            int capture_height = (int)((float)Screen.height * _quality);

            Texture2D screenShot = new Texture2D(capture_width, capture_height, TextureFormat.RGB24, false);

            screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);

            screenShot.Apply();
            _onComplete(screenShot);
            yield break;
        }
    }
}
