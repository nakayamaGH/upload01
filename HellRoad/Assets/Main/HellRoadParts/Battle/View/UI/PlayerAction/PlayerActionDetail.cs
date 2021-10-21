using TMPro;
using UnityEngine;

public class PlayerActionDetail : MonoBehaviour
{
    [SerializeField] TMP_Text detailText = null;

    public void ShowDetail(string detail)
    {
        detailText.text = detail;
    }
}
