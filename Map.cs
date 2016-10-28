using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TowerDefenseOOP
{
    /// <summary>
    /// Class đọc ma trận thể hiện bản đồ sẽ sử dụng trong Game từ file Map.txt dưới dạng mảng hai chiều
    /// </summary>
    class Map
    {
        List<int[,]> mapList = new List<int[,]>();      //Mảng hai chiều chứa ma trận thể hiện map

        public List<int[,]> MapList
        {
            get { return mapList; }
        }

        //Constructor, đọc dữ liệu map vào mảng hai chiều từ file
        public Map()
        {
            int[,] map = new int[Container.MapHeight, Container.MapWidth];
            FileStream stream = new FileStream("Map.txt", FileMode.Open);   //Mở file Map.txt để đọc
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);
            int y = 0;
            int x = 0;
            while (reader.EndOfStream != true)
            {
                char ch = (char)reader.Read();
                if ((int)ch >= 48 && (int)ch <= 57)
                {
                    map[y, x] = int.Parse(ch.ToString());
                    if (x == 14)
                    {
                        x = 0;
                        y++;
                    }
                    else
                        x++;
                    if (y == Container.MapHeight)
                    {
                        mapList.Add(map);
                        map = new int[Container.MapHeight, Container.MapWidth];
                        x = 0;
                        y = 0;
                    }
                }
                else
                    continue;
            }
            reader.Close();
            stream.Close();
        }
    }
}
