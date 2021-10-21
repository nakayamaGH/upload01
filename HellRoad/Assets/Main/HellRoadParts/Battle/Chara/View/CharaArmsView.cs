using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class CharaArmsView : CharaPartsView
    {
        [SerializeField] SpriteRenderer[] armsRenderers = null;

        private List<EffectView> auras = new List<EffectView>();

        public void Initaize(List<Sprite> sprites, Vector2 armSeams, Vector2 arm_2_Seams)
        {
            ResetRotation();

            for (int i = 0; i < sprites.Count; i++)
            {
                armsRenderers[i].sprite = sprites[i];
            }

            Vec2ToVec3(armSeams, transform);
            Vec2ToVec3(arm_2_Seams, armsRenderers[1].transform);
        }

        private void Vec2ToVec3(Vector2 vec2, Transform tra)
        {
            tra.localPosition = new Vector3(vec2.x, vec2.y, tra.localPosition.z);
        }

        public override void ResetRotation()
        {
            transform.localEulerAngles = Vector3.zero;
            foreach (SpriteRenderer rend in armsRenderers)
                rend.transform.localEulerAngles = Vector3.zero;
        }

        public override void SetColor(Color color)
        {
            Array.ForEach(armsRenderers, x => x.color = new Color(color.r, color.g, color.b, x.color.a));
        }

        public override void DOFadeColor(Color color, float duration)
        {
            Array.ForEach(armsRenderers, x => x.DOColor(color, duration).SetLink(gameObject));
        }

        public override void GenerateAura(EffectID id)
        {
            IPlayEffect playEffect = Locater<IPlayEffect>.Resolve();
            foreach (SpriteRenderer rend in armsRenderers)
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