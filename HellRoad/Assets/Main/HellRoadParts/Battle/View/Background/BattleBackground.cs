using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScreenCapture = Utility.ScreenCapture;

namespace HellRoad.External
{
	public class BattleBackground : MonoBehaviour
	{
		[SerializeField] float showTime = 1;
		[SerializeField] float hideTime = 1;
		[SerializeField] Color color = new Color();
		private ScreenCapture screenCapture = null;
		private RawImage backImage = null;

		private bool alreadySetup = false;
		
		private void Setup()
		{
			alreadySetup = true;
			screenCapture = GetComponent<ScreenCapture>();
			backImage = GetComponent<RawImage>();
		}

		public void Show()
		{
			if (!alreadySetup) Setup();
			gameObject.SetActive(true);
			backImage.color = new Color(1, 1, 1, 0);
			screenCapture.Run((tex) => 
			{
				backImage.texture = tex;
				backImage.DOColor(color, showTime);
			}, 1);
		}

		public void Hide()
		{
			Color c = backImage.color;
			backImage.DOColor(new Color(c.r, c.g, c.b, 0), hideTime).onComplete += () =>
			{
				gameObject.SetActive(false);
			};
		}
	}
}
