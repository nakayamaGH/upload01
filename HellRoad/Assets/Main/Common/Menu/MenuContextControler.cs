using Utility;
using Utility.Inputer;
using System.Collections;
using UnityEngine;

namespace Menu
{
    public class MenuContextControler : MonoBehaviour
    {
        [SerializeField] MenuContext menu = null;

        private IInputer inputer = null;
        private bool canMove = true;
        private bool firstMove = true;
        private Coroutine moveCor = null;

        private void Awake()
        {
            inputer = Locater<IInputer>.Resolve();
            if (menu != null) if (menu.ChildCount != 0) menu.Select(0);
        }

        private void OnEnable()
        {
            canMove = true;
            if (this.menu != null) this.menu.Active();
        }

        public void ChangeControlTarget(MenuContext menu)
        {
            if (this.menu != null) this.menu.Inactive();
            this.menu = menu;
            if (this.menu != null) this.menu.Active();
        }

        private void Update()
        {
            if (menu == null) return;
            MoveSelectControl();
            ButtonDownControl();
        }

        private void MoveSelectControl()
        {
            if (inputer.VertMoveDir() == 0 && inputer.HoriMoveDir() == 0)
            {
                canMove = true;
                firstMove = true;
            }
            if (!canMove) return;
            if (inputer.HoriMoveDir() > 0) MoveSelect(Direction.Right); else
            if (inputer.HoriMoveDir() < 0) MoveSelect(Direction.Left);else
            if (inputer.VertMoveDir() > 0) MoveSelect(Direction.Up);else
            if (inputer.VertMoveDir() < 0) MoveSelect(Direction.Down);
        }

        private void MoveSelect(Direction dir)
        {
            if (moveCor != null)
            {
                StopCoroutine(moveCor);
                moveCor = null;
            }
            moveCor = StartCoroutine(MoveSelectCoroutine(dir));
        }

        private IEnumerator MoveSelectCoroutine(Direction dir)
        {
            canMove = false;
            menu.Move(dir);
            if (firstMove)
            {
                yield return new WaitForSeconds(0.2f);
                firstMove = false;
            }
            else
            {
                yield return new WaitForSeconds(0.075f);
            }
            canMove = true;
        }

        private void ButtonDownControl()
        {
            if (inputer.DecideDown())
            {
                menu.Decided();
            }
            if (inputer.CancelDown())
            {
                menu.Canceled();
            }
        }
    }
}