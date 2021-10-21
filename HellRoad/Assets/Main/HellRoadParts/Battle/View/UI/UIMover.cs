using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    public Vector2 MinAnchor { get; private set; }
    public Vector2 MaxAnchor { get; private set; }

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = (RectTransform)transform;
        MinAnchor = rectTransform.anchorMin;
        MaxAnchor = rectTransform.anchorMax;

        //DOMoveToMinAnchor(new Vector2(1.2f, 0), 1).onComplete += () => DOMoveToDefaultMinAnchor(1);
    }

    public void SetAnchor(Vector2 minAnchor, Vector2 maxAnchor)
    {
        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void SetMinAnchor(Vector2 minAnchor)
    {
        rectTransform.anchorMin = minAnchor;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void SetMaxAnchor(Vector2 maxAnchor)
    {
        rectTransform.anchorMax = maxAnchor;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public TweenerCore<Vector2, Vector2, VectorOptions> DOMoveToMinAnchor(Vector2 minAnchor, float duration)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> tween = rectTransform.DOAnchorMin(minAnchor, duration);
        tween.onUpdate += () => { rectTransform.anchoredPosition = Vector2.zero; };
        return tween;
    }

    public TweenerCore<Vector2, Vector2, VectorOptions> DOMoveToMaxAnchor(Vector2 maxAnchor, float duration)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> tween = rectTransform.DOAnchorMax(maxAnchor, duration);
        tween.onUpdate += () => { rectTransform.anchoredPosition = Vector2.zero; };
        return tween; 
    }

    public TweenerCore<Vector2, Vector2, VectorOptions> DOMoveToDefaultMinAnchor(float duration)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> tween = rectTransform.DOAnchorMin(MinAnchor, duration);
        tween.onUpdate += () => { rectTransform.anchoredPosition = Vector2.zero; };
        return tween;
    }

    public TweenerCore<Vector2, Vector2, VectorOptions> DOMoveToDefaultMaxAnchor(float duration)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> tween = rectTransform.DOAnchorMax(MaxAnchor, duration);
        tween.onUpdate += () => { rectTransform.anchoredPosition = Vector2.zero; };
        return tween;
    }
}
