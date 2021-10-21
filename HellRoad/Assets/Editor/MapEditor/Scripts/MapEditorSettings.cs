using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace MapEditor
{
    public class MapEditorSettings : ScriptableObject
    {
        public int minTileKind = 0;
        public int maxTileKind = 9;

        public int mapWidth = 8;
        public int mapHeight = 8;

        public int drawMapOffsetX = 32;
        public int drawMapOffsetY = 8;

        public int nowTileIdx = 0;

        public Texture2D[] tileTex = null;
        public int tileTexWidth = 16;
        public int tileTexHeight = 16;

        public BrushType brushType = BrushType.Brush;

        public ValueList[] mapData;


        private static MapEditorWindow editWindow;

        [OnOpenAsset(0)]
        public static bool OnOpen(int instanceID, int line)
        {
            var asset = EditorUtility.InstanceIDToObject(instanceID);
            if (asset is MapEditorSettings)
            {
                if (editWindow == null)
                {
                    editWindow = CreateInstance<MapEditorWindow>();
                    editWindow.titleContent = new GUIContent("MapEditorWindow");
                }
                editWindow.Show();
                return true;
            }
            return false;
        }
    }

    [System.Serializable]
    public class ValueList
    {
        public int[] values;
        public ValueList(int size)
        {
            values = new int[size];
        }
    }

    public enum BrushType
    {
        Brush,
        Rect,
        FillRect,
        Bucket,
    }
}