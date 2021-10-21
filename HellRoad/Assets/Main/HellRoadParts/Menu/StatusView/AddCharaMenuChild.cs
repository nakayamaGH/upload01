using Menu;
using TMPro;
using UnityEngine;
using Utility;
using Utility.Audio;

namespace HellRoad.External
{
    public class AddCharaMenuChild : MonoBehaviour
    {
        [SerializeField] MenuChild menuChild = null;
        [SerializeField] TMP_Text costText = null;

        private void Awake()
        {
            menuChild.DecidedActionAddListener(OnDecided);
            menuChild.SelectedActionAddListener(OnSelected);
        }

		private void OnEnable()
        {
            ValidateCostText();
        }

		private void OnDecided()
        {
            AddChara();
            ValidateCostText();
        }

        private void OnSelected()
        {
            ValidateCostText();
        }

        private void ValidateCostText()
        {
            AddCharaInParty addCharaInParty = Locater<AddCharaInParty>.Resolve();
            int cost = addCharaInParty.ConsumeSoulFragments();
            costText.text = cost.ToString() + "è¡îÔ";
        }

        private void AddChara()
        {
            AddCharaInParty addCharaInParty = Locater<AddCharaInParty>.Resolve();
            if(addCharaInParty.HaveEnoughMoney() && !addCharaInParty.MaximumNumberOfParty())
            {
                Locater<IPlayAudio>.Resolve().PlaySE("Fire_1");
                addCharaInParty.AddChara();
            }
            else
            {
                Locater<IPlayAudio>.Resolve().PlaySE("Cannot");
            }
        }
    }
}