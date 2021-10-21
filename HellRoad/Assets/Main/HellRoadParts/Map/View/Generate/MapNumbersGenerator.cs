using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HellRoad.External
{
    public class MapNumbersGenerator
    {
        private int[,] tiles;

        public int[,] Generate(MapAsset asset, int maxMapSizeX = 500, int maxMapSizeY = 500, int generateBackCount = 3)
        {
            tiles = new int[maxMapSizeX, maxMapSizeY];
            if (asset.Status.FilledAll)
                for (int y = 0; y < maxMapSizeY; y++)
                    for (int x = 0; x < maxMapSizeX; x++)
                        tiles[x, y] = 1;

            List<MapRoomAsset> rooms = asset.GetAllClampedRooms().OrderBy(g => Guid.NewGuid()).ToList();

            //リスト初期化
            List<Vector2Int> generatePoses = new List<Vector2Int>();
            List<Direction>[] outputExclusions = new List<Direction>[rooms.Count];
            for (int i = 0; i < rooms.Count; i++) outputExclusions[i] = new List<Direction>();

            //一番最初のルームを設置
            MapRoomAsset nowRoom = rooms[0];
            generatePoses.Add(new Vector2Int((maxMapSizeX - nowRoom.Width) / 2, (maxMapSizeY - nowRoom.Height) / 2));

            WriteRoomToTiles(generatePoses[0], nowRoom);

            int generationRange = generateBackCount;
            for (int i = 1; i < rooms.Count; i++)
            {
                int beforeIdx = 0;
                do
                {
                    beforeIdx = Mathf.Max(Random.Range(i - generationRange, i), 0);
                }
                while (outputExclusions[beforeIdx].Count == 4);

                MapRoomAsset beforeRoom = rooms[beforeIdx];
                RoomOutputs beforeOutput = beforeRoom.DirectionToOutput(GetRandomDir(outputExclusions[beforeIdx]));
                Vector2Int beforeGeneratePos = generatePoses[beforeIdx];
                Vector2Int beforeOutputPos = beforeRoom.OutputToPos(beforeOutput);

                Direction inputDir = beforeOutput.Direction.Invert();

                nowRoom = rooms[i];
                RoomOutputs nowInput = nowRoom.DirectionToOutput(inputDir);
                Vector2Int nowInputPos = nowRoom.OutputToPos(nowInput);
                Vector2Int nowGeneratePos = beforeGeneratePos + beforeOutputPos - nowInputPos;

                if (!CanPutRoom(asset.Status.FilledAll, tiles, nowGeneratePos, nowRoom))
                {
                    i--;
                    generationRange++;
                    continue;
                }
                WriteOutputToTiles(beforeGeneratePos, beforeRoom, beforeOutput);
                WriteRoomToTiles(nowGeneratePos, nowRoom);
                WriteOutputToTiles(nowGeneratePos, nowRoom, nowInput);
                generationRange = generateBackCount;
                generatePoses.Add(nowGeneratePos);
                outputExclusions[beforeIdx].Add(beforeOutput.Direction);
                outputExclusions[i].Add(inputDir);

            }
            TrimSpace(asset.Status.FilledAll, maxMapSizeX, maxMapSizeY);
            return tiles;
        }

        private bool CanPutRoom(bool filledAll, int[,] tiles, Vector2Int startPos, MapRoomAsset room)
        {
            int checkTile = NormalTile(filledAll);

            for (int y = 0; y < room.Height; y++)
                for (int x = 0; x < room.Width; x++)
                    if (tiles[startPos.x + x, startPos.y + y] != checkTile)
                        return false;
            return true;
        }

        private void WriteOutputToTiles(Vector2Int startPos, MapRoomAsset room, RoomOutputs output)
        {
            List<Vector2Int> poses = room.OutputToPoses(output);
            bool isVertical = output.Direction == Direction.Bottom || output.Direction == Direction.Top;
            for(int i = 0;i < poses.Count; i++)
            {
                int x = startPos.x + poses[i].x;
                int y = startPos.y + poses[i].y;
                if (isVertical)
                {
                    tiles[x, y] = (int)TileType.Platform;
                }
                else
                {
                    tiles[x, y] = 0;
                }
            }
        }

        private void WriteRoomToTiles(Vector2Int startPos, MapRoomAsset room)
        {
            for (int y = 0; y < room.Height; y++)
                for (int x = 0; x < room.Width; x++)
                    tiles[startPos.x + x, startPos.y + y] = room.GetTile(x, y);
        }

        private Direction GetRandomDir(List<Direction> exclusions)
        {
            List<Direction> dirs = new List<Direction>() { Direction.Top, Direction.Left, Direction.Bottom, Direction.Right };
            foreach(Direction exclusion in exclusions) dirs.Remove(exclusion);
            return dirs[Random.Range(0, dirs.Count)];
        }

        private Vector2Int LeftAndRight(bool filledAll, int maxMapSizeX)
        {
            int checkTile = NormalTile(filledAll);
            int filledTileLength = 15;

            int left = maxMapSizeX;
            int right = 0;
            for (int y = 0; y < tiles.GetLength(1);y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    if (tiles[x, y] != checkTile)
                    {
                        if (left > x)
                            left = x;
                            else
                        if (right < x)
                            right = x;
                    }

                }
            }
            if(filledAll)
            {
                left = Mathf.Max(0, left - filledTileLength);
                right = Mathf.Min(maxMapSizeX - 1, right + filledTileLength);
            }
            return new Vector2Int(left, right + 1);
        }

        private Vector2Int BottomAndTop(bool filledAll, int maxMapSizeY)
        {
            int checkTile = NormalTile(filledAll);
            int filledTileLength = 10;

            int bottom = maxMapSizeY;
            int top = 0;
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y] != checkTile)
                    {
                        if (bottom > y)
                            bottom = y;
                        else
                        if (top < y)
                            top = y;
                    }

                }
            }
            if (filledAll)
            {
                bottom = Mathf.Max(0, bottom - filledTileLength);
                top = Mathf.Min(maxMapSizeY - 1, top + filledTileLength);
            }
            return new Vector2Int(bottom, top + 1);
        }

        private void TrimSpace(bool filledAll, int maxMapSizeX, int maxMapSizeY)
        {
            Vector2Int leftRight = LeftAndRight(filledAll, maxMapSizeX);
            Vector2Int bottomTop = BottomAndTop(filledAll, maxMapSizeY);
            int[,] tmpTiles = new int[leftRight.y - leftRight.x, bottomTop.y - bottomTop.x];

            for (int y = bottomTop.x; y < bottomTop.y; y++)
            {
                for (int x = leftRight.x; x < leftRight.y; x++)
                {
                    tmpTiles[x - leftRight.x, y - bottomTop.x] = tiles[x, y];
                }
            }
            tiles = tmpTiles;
        }

        private int NormalTile(bool filledAll)
        {
            int checkTile = 0;
            if (filledAll) checkTile = 1;
            return checkTile;
        }

        //public void ShowResult()
        //{
        //    System.Text.StringBuilder builder = new System.Text.StringBuilder();
        //    char[] words = new char[] { '□', '■', '―' };

        //    for (int y = 0; y < tiles.GetLength(1); y++)
        //    {
        //        for (int x = 0; x < tiles.GetLength(0); x++)
        //        {
        //            int id = tiles[x, y];
        //            if (words.Length > id)
        //                builder.Append(words[id]);
        //        }
        //        builder.Append("\n");
        //    }
        //    GUIUtility.systemCopyBuffer = builder.ToString();
        //}
    }
}