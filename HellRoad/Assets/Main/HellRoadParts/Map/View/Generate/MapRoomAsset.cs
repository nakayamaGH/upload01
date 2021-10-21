using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Random = UnityEngine.Random;

namespace HellRoad.External
{
    [CreateAssetMenu(menuName = "Map/Room")]
    public class MapRoomAsset : ScriptableObject
    {
        [SerializeField] private SerializableTiles tiles = null;
        [SerializeField] private RoomOutputs[] outputs = null;

        public ReadOnlyCollection<RoomOutputs> Outputs => outputs.ToList().AsReadOnly();

        public int Width => tiles.Width;
        public int Height => tiles.Height;

        public int GetTile(int x, int y)
        {
            return tiles.Get(x, y);
		}

        public RoomOutputs DirectionToOutput(Direction dir)
        {
            return Array.Find(outputs, x => x.Direction == dir);
		}

        public RoomOutputs GetRandomOutput()
        {
            return outputs[Random.Range(0, outputs.Length)];
		}

        public Vector2Int OutputToPos(RoomOutputs output)
        {
            switch (output.Direction)
            {
                case Direction.Top:
                    return new Vector2Int(output.Offset, Height);
                case Direction.Left:
                    return new Vector2Int(0, output.Offset);
                case Direction.Bottom:
                    return new Vector2Int(output.Offset, 0);
                case Direction.Right:
                    return new Vector2Int(Width, output.Offset);
            }
            return new Vector2Int(-1, -1);
        }

        public List<Vector2Int> OutputToPoses(RoomOutputs output)
        {
            List<Vector2Int> positions = new List<Vector2Int>();
            switch (output.Direction)
            {
                case Direction.Top:
                    for (int i = 0; i < output.Size; i++) positions.Add(new Vector2Int(output.Offset - output.Size / 2 + i, Height - 1));
                    break;
                case Direction.Left:
                    for (int i = 0; i < output.Size; i++) positions.Add(new Vector2Int(0, output.Offset - output.Size / 2 + i));
                    break;
                case Direction.Bottom:
                    for (int i = 0; i < output.Size; i++) positions.Add(new Vector2Int(output.Offset - output.Size / 2 + i, 0));
                    break;
                case Direction.Right:
                    for (int i = 0; i < output.Size; i++) positions.Add(new Vector2Int(Width - 1, output.Offset - output.Size / 2 + i));
                    break;
            }
            return positions;
        }

        [System.Serializable]
        private class SerializableTileVert
        {
            [SerializeField] private int[] hori = null;

            public int Length => hori.Length;

            public SerializableTileVert(int length)
            {
                hori = new int[length];
			}

            public void Set(int x, int value)
            {
                hori[x] = value;
            }

            public int Get(int x)
            {
                return hori[x];
            }
        }

        [System.Serializable]
        private class SerializableTiles
        {
            [SerializeField] private SerializableTileVert[] tiles = null;

            public int Height => tiles.Length;
            public int Width => tiles[0].Length;

            public SerializableTiles(int width, int height)
            {
                tiles = new SerializableTileVert[height];
                for(int i = 0; i < tiles.Length; i++)
                {
                    tiles[i] = new SerializableTileVert(width);
				}
			}

            public void Set(int x, int y, int value)
            {
                tiles[y].Set(x, value);
			}

            public int Get(int x, int y) => tiles[y].Get(x);
		}

#if UNITY_EDITOR
        [CustomEditor(typeof(MapRoomAsset))]
        private class MapRoomEditor : Editor
        {
			public override void OnInspectorGUI()
			{
				base.OnInspectorGUI();
                TextAsset csv;
                csv = (TextAsset)EditorGUILayout.ObjectField("Input => .txt", null, typeof(TextAsset), true);
                if(csv != null)
                {
                    LoadCSV(csv.text);
                }
			}

            private void LoadCSV(string tilesStr)
            {
                string[] vert = tilesStr.Split('\n');
                int Width = vert[0].Split(',').Length;
                int Height = vert.Length;

                SerializableTiles tiles = new SerializableTiles(Width, Height);

                for (int y = 0; y < Height; y++)
                {
                    string[] hori = vert[y].Split(',');
                    for (int x = 0; x < Width; x++)
                    {
                        tiles.Set(x, Height - y - 1, int.Parse(hori[x]));
                    }
                }
                MapRoomAsset asset = (MapRoomAsset)target;
                asset.tiles = tiles;
                EditorUtility.SetDirty(asset);
            }
		}
#endif
    }

    [System.Serializable]
    public class RoomOutputs
    {
        [SerializeField] private Direction direction;
        [SerializeField] private int offset = 0;
        [SerializeField] private int size = 0;

        public Direction Direction => direction;
        public int Offset => offset;
        public int Size => size;
    }
}