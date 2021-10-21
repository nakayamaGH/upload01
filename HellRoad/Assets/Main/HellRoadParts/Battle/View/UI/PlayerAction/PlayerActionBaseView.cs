using Menu;
using UnityEngine;

namespace HellRoad.External
{
    public class PlayerActionBaseView : MonoBehaviour
    {
        [SerializeField] MenuChild[] children;
        [SerializeField] PlayerActionDetail detail = null;
        [SerializeField, TextArea(1, 3)] string[] abouts = null;

        private void Awake()
        {
            for(int i = 0; i < children.Length;i++)
            {
                int a = i;
                children[i].SelectedActionAddListener(() => ShowDetail(a));
            }
        }

        private void ShowDetail(int i)
        {
            detail.ShowDetail(abouts[i]);
        }

        public void SelectedAction()
        {
            gameObject.SetActive(true);
        }

        public void DiselectedAction()
        {
            gameObject.SetActive(false);
        }
    }
}