using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class EnemyColliderView : MonoBehaviour
    {
        [SerializeField] MapEnemyView mapEnemy = null;
		public WholeBodyView WholeBody => mapEnemy.WholeBody;
        public MapEnemyView MapEnemy => mapEnemy;

        public void HitPlayer()
        {
            mapEnemy.AddEndBattleAnim();
        }
	}
}