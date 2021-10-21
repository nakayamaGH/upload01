using TMPro;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class ShowSoulFragmentWalletView : MonoBehaviour
    {
        [SerializeField] TMP_Text amountText = null;

        public void Awake()
        {
            Locater<IAddReduceSoulFragmentEventListener>.Resolve().OnChangeValueAction += OnChangeValue;
            OnChangeValue(Locater<IGetSoulFragment>.Resolve().GetHaveFragments());
        }

        private void OnDestroy()
        {
            Locater<IAddReduceSoulFragmentEventListener>.Resolve().OnChangeValueAction -= OnChangeValue;
        }

        private void OnChangeValue(int amount)
        {
            if (gameObject.activeSelf)
                amountText.text = amount.ToString();
        }
    }
}