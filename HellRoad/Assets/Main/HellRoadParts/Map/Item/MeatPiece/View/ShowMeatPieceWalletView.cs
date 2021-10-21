using TMPro;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class ShowMeatPieceWalletView : MonoBehaviour
    {
        [SerializeField] TMP_Text amountText = null;

        public void Awake()
        {
            Locater<IAddReduceMeatPieceEventListener>.Resolve().OnChangeValueAction += OnChangeValue;
            OnChangeValue(Locater<IGetMeatPieces>.Resolve().GetHavePieces());
        }

        private void OnDestroy()
        {
            Locater<IAddReduceMeatPieceEventListener>.Resolve().OnChangeValueAction -= OnChangeValue;
        }

        private void OnChangeValue(int amount)
        {
            if(gameObject.activeSelf)
                amountText.text = amount.ToString();
        }
    }
}