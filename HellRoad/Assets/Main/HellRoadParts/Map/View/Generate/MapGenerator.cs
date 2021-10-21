using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace HellRoad.External
{
	public class MapGenerator : MonoBehaviour, IMapGeneratedEvent
	{
		[SerializeField] private Tilemap wallTilemap = null;
		[SerializeField] private Tilemap platformTilemap = null;
		[SerializeField] private Tilemap ladderTilemap = null;
		[SerializeField] private OrnamentTilemap ornamentsTilemap = null;
		[SerializeField] private CharaViewCreater charaCreater = null;
		[SerializeField] private AbandonedPartsViewTilemap abandonedPartsViewTilemap = null;

		[SerializeField] private GameObject generateWaiter = null;

		private List<GameObject> generatedInitalizeObjects = new List<GameObject>();
		private GameObject background = null;
		public bool generated { get; private set; } = false;

		public event Action<int[,]> OnEndGenerate;

		private void Initalize(MapAsset mapAsset)
		{
			wallTilemap.GetComponent<TilemapCollider2D>().enabled = false;
			wallTilemap.ClearAllTiles();
			platformTilemap.GetComponent<TilemapCollider2D>().enabled = false;
			platformTilemap.ClearAllTiles();
			ladderTilemap.GetComponent<TilemapCollider2D>().enabled = false;
			ladderTilemap.ClearAllTiles();
			ornamentsTilemap.Initalize();
			foreach(GameObject obj in generatedInitalizeObjects)
			{
				Destroy(obj);
			}
			generatedInitalizeObjects = new List<GameObject>();
			generateWaiter.gameObject.SetActive(true);
		}

		public void Generate(MapAsset mapAsset)
		{
			generated = false;
			StartCoroutine(GenerateCoroutine(mapAsset));
		}

		private IEnumerator GenerateCoroutine(MapAsset mapAsset)
		{
			Initalize(mapAsset);

			MapNumbersGenerator generator = new MapNumbersGenerator();
			int[,] tiles = generator.Generate(mapAsset);
			//generator.ShowResult();

			int mapWidth = tiles.GetLength(0);
			int mapHeight = tiles.GetLength(1);
			int halfWidth = mapWidth / 2;
			int halfHeight = mapHeight / 2;

			float goNextFrameTime = Time.realtimeSinceStartup + 0.01f;
			for (int y = 0; y < tiles.GetLength(1); y++)
			{
				for (int x = 0; x < tiles.GetLength(0); x++)
				{
					SetTile(x - halfWidth, y - halfHeight, (TileType)tiles[x, y], mapAsset);

					// 10msecˆÈãŒo‰ß‚µ‚½‚çŽŸƒtƒŒ[ƒ€‚Ö
					if (Time.realtimeSinceStartup >= goNextFrameTime)
					{
						yield return null;
						goNextFrameTime = Time.realtimeSinceStartup + 0.01f;
					}
				}
			}
			int[,] dest = new int[tiles.GetLength(0), tiles.GetLength(1)];
			Array.Copy(tiles, dest, tiles.Length);
			EndGenerate(dest, mapAsset);
			
			yield return null;

			foreach (GameObject o in generatedInitalizeObjects)
				o.SetActive(true);
		}

		private void EndGenerate(int[,] tiles, MapAsset mapAsset)
		{
			ChangeBackground(mapAsset);
			wallTilemap.GetComponent<TilemapCollider2D>().enabled = true;
			platformTilemap.GetComponent<TilemapCollider2D>().enabled = true;
			ladderTilemap.GetComponent<TilemapCollider2D>().enabled = true;
			generated = true;
			OnEndGenerate?.Invoke(tiles);
			generateWaiter.gameObject.SetActive(false);
		}

		private void SetTile(int x, int y, TileType tileType, MapAsset mapAsset)
		{
			switch (tileType)
			{
				case TileType.Space:
					RemoveWall(x, y);
					return;
				case TileType.Wall:
					PutWall(x, y, mapAsset);
					return;
				case TileType.Platform:
					PutPlatform(x, y, mapAsset);
					return;
				case TileType.Ladder:
					PutLadder(x, y, mapAsset);
					return;
				case TileType.Teleport:
					return;
				case TileType.Enemy:
					PutEnemy(x, y, mapAsset);
					return;
				case TileType.Door:
					return;
				case TileType.LockedDoor:
					return;
				case TileType.Key:
					return;
				case TileType.Treasure:
					PutAbandonedParts(x, y, mapAsset);
					return;
				case TileType.EntranceGate:
					EntranceGate(x, y);
					return;
				case TileType.PlatformAndLadder:
					PlatformAndLadder(x, y, mapAsset);
					return;
				case TileType.OtherEnemy_1:
					PutEnemy(x, y, mapAsset.Status.GetRandomEncountOtherEnemyGroup_1);
					return;
				case TileType.OtherEnemy_2:
					PutEnemy(x, y, mapAsset.Status.GetRandomEncountOtherEnemyGroup_2);
					return;
				case TileType.OtherEnemy_3:
					PutEnemy(x, y, mapAsset.Status.GetRandomEncountOtherEnemyGroup_3);
					return;
			}
			if (tileType >= TileType.Goal_0 && tileType <= TileType.Goal_3)
			{
				PutGoal(tileType - TileType.Goal_0, x, y, mapAsset);
				return;
			}
			else
			if (tileType >= TileType.Prefab_0 && tileType <= TileType.Prefab_16)
			{
				PutOrnament(tileType - TileType.Prefab_0, x, y, mapAsset);
				return;
			}
		}

		private void RemoveWall(int x, int y)
		{
			wallTilemap.SetTile(new Vector3Int(x, y, 0), null);
		}

		private void PutWall(int x, int y, MapAsset mapAsset)
		{
			wallTilemap.SetTile(new Vector3Int(x, y, 0), mapAsset.Status.WallTile);
		}

		private void PutPlatform(int x, int y, MapAsset mapAsset)
		{
			platformTilemap.SetTile(new Vector3Int(x, y, 0), mapAsset.Status.PlatformTile);
		}

		private void PutLadder(int x, int y, MapAsset mapAsset)
        {
			ladderTilemap.SetTile(new Vector3Int(x, y, 0), mapAsset.Status.LadderTile);
		}

		private void PutOrnament(int index, int x, int y, MapAsset mapAsset)
		{
			ornamentsTilemap.SetOrnament(mapAsset.Status.GetOrnament(index), x, y);
		}

		private void PutGoal(int index, int x, int y, MapAsset mapAsset)
		{
			GoalView goal = mapAsset.Status.GetGoals(index);
			ornamentsTilemap.SetOrnament(goal.Ornament, x, y);
		}

		private void PutEnemy(int x, int y, MapAsset mapAsset)
		{
			PutEnemy( x,  y, mapAsset.Status.GetRandomEncountEnemyGroup);
		}

		private void PutEnemy(int x, int y, EnemyGroupAsset enemy)
        {
			int posX = x * (int)wallTilemap.cellSize.x + (int)wallTilemap.cellSize.x / 2;
			int posY = y * (int)wallTilemap.cellSize.y;
			MapEnemyView enemyView = charaCreater.CreateEnemy(enemy);
			enemyView.MapCharaView.transform.position = new Vector3(posX, posY, enemyView.MapCharaView.transform.position.z);
			enemyView.gameObject.SetActive(false);
			generatedInitalizeObjects.Add(enemyView.gameObject);
		}

		private void PutAbandonedParts(int x, int y, MapAsset mapAsset)
        {
			PartsType type = (PartsType)Random.Range(0, 4);
			abandonedPartsViewTilemap.Put(type, mapAsset.Status.GetAbandonedEnemyGroup(type), true, x, y);
		}

		private void EntranceGate(int x, int y)
		{
			int posX = x * (int)wallTilemap.cellSize.x;
			int posY = y * (int)wallTilemap.cellSize.y;
			MapPlayerView playerView = charaCreater.CreatePlayer();
			playerView.MapCharaView.transform.position = new Vector3(posX, posY, playerView.MapCharaView.transform.position.z);
			playerView.gameObject.SetActive(false);
			generatedInitalizeObjects.Add(playerView.gameObject);
		}

		private void PlatformAndLadder(int x, int y, MapAsset mapAsset)
		{
			PutLadder(x, y, mapAsset);
			PutPlatform(x, y, mapAsset);
		}
		
		private void ChangeBackground(MapAsset mapAsset)
		{
			if (background != null) Destroy(background);
			background = Instantiate(mapAsset.Status.Background);
		}
	}

	public interface IMapGeneratedEvent
	{
		public event Action<int[,]> OnEndGenerate;
	}
}