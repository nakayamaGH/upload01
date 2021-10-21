using DG.Tweening;
using Menu;
using UnityEngine;

namespace HellRoad.External
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] MenuContext baseMenu = null;
        [SerializeField] MenuContext thisMenu = null;
        [SerializeField] MenuContextControler menuControler = null;
        [SerializeField] CanvasGroup group = null;

        public void HideMenu()
        {
            group.DOFade(0, 0.25f).onComplete += () => 
            {
                gameObject.SetActive(false);
            };
            menuControler.ChangeControlTarget(baseMenu);
        }

        public void ShowMenu()
        {
            gameObject.SetActive(true);
            group.DOFade(1, 0.25f);
            menuControler.ChangeControlTarget(thisMenu);
        }
    }
}