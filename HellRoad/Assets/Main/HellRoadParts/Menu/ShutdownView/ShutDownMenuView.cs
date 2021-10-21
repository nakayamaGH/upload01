using Menu;
using TMPro;
using UnityEngine;

namespace HellRoad.External
{
    public class ShutDownMenuView : MonoBehaviour
    {
        [SerializeField] MenuContext baseMenu = null;
        [SerializeField] MenuContext thisMenu = null;
        [SerializeField] MenuContextControler menuControler = null;


        public void HideMenu()
        {
            gameObject.SetActive(false);
            menuControler.ChangeControlTarget(baseMenu);
        }

        public void ShowMenu()
        {
            gameObject.SetActive(true);
            menuControler.ChangeControlTarget(thisMenu);
        }
    }
}