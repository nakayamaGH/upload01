using UnityEditor;
using UnityEngine;

namespace MapEditor
{
    public class MapEditorWindow : EditorWindow
    {
        [SerializeField] private MapEditorSettings settings; // データ
        [SerializeField] private int mapWidth = 0;
        [SerializeField] private int mapHeight = 0;

        private Vector2 scrollPosition = Vector2.zero;

        [MenuItem("Window/MapEditor")]
        static void Open()
        {
            GetWindow<MapEditorWindow>("MapEditorWindow");
        }

        void OnGUI()
        {
            if (settings == null) settings = LoadSettings();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            DrawMap();
            ChangeTileBrushIdx();
            ChangeTileSize();
            ResizeMapSize();
            LoadFile();
            ChangeTileTexSize();
            ShowSaveButton();
            EditorGUILayout.EndScrollView();
        }

        private MapEditorSettings LoadSettings()
        {
            string filePath = "Assets/Editor/MapEditor/MapEditorSettings.asset";
            MapEditorSettings settings = (MapEditorSettings)AssetDatabase.
                LoadAssetAtPath(filePath, typeof(MapEditorSettings));
            if (settings == null)
            {
                settings = CreateInstance<MapEditorSettings>();
                AssetDatabase.CreateAsset(settings, filePath);
                AssetDatabase.Refresh();
            }
            mapWidth = settings.mapWidth;
            mapHeight = settings.mapHeight;
            return settings;
        }

        private void ShowSaveButton()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("SaveToFile", GUILayout.MaxWidth(200), GUILayout.MaxHeight(50)))
            {
                EditorUtility.SetDirty(settings);
                MapCSVCreater.Create(settings.mapData);
                AssetDatabase.Refresh();
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
        }

        private void ChangeTileBrushIdx()
        {
            EditorGUILayout.LabelField("タイルブラシ変更");
            settings.minTileKind = EditorGUILayout.IntField("MinTileKind", settings.minTileKind, GUILayout.MaxWidth(200));
            settings.maxTileKind = EditorGUILayout.IntField("MaxTileKind", settings.maxTileKind, GUILayout.MaxWidth(200));
            settings.nowTileIdx = (int)(TileType)EditorGUILayout.EnumPopup("NowTileIdx", (TileType)settings.nowTileIdx, GUILayout.MaxWidth(400));
            //settings.nowTileIdx = EditorGUILayout.IntSlider("NowTileIdx", settings.nowTileIdx, settings.minTileKind, settings.maxTileKind, GUILayout.MaxWidth(400));
            settings.brushType = (BrushType)EditorGUILayout.EnumPopup("BrushType", settings.brushType, GUILayout.MaxWidth(300));
            EditorGUILayout.Space(16);
        }

        private void ChangeTileSize()
        {
            EditorGUILayout.LabelField("マップサイズ");

            mapWidth = EditorGUILayout.IntField("MapWidth", mapWidth, GUILayout.MaxWidth(200));
            mapHeight = EditorGUILayout.IntField("MapHeight", mapHeight, GUILayout.MaxWidth(200));
        }

        private void ResizeMapSize()
        {
            if (GUILayout.Button("ResetMap", GUILayout.MaxWidth(200)))
            {
                Resize();
            }
            EditorGUILayout.Space(16);
        }

        public void Resize()
        {
            settings.mapWidth = mapWidth;
            settings.mapHeight = mapHeight;
            ValueList[] valueList = new ValueList[mapHeight];
            for (int y = 0; y < valueList.Length; y++)
            {
                valueList[y] = new ValueList(mapWidth);
                for(int x = 0; x < valueList[y].values.Length;x++)
                {
                    if (settings.mapData.Length <= y) break;
                    if (settings.mapData[y].values.Length <= x) break;
                    valueList[y].values[x] = settings.mapData[y].values[x];
                }
            }
            settings.mapData = valueList;
        }

        public void LoadFile()
        {
            TextAsset csv = (TextAsset)EditorGUILayout.ObjectField("Input => .txt", null, typeof(TextAsset), true);
            if (csv != null)
            {
                LoadCSV(csv.text);
            }
        }

        private void LoadCSV(string tilesStr)
        {
            string[] vert = tilesStr.Split('\n');
            mapWidth = vert[0].Split(',').Length;
            mapHeight = vert.Length;

            settings.mapWidth = mapWidth;
            settings.mapHeight = mapHeight;

            ValueList[] valueList = new ValueList[mapHeight];
            for (int y = 0; y < mapHeight; y++)
            {
                valueList[y] = new ValueList(mapWidth);
                string[] hori = vert[y].Split(',');
                for (int x = 0; x < mapWidth; x++)
                {
                    if (valueList[y].values.Length <= x) break;
                    valueList[y].values[x] = int.Parse(hori[x]);
                }
            }
            settings.mapData = valueList;
        }

        private void ChangeTileTexSize()
        {
            EditorGUILayout.LabelField("描画位置・サイズ調整");

            settings.tileTexWidth = EditorGUILayout.IntField("TileTexWidth", settings.tileTexWidth, GUILayout.MaxWidth(200));
            settings.tileTexHeight = EditorGUILayout.IntField("TileTexHeight", settings.tileTexHeight, GUILayout.MaxWidth(200));
            settings.drawMapOffsetX = EditorGUILayout.IntField("DrawMapOffsetX", settings.drawMapOffsetX, GUILayout.MaxWidth(200));
            settings.drawMapOffsetY = EditorGUILayout.IntField("DrawMapOffsetY", settings.drawMapOffsetY, GUILayout.MaxWidth(200));
            EditorGUILayout.Space(16);
        }

        private void DrawMap()
        {
            Vector2 mousePos = Event.current.mousePosition;

            for (int y = 0; y < settings.mapHeight; y++)
            {
                for (int x = 0; x < settings.mapWidth; x++)
                {
                    int posX = x * settings.tileTexWidth + settings.drawMapOffsetX;
                    int posY = y * settings.tileTexHeight + settings.drawMapOffsetY;
                    bool xIsOK = mousePos.x > posX && mousePos.x < posX + settings.tileTexWidth;
                    bool yIsOK = mousePos.y > posY && mousePos.y < posY + settings.tileTexHeight;
                    if (xIsOK && yIsOK)
                    {
                        switch (settings.brushType)
                        {
                            case BrushType.Brush:
                                SimpleBrush(x, y);
                                break;
                            case BrushType.Rect:
                                RectBrush(x, y, false);
                                break;
                            case BrushType.FillRect:
                                RectBrush(x, y, true);
                                break;
                            case BrushType.Bucket:
                                Bucket(x, y);
                                break;
                        }
                    }
                    Texture tileTex = settings.tileTex[settings.mapData[y].values[x]];
                    EditorGUI.DrawPreviewTexture(new Rect(posX, posY, settings.tileTexWidth, settings.tileTexHeight), tileTex);
                }
            }

            EditorGUILayout.Space(settings.mapHeight * settings.tileTexHeight + settings.drawMapOffsetY + 8);
        }

        private void Paint(int x, int y)
        {
            settings.mapData[y].values[x] = settings.nowTileIdx;
            Repaint();
        }

        private void SimpleBrush(int x, int y)
        {
            bool mousePress = Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown;
            if (mousePress)
            {
                Undo.RecordObject(settings, "DrawTile");
                Paint(x, y);
            }
        }


        int downX, downY = 0;
        private void RectBrush(int x, int y, bool fill)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                downX = x;
                downY = y;
            }
            if (Event.current.type == EventType.MouseUp)
            {
                int startX = Mathf.Min(downX, x);
                int startY = Mathf.Min(downY, y);
                int endX = Mathf.Max(downX, x);
                int endY = Mathf.Max(downY, y);

                Undo.RecordObject(settings, "RectFillTile");
                for (int _y = startY; _y <= endY; _y++)
                {
                    for (int _x = startX; _x <= endX; _x++)
                    {
                        if(fill)
                        {
                            Paint(_x, _y);
                        }
                        else
                        if (_x == endX || _y == endY || _x == startX || _y == startY)
                        {
                            Paint(_x, _y);
                        }
                    }
                }
            }
        }

        int bucketFirstTile = -1;

        private void BucketProcess(int x, int y)
        {
            Paint(x, y);
            if (y - 1 >= 0)
                if (bucketFirstTile == settings.mapData[y - 1].values[x])
                {
                    BucketProcess(x, y - 1);
                }
            if (y + 1 < settings.mapHeight)
                if (bucketFirstTile == settings.mapData[y + 1].values[x])
                {
                    BucketProcess(x, y + 1);
                }
            if (x - 1 >= 0)
                if (bucketFirstTile == settings.mapData[y].values[x - 1])
                {
                    BucketProcess(x - 1, y);
                }
            if (x + 1 < settings.mapWidth)
                if (bucketFirstTile == settings.mapData[y].values[x + 1])
                {
                    BucketProcess(x + 1, y);
                }
        }

        private void Bucket(int x, int y)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                Undo.RecordObject(settings, "Bucket");
                bucketFirstTile = settings.mapData[y].values[x];
                if (bucketFirstTile != settings.nowTileIdx)
                    BucketProcess(x, y);
            }
        }

        private void CreateCSV()
        {
            
        }
    }
    public enum TileType
    {
        Space = 0,
        Wall = 1,
        Platform = 2,
        Ladder = 3,
        Teleport = 4,
        Enemy = 5,
        Door = 6,
        LockedDoor = 7,
        Key = 8,
        Treasure = 9,
        EntranceGate = 10,
        PlatformAndLadder = 11,

        OtherEnemy_1 = 31,
        OtherEnemy_2 = 32,
        OtherEnemy_3 = 33,

        Goal_0 = 50,
        Goal_1 = 51,
        Goal_2 = 52,
        Goal_3 = 53,

        Prefab_0 = 60,
        Prefab_1 = 61,
        Prefab_2 = 62,
        Prefab_3 = 63,
        Prefab_4 = 64,
        Prefab_5 = 65,
        Prefab_6 = 66,
        Prefab_7 = 67,
        Prefab_8 = 68,
        Prefab_9 = 69,
        Prefab_10 = 70,
        Prefab_11 = 71,
        Prefab_12 = 72,
        Prefab_13 = 73,
        Prefab_14 = 74,
        Prefab_15 = 75,
        Prefab_16 = 76,
    }
}