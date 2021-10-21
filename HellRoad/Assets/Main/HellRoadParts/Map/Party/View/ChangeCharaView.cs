using System;
using UnityEngine;
using Utility;
using Utility.Inputer;

namespace HellRoad.External
{
    public class ChangeCharaView : MonoBehaviour
    {
        [SerializeField] CharaIconView frontCharaIcon = null;

        public event Action ChangedEvent;

        private IInputer inputer = null;
        private ISortParty sortingChara = null;

        private bool canShift = false;

        private void Awake()
        {
            inputer = Locater<IInputer>.Resolve();
            sortingChara = Locater<ISortParty>.Resolve();
        }

        public void InUpdate()
        {
            if (inputer.DecideDown())
            {
                sortingChara.Shift(Direction.Left);
                return;
            }

            float move = inputer.HoriMoveDir();

            if (move == 0)
            {
                if (!canShift)
                    canShift = true;
                return;
            }
            if (!canShift) return;
            if (move > 0)
            {
                sortingChara.Shift(Direction.Right);
            }
            else
            {
                sortingChara.Shift(Direction.Left);
            }

            canShift = false;
            ChangedEvent?.Invoke();
        }

        public void ValidateCharaIcon()
        {
            IGetWholeBodyProperty wholeBody = Locater<IGetCharaInfoInParty>.Resolve().GetInfo(0).GetWholeBody;
            IGetPartsInfoFromDB getInfo = Locater<IGetPartsInfoFromDB>.Resolve();
            frontCharaIcon?.SetIcon(getInfo.Get(wholeBody.GetParts(PartsType.Head)).Icon);
        }
    }
}