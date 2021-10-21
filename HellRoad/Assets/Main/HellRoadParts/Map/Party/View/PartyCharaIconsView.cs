using HellRoad.External.Animation;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class PartyCharaIconsView : MonoBehaviour, IPartyCharaIconsView
    {
        [SerializeField] CharaIconView[] iconViews = null;
        [SerializeField] Sprite nullCharaIcon = null;

        private IGetCharaInfoInParty getCharaInfo;

        public void SetTargetParty(IGetCharaInfoInParty getCharaInfo, IPartyEventListener eventListener)
        {
            this.getCharaInfo = getCharaInfo;

            eventListener.OnAddChara += OnAddChara;
            eventListener.OnRemoveChara += OnRemoveChara;
            eventListener.OnStickParts += OnStickParts;
            eventListener.OnSortingParty += OnSortingParty;
            eventListener.OnShiftedParty += ValidateParty;
            ValidateParty();
        }

        private void OnAddChara(int idx)
        {
            SetChara(idx);

            Locater<IAddGameAnimation>.Resolve().Add(new OriginalAnimation((anim) =>
            {
                iconViews[idx].PlayEffect();
                anim.EndAnimation = true;
            }));
        }

        private void OnRemoveChara(int idx)
        {
            NullChara(idx);
        }

        private void OnStickParts(int idx, PartsID partsID)
        {
            SetChara(idx);
        }

        private void OnSortingParty(int idx_1, int idx_2)
        {
            SetChara(idx_1);
            SetChara(idx_2);
        }

        private void ValidateParty()
        {
            for(int i = 0; i < iconViews.Length; i++)
            {
                SetChara(i);
            }
        }

        private void SetChara(int idx)
        {
            CharaInfo info = getCharaInfo.GetInfo(idx);
            if(info != null)
            {
                Locater<IAddGameAnimation>.Resolve().Add(new OriginalAnimation((anim) =>
                {
                    PartsID headID = info.GetWholeBody.GetParts(PartsType.Head);
                    Sprite headIcon = Locater<IGetPartsInfoFromDB>.Resolve().Get(headID).Icon;
                    iconViews[idx].SetIcon(headIcon);
                    anim.EndAnimation = true;
                }));
            }
            else
            {
                NullChara(idx);
            }
        }

        private void NullChara(int idx)
        {
            Locater<IAddGameAnimation>.Resolve().Add(new OriginalAnimation((anim) =>
            {
                iconViews[idx].SetIcon(nullCharaIcon);
                anim.EndAnimation = true;
            }));
        }
    }

    public interface IPartyCharaIconsView
    {
        void SetTargetParty(IGetCharaInfoInParty getCharaInfo, IPartyEventListener eventListener);
    }
}