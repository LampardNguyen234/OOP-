using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDefenseOOP
{
    class MouseManager
    {
        #region Khai báo
        public static Vector2 position;
        MouseState mouseState;
        Texture2D mouseTexture;
        List<Texture2D> towerTextureList = new List<Texture2D>();
        Texture2D radiusTexture;
        MouseState oldState;
        List<Tower> towerList;
        List<Enemy> enemyList;
        private Vector2 origin;
        private bool isBuildingTower;
        int level;
        float scale;
        public static int buildTower;
        private int tempBuildTower;

        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        #endregion

        List<Vector2> roadCenters = new List<Vector2>();
        List<Vector2> rockCenters = new List<Vector2>();
        int[,] map = new int[Container.MapHeight, Container.MapWidth];
        private int Level_2;


        //Constructor
        public MouseManager(int[,] map, int level)
        {
            this.map = map;
            this.level = level;
            this.scale = Container.enemyTextureScale;
            origin = new Vector2(Container.towerSize / 2, Container.towerSize / 2);
            buildTower = -1;
            tempBuildTower = -1;
            isBuildingTower = false;
        }


        //Đưa vị trí cái waypoint và rock vào list
        public void addToMap()
        {
            Vector2 temp = new Vector2(Container.roadSize / 2, Container.roadSize / 2);
            for (int y = 0; y < Container.MapHeight; y++)
            {
                for (int x = 0; x < Container.MapWidth; x++)
                {
                    if (map[y, x] == 1)//Chỉ số các ô nằm trên đường đi bằng 1
                    {
                        Vector2 temp2 = new Vector2(x, y) * Container.roadSize + temp;//Lấy vị trí tâm của ô 
                        roadCenters.Add(temp2);  //Thêm vào list
                    }
                    if (map[y, x] == 2)//Chỉ số các ô chứa đá bằng 2
                    {
                        Vector2 temp2 = new Vector2(x, y) * Container.roadSize + temp;//Lấy vị trí tâm của ô 
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
            if (position.X > 870) return false;
            foreach (Vector2 roadCenter in roadCenters)
            {
                if (Vector2.Distance(position, roadCenter) < (float)Container.tileSize * (scale + 0.05f))
                    return false;
            }
            foreach (Vector2 rockCenter in rockCenters)
            {
                if (Vector2.Distance(position, rockCenter) < (float)Container.towerSize * 2)
                    return false;
            }
            return true;
        }

        //Load content
        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < 6; i++)
            {
                string s = "tower_00" + i.ToString();
                Texture2D sTemp = content.Load<Texture2D>(s);
                towerTextureList.Add(sTemp);
            }
            mouseTexture = content.Load<Texture2D>("base");
            radiusTexture = content.Load<Texture2D>("radius");
            addToMap();
        }


        //Update
        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            position = new Vector2(mouseState.X, mouseState.Y);
            buildTower = -1;

            //Hàm lấy giá trị và trạng thái chọn tháp để mang vào map xây
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed && position.X > 900 && position.X<1000)
            {
                tempBuildTower = 0;//Giá trị trả về từ hàm isClick() trong Menu
                isBuildingTower = true;
            }
            else if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed && position.X > 1000)
            {
                tempBuildTower = 1;
                isBuildingTower = true;
            }

            //RightClick để hủy việc đang chọn Tower
            if (mouseState.RightButton == ButtonState.Released && oldState.RightButton == ButtonState.Pressed && isBuildingTower)
            {
                isBuildingTower = false;
                tempBuildTower = -1;
            }


            //Check vị trí của Tower
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed && isBuildingTower)
            {
                if (checkTowerAvailable(position))
                {
                    buildTower = tempBuildTower;
                    tempBuildTower = -1;
                    isBuildingTower = false;
                }
            }
            oldState = mouseState;
        }


        //Hàm vẽ
        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            if (checkTowerAvailable(position))
                color = Color.White;
            else
                color = Color.Red;
            if (isBuildingTower)
            {
                spriteBatch.Draw(mouseTexture, position, null, color, 0f, origin, (float)Container.towerSize / (float)mouseTexture.Width, SpriteEffects.None, 0f);
                if(tempBuildTower!=-1)
                    spriteBatch.Draw(towerTextureList[tempBuildTower], position, null, color, 0f, origin, (float)Container.towerSize / (float)towerTextureList[tempBuildTower].Width, SpriteEffects.None, 0f);
                spriteBatch.Draw(radiusTexture, position, null, Color.Green * 0.2f, 0f, new Vector2(224 / 2, 224 / 2), (float)Container.towerSize / (float)mouseTexture.Width, SpriteEffects.None, 0f);
            }

        }
    }
}