using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusView : MonoBehaviour
{
    [SerializeField] UIMover uiMover = null;

    private Vector2 offset = new Vector2(1.2f, 0);

    public void Show(float duration)
    {
        gameObject.SetActive(true);
        uiMover.SetMinAnchor(offset);
        uiMover.DOMoveToDefaultMinAnchor(duration);
    }

    public void Hide(float duration)
    {
        uiMover.DOMoveToMinAnchor(offset, duration).onComplete += () =>
        gameObject.SetActive(false);
    }
}
