using TMPro;
using UnityEngine;

namespace HellRoad.External
{
    public class CharaBuffView : MonoBehaviour
    {
        [SerializeField] TMP_Text nameText = null;
        [SerializeField] TMP_Text durationText = null;
        [SerializeField] UIMover uiMover = null;

        public void ShowBuff(string name, int duration)
        {
            nameText.text = name;
            durationText.text = duration.ToString();

            nameText.gameObject.SetActive(true);
            durationText.gameObject.SetActive(true);
            gameObject.SetActive(true);

            //if (((RectTransform)uiMover.transform).anchorMax.x > 0) return;
            //uiMover.DOMoveToMaxAnchor(new Vector2(1, 1), 0.1f);
        }

        public void ValidateDuration(int duration)
        {
            durationText.text = duration.ToString();
        }

        public void RemoveBuff()
        {
            nameText.gameObject.SetActive(false);
            durationText.gameObject.SetActive(false);

            //uiMover.DOMoveToMaxAnchor(new Vector2(0, 1), 0.1f).onComplete += () =>
            //{
                
            //};
            gameObject.SetActive(false);
        }
    }
}