using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class CharaShowMapIconView : MonoBehaviour, IUpdate
    {
        [SerializeField] MapCharaViewCore core = null;
        [SerializeField] MapCharaView mapCharaView = null;
        [SerializeField] Sprite iconSprite = null;

        private ShowMiniMapIcon icon;

        private void Awake()
        {
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
            icon = new ShowMiniMapIcon();
            icon.AddIcon(iconSprite, mapCharaView.transform.position);
        }

        void IUpdate.ManagedFixedUpdate()
        {
        }

        void IUpdate.ManagedUpdate()
        {
            icon.SetPosition(mapCharaView.transform.position);
        }

        private void OnDestroy()
        {
            icon.RemoveIcon();
            Locater<IAddUpdateInMap>.Resolve().RemoveUpdate(this, MapSceneState.Map);
        }
    }
}