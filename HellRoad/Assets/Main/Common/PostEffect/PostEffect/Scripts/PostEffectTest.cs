using DG.Tweening;
using UnityEngine;
using Utility.PostEffect;

public class PostEffectTest : MonoBehaviour
{
    PostEffector postEffect;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            postEffect.Fade(PostEffectType.SimpleFade, 1, Color.black, PostEffector.FadeType.In);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            postEffect.Fade(PostEffectType.SimpleFade, 1, Color.black, PostEffector.FadeType.Out);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            postEffect.Fade(PostEffectType.BloodFade, 1, Color.black, PostEffector.FadeType.In);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            postEffect.Fade(PostEffectType.BloodFade, 1, Color.black, PostEffector.FadeType.Out);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            postEffect.Fade(PostEffectType.PressureFade, 0.5f, Color.white, PostEffector.FadeType.In, Ease.OutCubic);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            postEffect.Fade(PostEffectType.PressureFade, 0.5f, Color.white, PostEffector.FadeType.Out, Ease.OutCubic);
        }
    }
}
