using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellRoad.External
{
    public class PlatformActivater : MonoBehaviour, IPlatformActivater
    {
        [SerializeField] PlatformEffector2D platform = null;

        private int offLayerMask;
        private int onLayerMask;

        public bool IsActive { get; private set; } = true;

        private void Awake()
        {
            offLayerMask = ~(1 << LayerMask.NameToLayer("Player"));
            onLayerMask = -1;
        }

		public void Active(bool active)
        {
            IsActive = active;
            if (active)
            {
                platform.colliderMask = onLayerMask;
            }
            else
            {
                int layerMask = offLayerMask;
                platform.colliderMask = layerMask;
            }
        }
    }

    public interface IPlatformActivater
    {
        void Active(bool active);
        bool IsActive { get; }
    }
}