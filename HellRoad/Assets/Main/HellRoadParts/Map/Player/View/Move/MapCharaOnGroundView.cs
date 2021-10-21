using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class MapCharaOnGroundView : MonoBehaviour
    {
        [SerializeField] private ContactFilter2D filter2d = new ContactFilter2D();

        private MapCharaViewCore core;
        private Rigidbody2D rb;
        private ISetOnGround setOnGround;

        private bool onGround = false;

        
        private IPlatformActivater platformActivater = null;

        [SerializeField] private float waitEndIgnorePlatformTime = 1f;

        private void Awake()
		{
            rb = GetComponent<Rigidbody2D>();
            core = GetComponent<MapCharaViewCore>();

            platformActivater = Locater<IPlatformActivater>.Resolve();

            setOnGround = core;
        }

        private void CheckOnGround()
        {
            bool isTouch = rb.IsTouching(filter2d);
            if (isTouch && !onGround)
            {
                setOnGround.SetOnGround = true;
            }

            if (!isTouch && onGround)
            {
                setOnGround.SetOnGround = false;
            }

            onGround = isTouch;
        }

		public void OnFixedUpdate()
		{
            CheckOnGround();

        }

        public void OnUpdate()
        {
            if (core.GetCharaInput.GetVertInput < 0)
            {
                IgnorePlatform();
            }
        }

        private void IgnorePlatform()
        {
            if (!platformActivater.IsActive) return;
            platformActivater.Active(false);
            WaitIgnorePlatform();
        }

        private async void WaitIgnorePlatform()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitEndIgnorePlatformTime));
            platformActivater.Active(true);
        }
    }
}