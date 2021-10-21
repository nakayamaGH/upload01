using DG.Tweening;
using TMPro;
using UnityEngine;

namespace HellRoad.External
{
	public class SignBoardCollider : TouchableObject
	{
		[SerializeField] GameObject detailObject = null;
		[SerializeField] TMP_Text[] detailText = null;
		[SerializeField] SpriteRenderer[] spriteRenderers = null;

		[SerializeField] float fadeTime = 1;

		private Sequence hideTween = null;

		public override void Touch()
		{
			if (hideTween != null) hideTween.Kill();
			foreach(SpriteRenderer rend in spriteRenderers)
				rend.color = new Color(1, 1, 1, 0);
			foreach (TMP_Text text in detailText)
				text.color = new Color(1, 1, 1, 0);
			detailObject.SetActive(true);
			foreach (SpriteRenderer rend in spriteRenderers)
				rend.DOColor(new Color(1, 1, 1, 1), fadeTime);
			foreach (TMP_Text text in detailText)
				text.DOColor(new Color(1, 1, 1, 1), fadeTime);
		}

		public override void Exit()
		{
			hideTween = DOTween.Sequence();
			foreach (SpriteRenderer rend in spriteRenderers)
				rend.color = new Color(1, 1, 1, 1);
			foreach (TMP_Text text in detailText)
				text.color = new Color(1, 1, 1, 1);
			foreach (SpriteRenderer rend in spriteRenderers)
				hideTween.Append(rend.DOColor(new Color(1, 1, 1, 0), fadeTime));
			foreach (TMP_Text text in detailText)
				hideTween.Join(text.DOColor(new Color(1, 1, 1, 0), fadeTime));
			hideTween.onComplete += () =>
			{
				detailObject.SetActive(false);
				hideTween = null;
			};
		}
	}
}