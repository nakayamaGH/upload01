using UnityEngine;
using UnityEngine.UI;

namespace HellRoad.External
{
    public class MiniMapIconView : MonoBehaviour
    {
        [SerializeField] Image iconView = null;

        public void SetIcon(Sprite icon)
        {
            iconView.sprite = icon;
        }

        public void SetPosition(Vector2 pos, float miniMapSize, float tileSize)
        {
            transform.localPosition = pos / tileSize * miniMapSize;
        }
    }
}