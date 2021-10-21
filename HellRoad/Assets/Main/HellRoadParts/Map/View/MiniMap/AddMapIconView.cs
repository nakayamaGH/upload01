using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace HellRoad.External
{
    public class AddMapIconView : CanAddMapIconView
    {
        [SerializeField] MiniMapIconView minMapIconViewPrefab = null;
        [SerializeField] float mapScale = 3;
        [SerializeField] float tileSize = 32;

        private Dictionary<IShowMiniMapIcon, MiniMapIconView> icons = new Dictionary<IShowMiniMapIcon, MiniMapIconView>();


        public override void AddIcon(IShowMiniMapIcon icon)
        {
            MiniMapIconView view = Instantiate(minMapIconViewPrefab, transform);
            view.SetIcon(icon.Icon);
            view.SetPosition(icon.Position, mapScale, tileSize);
            view.gameObject.SetActive(true);
            icons.Add(icon, view);
        }

        public override void RemoveIcon(IShowMiniMapIcon icon)
        {
            if (icons[icon] is Object)
                if (icons[icon].gameObject is Object)
                    if (icons[icon].gameObject != null)
                        Destroy(icons[icon].gameObject);
            icons.Remove(icon);
        }

        public override void ValidateIconPosition(IShowMiniMapIcon icon)
        {
            icons[icon].SetPosition(icon.Position, mapScale, tileSize);
        }
    }
}