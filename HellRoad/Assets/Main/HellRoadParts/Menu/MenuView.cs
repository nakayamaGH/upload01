using Cysharp.Threading.Tasks;
using Menu;
using System;
using UnityEngine;

namespace HellRoad.External
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] Animator animator = null;
        [SerializeField] MenuContextControler menuControler = null;
        [SerializeField] MenuContext firstMenu = null;

        public void Show()
        {
            gameObject.SetActive(true);
            animator.SetBool("Show", true);
            menuControler.ChangeControlTarget(firstMenu);
        }

        public async void Hide(float duration)
        {
            animator.SetBool("Show", false);
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            gameObject.SetActive(false);

        }
    }
}