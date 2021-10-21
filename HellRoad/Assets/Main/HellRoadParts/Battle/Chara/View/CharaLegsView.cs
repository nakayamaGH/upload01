using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class CharaLegsView : CharaPartsView
    {
        [SerializeField] SpriteRenderer[] legsRenderers = null;
        [SerializeField] int baseZIndex = 1;

        private int addZIndex = 0;

        private List<EffectView> auras = new List<EffectView>();

        public void Initaize(List<Sprite> sprites, Vector2 legSeams, Vector2 leg_2_Seams, Vector2 leg_3_Seams, int zIndex)
        {
            ResetRotation();

            for (int i = 0; i < sprites.Count; i++)
            {
                legsRenderers[i].sprite = sprites[i];
            }

            this.addZIndex = zIndex;

            Vec2ToVec3(legSeams, transform, addZIndex + baseZIndex);
            Vec2ToVec3(leg_2_Seams, legsRenderers[1].transform, 0);
            Vec2ToVec3(leg_3_Seams, legsRenderers[2].transform, 0);
        }

		public override void ResetRotation()
		{
            transform.localEulerAngles = Vector3.zero;
            foreach (SpriteRenderer rend in legsRenderers)
                rend.transform.localEulerAngles = Vector3.zero;
		}

		private void Vec2ToVec3(Vector2 vec2, Transform tra, int zIndex)
        {
            tra.localPosition = new Vector3(vec2.x, vec2.y, zIndex);
        }

        public override void SetColor(Color color)
        {
            Array.ForEach(legsRenderers, x => x.color = new Color(color.r, color.g, color.b, x.color.a));
        }

        public override void DOFadeColor(Color color, float duration)
        {
            Array.ForEach(legsRenderers, x => x.DOColor(color, duration).SetLink(gameObject));
        }

        public override void GenerateAura(EffectID id)
        {
            IPlayEffect playEffect = Locater<IPlayEffect>.Resolve();
            foreach (SpriteRenderer rend in legsRenderers)
            {
                EffectView aura = playEffect.Play(id, rend.transform.position);
                auras.Add(aura);
                Vector3 scale = aura.transform.localScale;
                aura.transform.SetParent(rend.transform);
                aura.transform.localScale = scale;
            }
        }

        public override void DestroyAura()
        {
            foreach (EffectView aura in auras)
            {
                Destroy(aura.gameObject);
            }
            auras.Clear();
        }
    }
}