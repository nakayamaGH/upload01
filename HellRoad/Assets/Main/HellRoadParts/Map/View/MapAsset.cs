using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace HellRoad.External
{
    [CreateAssetMenu(menuName = "Map/Map")]
    public class MapAsset : ScriptableObject
    {
        [SerializeField] ClassifyRooms[] classifyRooms = null;
        [SerializeField] MapStatus mapStatus = null;

        public MapStatus Status => mapStatus;

        public RoomType[] RoomTypes => classifyRooms.Select(x => x.RoomType).ToArray();

        public MapRoomAsset[] GetRooms(RoomType type)
        {
            return Array.Find(classifyRooms, x => x.RoomType == type).GetClampedRooms();
		}

        public List<MapRoomAsset> GetAllClampedRooms()
        {
            List<MapRoomAsset> list = new List<MapRoomAsset>();
            for (int i = 0; i < classifyRooms.Length; i++)
            {
                list.AddRange(classifyRooms[i].GetClampedRooms());
            }
            return list;
        }
    }

    [Serializable]
    public class MapStatus
    {
        [SerializeField] MapID id = MapID.Underground;
        [SerializeField] TileBase wallTile = null;
        [SerializeField] TileBase platformTile = null;
        [SerializeField] TileBase ladderTile = null;
        [SerializeField] bool filledAll = false;
        [SerializeField] GameObject background = null;
        [SerializeField] GoalView[] goals = null;
        [SerializeField] Ornament[] ornaments = null;
        [SerializeField] string mapBGM = null;
        [SerializeField] string battleBGM = null;
        [SerializeField] EnemyGroupAsset[] encountEnemies = null;
        [SerializeField] EnemyGroupAsset[] abandonedEnemies = null;
        [SerializeField] EnemyGroupAsset[] otherEnemies_1 = null;
        [SerializeField] EnemyGroupAsset[] otherEnemies_2 = null;
        [SerializeField] EnemyGroupAsset[] otherEnemies_3 = null;

        public MapID ID => id;
        public TileBase WallTile => wallTile;
        public TileBase PlatformTile => platformTile;
        public TileBase LadderTile => ladderTile;
        public bool FilledAll => filledAll;
        public GameObject Background => background;
        public string MapBGM => mapBGM;
        public string BattleBGM => battleBGM;
        public GoalView GetGoals(int index) => goals[index];
        public Ornament GetOrnament(int index) => ornaments[index];

        public int EncountEnemiesKinds => encountEnemies.Length;

        public EnemyGroupAsset GetRandomEncountEnemyGroup => encountEnemies[Random.Range(0, encountEnemies.Length)];
        public EnemyGroupAsset GetAbandonedEnemyGroup(PartsType type) => abandonedEnemies[(int)type];
        public EnemyGroupAsset GetRandomEncountOtherEnemyGroup_1 => otherEnemies_1[Random.Range(0, otherEnemies_1.Length)];
        public EnemyGroupAsset GetRandomEncountOtherEnemyGroup_2 => otherEnemies_2[Random.Range(0, otherEnemies_2.Length)];
        public EnemyGroupAsset GetRandomEncountOtherEnemyGroup_3 => otherEnemies_3[Random.Range(0, otherEnemies_3.Length)];
    }

    [Serializable]
    public class ClassifyRooms
    {
        [SerializeField] RoomType roomType = RoomType.Midium;
        [SerializeField] MapRoomAsset[] rooms = null;
        [SerializeField] int min = 1;
        [SerializeField] int max = 3;

        public RoomType RoomType => roomType;
        public MapRoomAsset[] AllRooms => rooms;
        public int Min => min;
        public int Max => max;

        public MapRoomAsset[] GetClampedRooms()
        {
            int count = Random.Range(min, max);
            MapRoomAsset[] rms = new MapRoomAsset[count];
            for(int i = 0;i < count ;i++)
            {
                rms[i] = rooms[Random.Range(0, rooms.Length)];
			}
            return rms;
        }
    }
}