using UnityEngine;

namespace HellRoad.External
{
    public class PlayerActionShowPartsInfoMenuChild : MonoBehaviour
    {
        [SerializeField] PlayerActionShowPartsInfo playerInfo = null;
        [SerializeField] PlayerActionShowPartsInfo enemyInfo = null;

		public void ChangeShowOrHide()
        {
            playerInfo.ChangeShowOrHide();
            enemyInfo.ChangeShowOrHide();
        }

        public void Show()
        {
            playerInfo.Show();
            enemyInfo.Show();
        }

        public void Hide()
        {
            playerInfo.Hide();
            enemyInfo.Hide();
        }
    }
}