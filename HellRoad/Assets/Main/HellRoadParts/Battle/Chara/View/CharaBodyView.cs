using DG.Tweening;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class CharaBodyView : CharaPartsView
    {
        [SerializeField] SpriteRenderer bodyRenderer = null;
        [SerializeField] SpriteRenderer backBodyRenderer = null;

		private EffectView aura = null;

		public void Initalize(Sprite bodySprite, Sprite backBody)
		{
			ResetRotation();

			bodyRenderer.sprite = bodySprite;
			backBodyRenderer.sprite = backBody;
			transform.localPosition = Vector3.zero;
		}

		public override void ResetRotation()
		{
            transform.localEulerAngles = Vector3.zero;
			bodyRenderer.transform.localEulerAngles = Vector3.zero;
		}

		public override void SetColor(Color color)
		{
			bodyRenderer.color = new Color(color.r, color.g, color.b, bodyRenderer.color.a);
		}

		public override void DOFadeColor(Color color, float duration)
		{
			bodyRenderer.DOColor(color, duration).SetLink(gameObject);
		}

        public override void GenerateAura(EffectID id)
		{
			aura = Locater<IPlayEffect>.Resolve().Play(id, transform.position);
			Vector3 scale = aura.transform.localScale;
			aura.transform.SetParent(transform);
			aura.transform.localScale = scale;
		}

        public override void DestroyAura()
        {
			Destroy(aura.gameObject);
			aura = null;
        }
    }
}