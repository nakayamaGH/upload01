using UnityEngine;

namespace Utility.Inputer
{
    public class KeyInputer : IInputer
    {

        float IInputer.HoriMoveDir()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        float IInputer.VertMoveDir()
        {
            return Input.GetAxisRaw("Vertical");
        }

        bool IInputer.DecideDown()
        {
            return Input.GetButtonDown("Decide");
        }

        bool IInputer.Decide()
        {
            return Input.GetButton("Decide");
        }

        bool IInputer.DecideUp()
        {
            return Input.GetButtonUp("Decide");
        }

        bool IInputer.CancelDown()
        {
            return Input.GetButtonDown("Cancel");
        }

        bool IInputer.Cancel()
        {
            return Input.GetButton("Cancel");
        }

        bool IInputer.CancelUp()
        {
            return Input.GetButtonUp("Cancel");
        }

        bool IInputer.MenuDown()
        {
            return Input.GetButtonDown("Menu");
        }

        bool IInputer.Menu()
        {
            return Input.GetButton("Menu");
        }

        bool IInputer.MenuUp()
        {
            return Input.GetButtonUp("Menu");
        }

        bool IInputer.ShowMapDown()
        {
            return Input.GetButtonDown("ShowMap");
        }

        bool IInputer.ShowMap()
        {
            return Input.GetButton("ShowMap");
        }

        bool IInputer.ShowMapUp()
        {
            return Input.GetButtonUp("ShowMap");
        }
    }
}
