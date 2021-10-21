using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HellRoad.External
{
    public class SceneCharaAnimator : MonoBehaviour
    {
        [SerializeField] private AnimName[] anims = null;
        [SerializeField] private float animTime = 1.5f;

        private RandomizeWholeBodyView[] charas;

        private void Awake()
        {
            CharaUpdate();
        }

        private async void CharaUpdate()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            while (true)
            {
                charas = FindObjectsOfType<RandomizeWholeBodyView>();
                PlayCharasAnimation();
                await UniTask.Delay(TimeSpan.FromSeconds(animTime));
            }
        }

        private void PlayCharasAnimation()
        {
            AnimName anim = anims[Random.Range(0, anims.Length)];
            foreach (RandomizeWholeBodyView chara in charas)
            {
                PlayCharaAnimation(chara, anim);
            }
        }

        private void PlayCharaAnimation(RandomizeWholeBodyView chara, AnimName anim)
        {
            chara.PlayAnim(anim);
            chara.Randomize();
        }
    }
}