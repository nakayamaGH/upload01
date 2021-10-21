using DG.Tweening;
using TMPro;
using UnityEngine;

namespace HellRoad.External
{
    public class DamagePointText : MonoBehaviour
    {
        [SerializeField] TMP_Text text = null;
        [SerializeField] float riseAmount = 64;
        [SerializeField] float fadeTime = 0.5f;
        [SerializeField] float hideTime = 0.5f;

        [SerializeField] float randomOffsetX = 16;
        [SerializeField] float randomOffsetY = 16;

        public void Initalize(long pt)
        {
            text.text = pt.ToString();

            text.transform.localPosition += new Vector3(
                Random.Range(randomOffsetX, -randomOffsetX),
                Random.Range(randomOffsetY, -randomOffsetY),
                text.transform.localPosition.z);

            text.DOColor(Color.white, fadeTime);
            text.transform.DOLocalMoveY(riseAmount, fadeTime).onComplete += () =>
            {
                text.DOColor(new Color(1, 1, 1, 0), fadeTime);
            };
        }
    }
}