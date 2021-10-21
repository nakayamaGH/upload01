using UnityEngine;
using Utility;
using Utility.Inputer;

namespace HellRoad.External
{
    [DefaultExecutionOrder(-1)]
    public class PlayerInputView : MonoBehaviour, IUpdate
    {
        [SerializeField] private MapCharaViewCore core = null;

        private ISetCharaInput setInput;
        private IInputer inputer;
        private int h;
        private int v;

        private void Start()
        {
            setInput = (ISetCharaInput)core.GetCharaInput;
            inputer = Locater<IInputer>.Resolve();
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
        }

		private void CheckInputInUpdate()
        {
            setInput.DecideInput = inputer.Decide();

            h = (int)inputer.HoriMoveDir();
            v = (int)inputer.VertMoveDir();
            if (h != 0 && setInput.HoriInput == 0) setInput.HoriInputDown = h; else setInput.HoriInputDown = 0;
            if (h == 0 && setInput.HoriInput != 0) setInput.HoriInputUp = setInput.HoriInput; else setInput.HoriInputUp = 0;
            if (v != 0 && setInput.VertInput == 0) setInput.VertInputDown = v; else setInput.VertInputDown = 0;
            if (v == 0 && setInput.VertInput != 0) setInput.VertInputUp = setInput.VertInput; else setInput.VertInputUp = 0;
            setInput.HoriInput = h;
            setInput.VertInput = v;

        }

		void IUpdate.ManagedUpdate()
        {
            setInput.DecideDownInput = inputer.DecideDown();
            CheckInputInUpdate();
        }

		void IUpdate.ManagedFixedUpdate()
        {
        }
	}
}