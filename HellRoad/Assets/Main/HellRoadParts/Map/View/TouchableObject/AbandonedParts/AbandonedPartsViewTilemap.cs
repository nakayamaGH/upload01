using UnityEngine;

namespace HellRoad.External
{
    public class AbandonedPartsViewTilemap : MonoBehaviour
    {
        [SerializeField] OrnamentTilemap tilemap = null;

        [SerializeField] Ornament[] partsPrefabs = null;

        public void Put(PartsType type, EnemyGroupAsset enemyGroup, bool isEnemy, int x, int y)
        {
            Ornament instance = tilemap.SetOrnament(partsPrefabs[(int)type], x, y);

            instance.GetComponent<BaseAbandonedPartsView>().DataInitalize(enemyGroup, isEnemy);
        }
    }
}