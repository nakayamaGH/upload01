using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class ShowMiniMapIcon : IShowMiniMapIcon
    {
        private IAddMiniMapIcon addMiniMapIcon;

        public Sprite Icon { get; private set; }
        public Vector2 Position { get; private set; }

        public void AddIcon(Sprite icon, Vector2 position)
        {
            this.Icon = icon;
            addMiniMapIcon = Locater<IAddMiniMapIcon>.Resolve();
            addMiniMapIcon.AddIcon(this);
            SetPosition(position);
        }

        public void SetPosition(Vector2 position)
        {
            this.Position = position;
            addMiniMapIcon.ValidateIconPosition(this);
        }

        public void RemoveIcon()
        {
            addMiniMapIcon.RemoveIcon(this);
        }
    }


    public interface IShowMiniMapIcon
    {
        Sprite Icon { get; }
        Vector2 Position { get; }
    }
}