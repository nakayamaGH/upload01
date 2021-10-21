using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace HellRoad.External
{
    public class SkillActionMenuChild : MenuChild
    {
        [SerializeField] TMP_Text nameText = null;

        public void Show(string name, string about, bool show, PlayerActionDetail detail)
        {
            nameText.text = name;
            UnityAction act = () => detail.ShowDetail(about);

            if (show)
                SelectedActionAddListener(act);
            else
                SelectedActionRemoveListener(act);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
		}
    }
}