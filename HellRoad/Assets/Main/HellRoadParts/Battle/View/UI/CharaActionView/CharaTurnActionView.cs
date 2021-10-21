using DG.Tweening;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class CharaTurnActionView : MonoBehaviour, ICharaTurnActionView
    {
        [SerializeField] CanvasGroup group = null;
        [SerializeField] SkillInfoView skillInfoView = null;

        public float FadeTime => 0.5f;

        Tween hideTween = null;

        private void ShowBaseAttack()
        {
            skillInfoView.Show("��{�U��", "100%�̈З͂ŕ����A�܂��͖��@�U���B\n�_���[�W�ʂ��傫���������D�悳���");
        }

        private void ShowSkill(CharaInfo charaInfo, int idx)
        {
            UsableSkillID skillID = charaInfo.GetUsableSkills.GetID(idx);
            UsableSkillInfo skillInfo = Locater<IGetUsableSkillInfoFromDB>.Resolve().Get(skillID);

            skillInfoView.Show(skillInfo.Name, skillInfo.About);
        }

        private void ShowChangeChara()
        {
            skillInfoView.Show("���Ԍ��", "");
        }

        public void Show(TurnActionType turnActionType, CharaInfo charaInfo)
        {
            if(turnActionType == TurnActionType.Attack)
            {
                ShowBaseAttack();
            }
            else
            if (turnActionType >= TurnActionType.UseSkill_0 && turnActionType <= TurnActionType.UseSkill_7)
            {
                ShowSkill(charaInfo, turnActionType - TurnActionType.UseSkill_0);
            }
            else
            if(turnActionType == TurnActionType.ChangeFontChara_Left || turnActionType == TurnActionType.ChangeFontChara_Right)
            {
                ShowChangeChara();
            }
            Show();
        }

        public void Hide()
        {
            if (!gameObject.activeSelf) return;
            group.alpha = 1;
            hideTween = group.DOFade(0, FadeTime);
            hideTween.onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        public void Show(PassiveSkillID id)
        {
            PassiveSkillInfo skillInfo = Locater<IGetPassiveSkillInfoFromDB>.Resolve().Get(id);
            skillInfoView.Show(skillInfo.Name, skillInfo.About);
            Show();
        }

        private void Show()
        {
            hideTween?.Kill();
            gameObject.SetActive(true);
            group.alpha = 0;
            group.DOFade(1, FadeTime);
        }
    }

    public interface ICharaTurnActionView
    {
        float FadeTime { get; }
        void Show(TurnActionType turnActionType, CharaInfo charaInfo);
        void Show(PassiveSkillID id);
        void Hide();
    }
}