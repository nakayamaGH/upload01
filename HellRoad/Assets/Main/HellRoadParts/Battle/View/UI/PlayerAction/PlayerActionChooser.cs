using Menu;
using UnityEngine;

namespace HellRoad.External
{
    public class PlayerActionChooser : MonoBehaviour, IPlayerActionChooser
    {
        [SerializeField] MenuContextControler controler = null;
        [SerializeField] MenuContext baseMenu = null;

        [SerializeField] MenuChild attackChild = null;
        [SerializeField] MenuChild skill_0_Child = null;
        [SerializeField] MenuChild skill_1_Child = null;
        [SerializeField] MenuChild skill_2_Child = null;
        [SerializeField] MenuChild skill_3_Child = null;
        [SerializeField] MenuChild changeChara_Left_Child = null;

        private bool decided = false;
        private TurnActionType actionType;

        bool IPlayerActionChooser.Selected => decided;
        TurnActionType IPlayerActionChooser.ActionType => actionType;

        private bool changeMenu = false;
        private bool activeBase = false;

        private void Awake()
        {
            attackChild.DecidedActionAddListener(() => Decided(TurnActionType.Attack));
            skill_0_Child.DecidedActionAddListener(() => Decided(TurnActionType.UseSkill_0));
            skill_1_Child.DecidedActionAddListener(() => Decided(TurnActionType.UseSkill_1));
            skill_2_Child.DecidedActionAddListener(() => Decided(TurnActionType.UseSkill_2));
            skill_3_Child.DecidedActionAddListener(() => Decided(TurnActionType.UseSkill_3));
            changeChara_Left_Child.DecidedActionAddListener(() => Decided(TurnActionType.ChangeFontChara_Left));
        }

        private void Decided(TurnActionType actionType)
        {
            decided = true;
            this.actionType = actionType;
            if(actionType.ConsumeTurns())
            {
                changeMenu = true;
                activeBase = false;
            }
        }

        void IPlayerActionChooser.StartSelect()
        {
            decided = false;
            changeMenu = true;
            activeBase = true;
        }

        //非同期内の処理で実行できなかったためUpdateでメニューを変更
        private void Update()
        {
            if (!changeMenu) return;

            MenuContext menu = null;
            if (activeBase) menu = baseMenu;
            controler.ChangeControlTarget(menu);
            changeMenu = false;
        }
    }

    public interface IPlayerActionChooser
    {
        public bool Selected { get; }
        public TurnActionType ActionType { get; }
        public void StartSelect();
    }
}