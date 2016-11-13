using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDefenseOOP
{
    /// <summary>
    /// Class chọn Map tương ứng với level được truyền vào trong Constructor.
    /// Tạo các waypoint mô tả đường di chuyển của enemy
    /// </summary>
    class Level
    {
        #region Khai báo

       public int[,] map;       //ma trận hai chiều thể hiện bản đồ

       private Queue<Vector2> wayPoints = new Queue<Vector2>();     //Đường đi của enemy
       public Queue<Vector2> WayPoints
       {
           get { return wayPoints; }
           set { wayPoints = value; }
       }

       int level;

       private List<Texture2D> tileTextureList = new List<Texture2D>();     
       
       private List<Texture2D> roadTextureList = new List<Texture2D>();
       
       private Texture2D background;

       private Vector2 position;
        public int Width        //Độ rộng của ma trận map
        {
            get { return map.GetLength(1); }
        }
        public int Height       //Chiều cao của ma trận map
        {
            get { return map.GetLength(0); }
        }
        /// <summary>
        /// Contructor nhận vào hai parameter, tùy theo level mà chọn map tương ứng
        /// </summary>
        /// <param name="map"></param>
        /// <param name="mapLevel"></param>
        public Level(Map map, int mapLevel)
        {
            level = mapLevel;
            this.map = map.MapList[level-1];
        }

        #endregion

        #region     Tạo waypoints
        public void LoadWayPoint(int k, bool isNavy)
        {
            int index = 1;
            if (isNavy)     //Nếu là đường đi dưới nước
                index = 3;
            while (wayPoints.Count > 0)
                wayPoints.Dequeue();
            Random rand = new Random();                     // tạo số ngẫu nhiên
            Stack<Vector2> way = new Stack<Vector2>();
            List<Vector2> beginningList = new List<Vector2>();
            #region Danh sách các điểm bắt đầu
            for (int x = 0; x < Width; x++)
            {
                if (map[0, x] == index)
                {
                    Vector2 beginningPoint = new Vector2(x, 0) * Container.roadSize;
                    beginningList.Add(beginningPoint);
                }
            }
            for (int y = 0; y < Height; y++)
            {
                if (map[y, 0] == index)
                    beginningList.Add(new Vector2(0, y) * Container.roadSize);
            }
            int m = rand.Next(0, beginningList.Count);
            way.Push(beginningList[m]);
            #endregion

            #region Tạo waypoint
            for (int y = 1; y < Height; y++)
            {
                for (int x = 1; x < Width; x++)
                {
                    if (map[y, x] == index && !isContained(way, y, x))
                    {
                        Vector2 tempPos = way.Peek() / Container.roadSize;
                        if (y == Height - 1 || x == Width - 1)       //Trường hợp các điểm nằm ở biên phải và dưới
                        {
                            if ((int)tempPos.Y == y - 1 && (int)tempPos.X == x)
                            {
                                way.Push(new Vector2(x, y) * Container.roadSize);
                                x = Width - 1;
                                continue;
                            }
                            if ((int)tempPos.Y == y && (int)tempPos.X == x - 1)
                            {
                                way.Push(new Vector2(x, y) * Container.roadSize);
                                y = Height - 1;
                                continue;
                            }
                        }
                        else
                        {
                            #region Đi từ trên xuống
                            if ((int)tempPos.Y == y - 1 && (int)tempPos.X == x)
                            {
                                //Đi thẳng xuống hoặc rẽ phải
                                if (map[y + 1, x] == index && map[y, x + 1] == index)
                                {
                                    int i = rand.Next(0, 20);
                                    if (i + k < 20)     //Chọn rẽ phải
                                    {
                                        way.Push(new Vector2(x, y) * Container.roadSize);
                                        way.Push(new Vector2(x + 1, y) * Container.roadSize);
                                        continue;
                                    }
                                    else                //Chọn đi thẳng xuống
                                    {
                                        way.Push(new Vector2(x, y) * Container.roadSize);
                                        way.Push(new Vector2(x, y + 1) * Container.roadSize);
                                        y++; x = 0;
                                        continue;
                                    }
                                }
                                //Chỉ có thể đi rẽ trái
                                if (map[y + 1, x] != index && map[y, x + 1] != index && map[y, x - 1] == index)
                                {
                                    way.Push(new Vector2(x, y) * Container.roadSize);
                                    way.Push(new Vector2(x - 1, y) * Container.roadSize);
                                    x = 0;
                                    continue;
                                }
                                way.Push(new Vector2(x, y) * Container.roadSize);
                                continue;
                            }
                            #endregion

                            #region Đi từ trái sang phải
                            if ((int)tempPos.Y == y && (int)tempPos.X == x - 1)
                            {
                                //Có thể đi thẳng hoặc đi xuống
                                if (map[y + 1, x] == index && map[y, x + 1] == index)
                                {
                                    int i = rand.Next(0, 20);
                                    if (k + i < 20)         //Chọn đi thẳng sang phải
                                    {
                                        way.Push(new Vector2(x, y) * Container.roadSize);
                                        way.Push(new Vector2(x + 1, y) * Container.roadSize);
                                        continue;
                                    }
                                    else                    //Chọn rẽ xuống
                                    {
                                        way.Push(new Vector2(x, y) * Container.roadSize);
                                        way.Push(new Vector2(x, y + 1) * Container.roadSize);
                                        y = y + 1; x = 0;
                                        continue;
                                    }
                                }
                                //Chỉ có thể rẽ lên
                                if (map[y + 1, x] != index && map[y, x + 1] != index && map[y - 1, x] == index)
                                {
                                    way.Push(new Vector2(x, y) * Container.roadSize);
                                    way.Push(new Vector2(x, y - 1) * Container.roadSize);
                                    y = 0; x = 0;
                                    continue;
                                }
                                way.Push(new Vector2(x, y) * Container.roadSize);
                                continue;
                            }
                            #endregion

                            #region Đi từ phải sang trái
                            if ((int)tempPos.Y == y && (int)tempPos.X == x + 1)
                            {
                                way.Push(new Vector2(x, y) * Container.roadSize);
                                x = 0;
                                continue;
                            }
                            #endregion

                            #region Đi từ dưới lên trên
                            if ((int)tempPos.Y == y + 1 && (int)tempPos.X == x)
                            {
                                way.Push(new Vector2(x, y) * Container.roadSize);
                                y = 1; x = 0;
                                continue;
                            }

                            #endregion
                        }
                    }
                }
            }
            #endregion

            Stack<Vector2> temp = new Stack<Vector2>();
            while (way.Count > 0)
            {
                temp.Push(way.Pop());
            }
            while (temp.Count > 0)
                WayPoints.Enqueue(temp.Pop());
        }
        
        #endregion


        // Kiểm tra xem vị trí map[x,y] có phải là đường đi không
        bool isRoad(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return false;
            if (map[x, y] == 0)
                return false;
            else
                return true;
        }
        
        //Lấy chỉ số tương ứng của ô vuông 
        public int GetIndex(int cellY,int cellX)
        {
            if (cellX < 0 || cellX >= Width || cellY < 0 || cellY >= Height)
                return -1;
            else
                return map[cellY, cellX];
        }

        //Kiểm tra xem map[Y,X] đã có trong waypoints chưa
        bool isContained(Stack<Vector2> waypoints, int Y, int X)
        {
            foreach (Vector2 waypoint in waypoints)
            {
                if ((int)(waypoint.X / Container.tileSize) == X && (int)(waypoint.Y / Container.tileSize) == Y)
                    return true;
            }
            return false;
        }

        //LoadContent
        public void LoadContent(ContentManager Content)
        {
            for (int k = 0; k < 11; k++)
            {
                if (k < 10)
                {
                    string sTile = "tile_" + (level - 2).ToString("d2") + "_" + k.ToString("d2");   //Cần sửa ở đây
                    Texture2D tile = Content.Load<Texture2D>(sTile);
                    tileTextureList.Add(tile);
                }
                string sRoad = "road_" + (level - 2).ToString("d2") + "_" + k.ToString("d2");       //Đây cũng vậy
                Texture2D road = Content.Load<Texture2D>(sRoad);
                roadTextureList.Add(road);

            }
            string s = "map" + level.ToString();
            background = Content.Load<Texture2D>(s);
                
        }

        //Kiểm tra kiểu đường, mục đích để vẽ đường cho phù hợp
        int CheckRoad(int Y, int X)
        {
            if (map[Y, X] == 0)
            {
                if (Y < Height - 1 && X < Width - 1 && Y - 1 >= 0 && X - 1 >= 0)
                {
                    if (map[Y - 1, X] == 1 && map[Y, X - 1] == 1 && map[Y, X + 1] == 1 && map[Y + 1, X] == 1)
                        return -6;
                    if (map[Y - 1, X] == 1 && map[Y, X - 1] == 1 && map[Y, X + 1] == 1)
                        return -7;
                    if (map[Y - 1, X] == 1 && map[Y, X + 1] == 1 && map[Y + 1, X] == 1)
                        return -8;
                    if (map[Y, X - 1] == 1 && map[Y, X + 1] == 1 && map[Y + 1, X] == 1)
                        return -9;
                    if (map[Y - 1, X] == 1 && map[Y, X - 1] == 1 && map[Y + 1, X] == 1)
                        return -10;
                    if (map[Y - 1, X] == 1 && map[Y - 1, X + 1] == 1 && map[Y, X + 1] == 1)
                        return -2;
                    if (map[Y + 1, X + 1] == 1 && map[Y + 1, X] == 1 && map[Y, X + 1] == 1)
                        return -3;
                    if (map[Y + 1, X] == 1 && map[Y + 1, X - 1] == 1 && map[Y, X - 1] == 1)
                        return -4;
                    if (map[Y - 1, X] == 1 && map[Y - 1, X - 1] == 1 && map[Y, X - 1] == 1)
                        return -5;
                }
                else
                    if (Y == Height - 1)
                    {
                        if (map[Y - 1, X] == 1 && map[Y - 1, X - 1] == 1 && map[Y, X - 1] == 1)
                            return -5;
                        if (!(X == Width - 1))
                            if (map[Y - 1, X] == 1 && map[Y - 1, X + 1] == 1 && map[Y, X + 1] == 1)
                                return -2;
                    }
                    else
                        if (Y == 0 && X < Width - 1 && X - 1 >= 0)
                        {
                            if (map[Y, X - 1] == 1 && map[Y + 1, X - 1] == 1 && map[Y + 1, X] == 1)
                                return -4;
                            if (map[Y, X + 1] == 1 && map[Y + 1, X + 1] == 1 && map[Y + 1, X] == 1)
                                return -3;
                        }
                        else
                            if (X == 0 && Y > 0 && Y + 1 < Height)
                            {
                                if (map[Y + 1, X] == 1 && map[Y, X + 1] == 1)
                                    return -3;
                                if (map[Y - 1, X] == 1 && map[Y, X + 1] == 1)
                                    return -2;
                            }
                            else
                                if (X == Width - 1 && Y > 0 && Y + 1 < Height)
                                {
                                    if (map[Y + 1, X] == 1 && map[Y, X - 1] == 1)
                                        return -4;
                                    if (map[Y - 1, X] == 1 && map[Y, X - 1] == 1)
                                        return -5;
                                }

                return -1; //Grass
            }
            else
                if (map[Y, X] == 1)
                {
                    if (Y < Height - 1 && X < Width - 1 && Y - 1 >= 0 && X - 1 >= 0)
                    {
                        if (map[Y + 1, X] == 1 && map[Y, X - 1] == 1 && map[Y, X + 1] == 1 && map[Y - 1, X] == 1)
                            return 10;
                        if (map[Y + 1, X] == 1 && map[Y - 1, X] == 1 && map[Y, X + 1] == 1)
                            return 6;
                        if (map[Y + 1, X] == 1 && map[Y - 1, X] == 1 && map[Y, X - 1] == 1)
                            return 8;
                        if (map[Y - 1, X] == 1 && map[Y, X + 1] == 1 && map[Y, X - 1] == 1)
                            return 9;
                        if (map[Y + 1, X] == 1 && map[Y, X - 1] == 1 && map[Y, X + 1] == 1)
                            return 7;
                        if (map[Y, X - 1] == 1 && map[Y + 1, X] == 1)
                            return 2;
                        if (map[Y, X - 1] == 1 && map[Y - 1, X] == 1)
                            return 3;
                        if (map[Y, X + 1] == 1 && map[Y - 1, X] == 1)
                            return 4;
                        if (map[Y, X + 1] == 1 && map[Y + 1, X] == 1)
                            return 5;
                        if (map[Y + 1, X] == 1 && map[Y - 1, X] == 1)
                            return 1;
                        if (map[Y, X + 1] == 1 && map[Y, X - 1] == 1)
                            return 0;
                    }
                    if (Y == Height - 1 || Y == 0)
                        return 1;
                    if (X == Height - 1 || X == 0)
                        return 0;
                }
           return 0;
        }


        //Hàm vẽ map
        public void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            Color color = Color.Tan;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Vector2 origin = new Vector2(0, 0);
                    Texture2D texture = null;
                    position.X = x * Container.tileSize;
                    position.Y = y * Container.tileSize;
                    texture = roadTextureList[0];
                    spriteBatch.Draw(texture, position, null, color, 0f, origin, (float)Container.tileSize / (float)texture.Width, SpriteEffects.None, 0f);
                    int k = CheckRoad(y, x);
                    if (k < 0)
                    {
                        texture = roadTextureList[0];
                        spriteBatch.Draw(texture, position, null, color, 0f, origin, (float)Container.tileSize / (float)texture.Width, SpriteEffects.None, 0f);
                        texture = tileTextureList[-1 - k];
                        spriteBatch.Draw(texture, position, null, color, 0f, origin, (float)Container.tileSize / (float)texture.Width, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        texture = tileTextureList[0];
                        spriteBatch.Draw(texture, position, null, color, 0f, origin, (float)Container.tileSize / (float)texture.Width, SpriteEffects.None, 0f);
                        texture = roadTextureList[k];
                        spriteBatch.Draw(texture, position, null, color, 0f, origin, (float)Container.tileSize / (float)texture.Width, SpriteEffects.None, 0f);
                    }

                }
            }
            // spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
        }
    }
}
