using DG.Tweening;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class CharaHeadView : CharaPartsView
    {
        [SerializeField] SpriteRenderer headRenderer = null;

        private EffectView aura = null;

        public void Initalize(Sprite headSprite, Vector2 position)
        {
			ResetRotation();

			headRenderer.sprite = headSprite;
            transform.localPosition = new Vector3(position.x, position.y, transform.localPosition.z);
        }

		public override void ResetRotation()
        {
            transform.localEulerAngles = Vector3.zero;
            headRenderer.transform.localEulerAngles = Vector3.zero;
        }

		public override void SetColor(Color color)
		{
			headRenderer.color = new Color(color.r, color.g, color.b, headRenderer.color.a);
		}

		public override void DOFadeColor(Color color, float duration)
		{
			headRenderer.DOColor(color, duration).SetLink(gameObject);
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