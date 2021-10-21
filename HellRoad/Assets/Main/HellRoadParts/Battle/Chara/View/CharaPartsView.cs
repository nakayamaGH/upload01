using DG.Tweening;
using HellRoad.External;
using UnityEngine;

public abstract class CharaPartsView : MonoBehaviour
{
    public abstract void ResetRotation();
    public abstract void DOFadeColor(Color color, float duration);
    public abstract void SetColor(Color color);
    public abstract void GenerateAura(EffectID id);
    public abstract void DestroyAura();

    public void BlowAway(Vector2 pos, float toque, float duration)
    {
        transform.DOLocalMove(transform.localPosition + (Vector3)pos, duration).SetEase(Ease.InOutSine);
        transform.DORotate(new Vector3(0, 0, toque), duration);
    }

    public Vector2 GetPosition() => transform.position;
}
