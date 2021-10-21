using Menu;
using System;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class ChangeCharaInMenuChild : MonoBehaviour, IUpdate
    {
        [SerializeField] MenuChild menuChild = null;
        [SerializeField] ChangeCharaView changeCharaView = null;
        [SerializeField] MapSceneState mapState = MapSceneState.Menu;

        public event Action ChangedEvent;

        private void Awake()
        {
            menuChild.SelectedActionAddListener(OnSelected);
            menuChild.DiselectedActionAddListener(OnDiselected);
            changeCharaView.ChangedEvent += () =>
            {
                ValidateCharaIcon();
                ChangedEvent?.Invoke();
            };
        }

        private void OnSelected()
        {
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, mapState);
        }

        private void OnDiselected()
        {
            Locater<IAddUpdateInMap>.Resolve().RemoveUpdate(this, mapState);
        }

        void IUpdate.ManagedUpdate()
        {
            changeCharaView.InUpdate();
        }

        void IUpdate.ManagedFixedUpdate()
        {
        }

        public void ValidateCharaIcon()
        {
            changeCharaView.ValidateCharaIcon();
        }
    }
}