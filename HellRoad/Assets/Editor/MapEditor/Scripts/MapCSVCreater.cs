using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
namespace MapEditor
{
    public class MapCSVCreater
    {
        public static void Create(ValueList[] mapData)
        {
            string savePath = Application.dataPath + "/Editor/MapEditor/";
            string fileName = "mapData.csv";
            
            StringBuilder builder = new StringBuilder();
            for(int y = 0; y < mapData.Length;y++)
            {
                for(int x = 0;x < mapData[y].values.Length;x++)
                {
                    builder.Append(mapData[y].values[x]);
                    if (x < mapData[y].values.Length - 1)
                        builder.Append(",");
                }
                if(y < mapData.Length - 1)
                    builder.Append("\n");
            }

            string path = savePath + fileName;
            if (!File.Exists(path))
            {
                using (FileStream stream = new FileStream(savePath + fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(builder.ToString());
                    }
                }
            }
            else
            {
                File.WriteAllText(path, builder.ToString());
            }
        }
    }
}