using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellRoad.External
{
    public class AddMapIconsView : MonoBehaviour, IAddMiniMapIcon
    {
        [SerializeField] CanAddMapIconView[] views = null;

        public void AddIcon(IShowMiniMapIcon icon)
        {
            foreach (CanAddMapIconView view in views)
                view.AddIcon(icon);
        }

        public void RemoveIcon(IShowMiniMapIcon icon)
        {
            foreach (CanAddMapIconView view in views)
                view.RemoveIcon(icon);
        }

        public void ValidateIconPosition(IShowMiniMapIcon icon)
        {
            foreach (CanAddMapIconView view in views)
                view.ValidateIconPosition(icon);
        }
    }

    public abstract class CanAddMapIconView : MonoBehaviour
    {
        public abstract void AddIcon(IShowMiniMapIcon icon);
        public abstract void RemoveIcon(IShowMiniMapIcon icon);
        public abstract void ValidateIconPosition(IShowMiniMapIcon icon);
    }

    public interface IAddMiniMapIcon
    {
        void AddIcon(IShowMiniMapIcon icon);
        void RemoveIcon(IShowMiniMapIcon icon);
        void ValidateIconPosition(IShowMiniMapIcon icon);
    }
}