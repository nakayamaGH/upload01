using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

namespace HellRoad.External
{
	public class TitleLogoEffect : MonoBehaviour
	{
		[SerializeField] SpriteRenderer[] renderers = null;
		[SerializeField] float impactDuration = 1;
		[SerializeField] float impactDelay = 1;
		[SerializeField] float rotateTime = 1;
		[SerializeField] float rotate = 20;
		[SerializeField] Ease rotateEase = Ease.InOutCubic;
		[SerializeField] Vector2 endScale = Vector2.one * 3;

		private bool play = true;

		private void Awake()
		{
			PlayLogoAnimation();
			LogoEffectUpdate();
		}

		private void PlayLogoAnimation()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(transform.DORotate(new Vector3(0, 0, rotate), rotateTime).SetEase(rotateEase));
			sequence.Append(transform.DORotate(new Vector3(0, 0, -rotate), rotateTime).SetEase(rotateEase));
			sequence.Play().SetLoops(-1).SetLink(gameObject);
		}

		private async void LogoEffectUpdate()
		{
			while (play)
			{
				PlayLogoEffect(GetInactive());
				await UniTask.Delay(TimeSpan.FromSeconds(impactDelay));
			}
		}

		private void PlayLogoEffect(SpriteRenderer rend)
		{
			rend.gameObject.SetActive(true);
			rend.color = Color.white;
			rend.transform.localScale = transform.localScale;
			rend.transform.rotation = transform.rotation;
			rend.transform.position = transform.position;

			rend.DOColor(new Color(1, 1, 1, 0), impactDuration).SetLink(rend.gameObject).onComplete += () => rend.gameObject.SetActive(false);
			rend.transform.DOScale(new Vector3(endScale.x, endScale.y, 1), impactDuration).SetLink(rend.gameObject);
		}

		private SpriteRenderer GetInactive()
		{
			foreach (SpriteRenderer rend in renderers)
			{
				if (!rend.gameObject.activeSelf)
				{
					return rend;
				}
			}
			return null;
		}

		private void OnDestroy()
		{
			play = false;
		}
	}
}