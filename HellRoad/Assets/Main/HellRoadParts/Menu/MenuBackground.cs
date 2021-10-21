using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ScreenCapture = Utility.ScreenCapture;

namespace HellRoad.External
{
    public class MenuBackground : MonoBehaviour
    {
        [SerializeField] RawImage backImage = null;
        private ScreenCapture screenCapture = null;

        public void Show()
        {
            screenCapture = GetComponent<ScreenCapture>();
            screenCapture.Run(tex => 
            {
                backImage.texture = tex;
                gameObject.SetActive(true);
            });
        }

        public void Hide()
        {
            backImage.color = Color.white;
        }
    }
}