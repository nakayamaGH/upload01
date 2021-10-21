using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace HellRoad.External
{
    public class PartsInfoView : MonoBehaviour
    {
        [SerializeField] Image icon = null;
        [SerializeField] TMP_Text nameText = null;
        [SerializeField] TMP_Text aboutText = null;
        [SerializeField] PartsStatusView statusView = null;
        [SerializeField] SkillInfoView skillInfoView = null;

        public void Show(PartsInfo info)
        {
            icon.sprite = info.Icon;
            icon.SetNativeSize();
            icon.rectTransform.sizeDelta *= 2;
            nameText.text = info.Name;
            //aboutText.text = info.About;
            statusView.Show(info.Status.GetParameters);
            ShowSkillInfo(info);
        }

        public void Show(PartsID partsID)
        {
            PartsInfo info = Locater<IGetPartsInfoFromDB>.Resolve().Get(partsID);
            Show(info);
            gameObject.SetActive(true);
        }

        private void ShowSkillInfo(PartsInfo info)
        {
            string name = null;
            string about = null;
            if (info.PassiveSkills.Count != 0)
            {
                PassiveSkillInfo passiveSkillInfo = Locater<IGetPassiveSkillInfoFromDB>.Resolve().Get(info.PassiveSkills[0]);
                name = passiveSkillInfo.Name;
                about = passiveSkillInfo.About;
            }
            if (info.UsableSkills.Count != 0)
            {
                UsableSkillInfo usableSkillInfo = Locater<IGetUsableSkillInfoFromDB>.Resolve().Get(info.UsableSkills[0]);
                name = usableSkillInfo.Name;
                about = usableSkillInfo.About;
            }
            skillInfoView.Show(name, about);
        }

        public void Hide()
		{
            gameObject.SetActive(false);
		}
	}
}