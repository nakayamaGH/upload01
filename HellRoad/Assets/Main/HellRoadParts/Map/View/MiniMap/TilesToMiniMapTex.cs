using System.Collections.Generic;
using UnityEngine;

namespace HellRoad.External
{
    public class TilesToMiniMapTex
    {
        private Color otherColor = "#6f9fde".ParseHexColorCode();

        private Dictionary<TileType, Color> tileColors = new Dictionary<TileType, Color>() 
        {
            {TileType.Wall,     "#263b57".ParseHexColorCode() },
            {TileType.Platform, "#51719c".ParseHexColorCode() },
            {TileType.Ladder,   "#51719c".ParseHexColorCode() },
            {TileType.PlatformAndLadder, "#51719c".ParseHexColorCode() },
            {TileType.Door,     "#964d69".ParseHexColorCode() },
        };

        public Texture2D CreateTexture(int[,] tiles)
        {
            int width = tiles.GetLength(0);
            int height = tiles.GetLength(1);
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.filterMode = FilterMode.Point;

            for(int y = 0; y < height;y++)
            {
                for (int x = 0; x < width; x++)
                {
                    texture.SetPixel(x, y, GetColor((TileType)tiles[x, y]));
                }
            }
            texture.Apply();
            return texture;
		}

        private Color GetColor(TileType tileType)
        {
            bool containColor = tileColors.ContainsKey(tileType);
            if(containColor)
            {
                return tileColors[tileType];
            }
            return otherColor;
        }
    }
}