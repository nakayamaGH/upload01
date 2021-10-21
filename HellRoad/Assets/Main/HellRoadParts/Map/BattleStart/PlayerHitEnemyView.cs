using HellRoad.External.Animation;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class PlayerHitEnemyView : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
		{
            EnemyColliderView enemyColView = col.GetComponent<EnemyColliderView>();
            if (enemyColView != null)
            {
                enemyColView.HitPlayer();
                IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
                addAnim.Add(new StartBattleAnimation(Locater<IMapPlayerView>.Resolve().WholeBody, enemyColView.MapEnemy.WholeBody, enemyColView.MapEnemy.EnemyGroup));
            }
		}
	}
}