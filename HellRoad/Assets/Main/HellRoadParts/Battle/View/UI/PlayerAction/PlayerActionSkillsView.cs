using Menu;
using UnityEngine;
using UnityEngine.Events;
using Utility;
using Utility.Audio;

namespace HellRoad.External
{
    public class PlayerActionSkillsView : MonoBehaviour
    {
        [SerializeField] MenuContextControler menuControler = null;
        [SerializeField] MenuContext thisMenu = null;
        [SerializeField] MenuContext baseMenu = null;
        [SerializeField] PlayerActionDetail detail = null;
        [SerializeField] SkillActionMenuChild[] children = null;

        private void ShowOrHide(bool show)
        {
            UsableSkillInfo[] infos = GetPlayerUsableSkillAbouts();
            thisMenu.ClearChild();
            for (int i = 0; i < 4; i++)
            {
                if (infos.Length > i)
                {
                    children[i].Show(infos[i].Name, infos[i].About, show, detail);
                    thisMenu.AddChild(children[i]);
                }
                else
                {
                    children[i].Hide();
                }
            }
        }

        private UsableSkillInfo[] GetPlayerUsableSkillAbouts()
        {
            CharaInfo charaInfo = Locater<IGetCharaInfoInParty>.Resolve().GetInfo(0);
            if (charaInfo == null) return new UsableSkillInfo[0];
            var skillIDs = Locater<IGetCharaInfoInParty>.Resolve().GetInfo(0).GetUsableSkills.GetAllID();
            IGetUsableSkillInfoFromDB getSkill = Locater<IGetUsableSkillInfoFromDB>.Resolve();
            UsableSkillInfo[] infos = new UsableSkillInfo[skillIDs.Count];
            for(int i = 0;i < skillIDs.Count ;i++)
            {
                UsableSkillInfo skillInfo = getSkill.Get(skillIDs[i]);
                infos[i] = skillInfo;
            }
            return infos;
        }

        public void ReturnBaseMenu()
        {
            menuControler.ChangeControlTarget(baseMenu);
        }

        public void ChangeThisMenu()
        {
            if(GetPlayerUsableSkillAbouts().Length == 0)
            {
                detail.ShowDetail("‚±‚Ì“÷‘Ì‚É‚ÍƒXƒLƒ‹‚ª‘¶Ý‚µ‚Ü‚¹‚ñB");
                Locater<IPlayAudio>.Resolve().PlaySE("Cannot");
            }
            else
            {
                menuControler.ChangeControlTarget(thisMenu);
            }
        }

        public void SelectedAction()
        {
            ShowOrHide(true);
            gameObject.SetActive(true);
        }

        public void DiselectedAction()
        {
            ShowOrHide(false);
            gameObject.SetActive(false);
        }
    }
}