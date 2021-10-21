using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace HellRoad.External
{
    public class StatusParamView : MonoBehaviour
    {
        [SerializeField] Image iconImage = null;
        [SerializeField] TMP_Text valueText = null;

        public void ChangeValue(long value)
        {
            valueText.text = value.ToString();
        }

        public void ChangeIcon(StatusParamType type)
        {
            iconImage.sprite = Locater<IGetStatusParamIcon>.Resolve().GetIcon(type);
        }
    }
}