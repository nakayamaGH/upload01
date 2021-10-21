using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace HellRoad.External
{
    public class SpriteRendererAura : MonoBehaviour
    {
        [SerializeField] ParticleSystem aura = null;

        public void SetTarget(SpriteRenderer target)
        {
            return;
            var shape = aura.shape;
            shape.shapeType = ParticleSystemShapeType.SpriteRenderer;
            shape.spriteRenderer = target;
        }

        public async void Stop(float waitTime = 3)
        {
            aura.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
            Destroy(gameObject);
        }
    }
}