using Menu;
using UnityEngine;

namespace HellRoad.External
{
    public class PlayerActionChangeCharaView : MonoBehaviour
    {
        [SerializeField] MenuContextControler menuControler = null;
        [SerializeField] MenuContext thisMenu = null;
        [SerializeField] MenuContext baseMenu = null;
        [SerializeField] MenuChild[] children = null;
        [SerializeField] PlayerActionDetail detail = null;
        [SerializeField] string[] abouts = null;

        private void Awake()
        {
            for(int i = 0; i < children.Length; i++)
            {
                int a = i;
                children[i].SelectedActionAddListener(() => ShowDetail(a));
            }
        }

        public void ReturnBaseMenu()
        {
            menuControler.ChangeControlTarget(baseMenu);
        }

        public void ChangeThisMenu()
        {
            menuControler.ChangeControlTarget(thisMenu);
        }

        public void SelectedAction()
        {
            gameObject.SetActive(true);
        }

        public void DiselectedAction()
        {
            gameObject.SetActive(false);
        }

        private void ShowDetail(int i)
        {
            detail.ShowDetail(abouts[i]);
        }
    }
}