using TMPro;
using UnityEngine;

namespace HellRoad.External
{
    public class SkillInfoView : MonoBehaviour
    {
        [SerializeField] TMP_Text nameText = null;
        [SerializeField] TMP_Text aboutText = null;

        public void Show(string name, string about)
        {
            nameText.text = name;
            aboutText.text = about;
		}
    }
}