using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class MapEnemyLookAtPlayer : MonoBehaviour, IUpdate
    {
        [SerializeField] Transform wholeBodyTransform = null;
        
        private Transform playerTra = null;

        private void Awake()
        {
            Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);
            playerTra = Locater<IMapPlayerView>.Resolve().WholeBody.transform;
        }

        void IUpdate.ManagedFixedUpdate()
        {
        }

        void IUpdate.ManagedUpdate()
        {
            //if (playerTra == null || wholeBodyTransform == null) return;

            if (playerTra.position.x > wholeBodyTransform.position.x)
            {
                if (wholeBodyTransform.localScale.x == 1f) return;
                wholeBodyTransform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (wholeBodyTransform.localScale.x == -1f) return;
                wholeBodyTransform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private void OnDestroy()
        {
            Locater<IAddUpdateInMap>.Resolve().RemoveUpdate(this, MapSceneState.Map);
        }
    }
}