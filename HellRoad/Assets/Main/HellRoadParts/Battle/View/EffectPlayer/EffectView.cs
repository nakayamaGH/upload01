using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HellRoad.External
{
    public class EffectView : MonoBehaviour
    {
        [SerializeField] float duration = 1;
        [SerializeField] bool infinity = false;

        public async void Play()
        {
            if (!infinity)
            {
                await UniTask.Delay((int)(duration * 1000));
                Destroy(gameObject);
            }
        }
    }
}