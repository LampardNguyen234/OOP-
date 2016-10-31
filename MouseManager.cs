using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TD
{
    class MouseManager
    {
        #region Khai báo
        public static Vector2 position;
        MouseState mouseState;
        Texture2D mouseTexture;
        MouseState oldState;
        List<Tower> towerList;
        List<Enemy> enemyList;
        private Vector2 origin;
        int level;
        public static bool buildTower;

        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        #endregion

        List<Vector2> roadCenters = new List<Vector2>();
        List<Vector2> rockCenters = new List<Vector2>();
        int[,] map = new int[Container.MapHeight, Container.MapWidth];


        //Constructor
        public MouseManager(Map map, int level, ref List<Tower> towerList)
        {
            this.map = map.MapList[level - 1];
            this.level = level;
            this.towerList = towerList;
            origin = new Vector2(Container.towerSize / 2, Container.towerSize / 2);
            buildTower = false;
        }


        //Đưa vị trí cái waypoint và rock vào list
        public void addToMap()
        {
            Vector2 temp = new Vector2(Container.towerSize / 2, Container.towerSize / 2);
            for (int y = 0; y < Container.MapHeight; y++)
            {
                for (int x = 0; x < Container.MapWidth; x++)
                {
                    if (map[y, x] == 1)//Chỉ số các ô nằm trên đường đi bằng 1
                    {
                        Vector2 temp2 = new Vector2(x, y) * Container.towerSize + temp;//Lấy vị trí tâm của ô 
                        roadCenters.Add(temp2);  //Thêm vào list
                    }
                    if (map[y, x] == 2)//Chỉ số các ô chứa đá bằng 2
                    {
                        Vector2 temp2 = new Vector2(x, y) * Container.towerSize + temp;//Lấy vị trí tâm của ô 
                        rockCenters.Add(temp2);  //Thêm vào list
                    }
                }
            }
        }


        //Kiểm tra vị trí trỏ chuột có thể xây tower không
        public bool checkTowerAvailable(Vector2 position)
        {
            int width = Container.MapWidth;
            int height = Container.MapHeight;
            foreach (Vector2 roadCenter in roadCenters)
            {
                if (Vector2.Distance(position, roadCenter) < (float)Container.tileSize)
                    return false;
            }
            foreach (Vector2 rockCenter in rockCenters)
            {
                if (Vector2.Distance(position, rockCenter) < (float)Container.tileSize * 2)
                    return false;
            }
            foreach (Tower tower in towerList)
            {
                if (Vector2.Distance(position, tower.Center) < (float)Container.towerSize)
                    return false;
            }
            return true;
        }

        //Load content
        public void LoadContent(ContentManager content)
        {
            mouseTexture = content.Load<Texture2D>("hover");
            addToMap();
        }


        //Update
        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            position = new Vector2(mouseState.X, mouseState.Y);
            buildTower = false;
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
            {
                if (checkTowerAvailable(position))
                {
                    buildTower = true;
                }
            }
            oldState=mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            if (checkTowerAvailable(position))
                color = Color.Blue;
            else
                color = Color.Red;
            spriteBatch.Draw(mouseTexture, position, null, color * 0.5f, 0f, origin, (float)Container.tileSize / (float)mouseTexture.Width, SpriteEffects.None, 0f);
        }
    }
}
