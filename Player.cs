using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TD
{
    class Player
    {
        #region Khai báo
        int[,] map = new int[Container.MapHeight, Container.MapWidth];    //Bản đồ trò chơi
        List<Vector2> roadCenters = new List<Vector2>();    //Danh sách các vector2 chứa các center của các ô có đường đi
        List<Vector2> rockCenters = new List<Vector2>();//Danh sách các vector2 chứa các center của các ô có đá/rừng
        List<Tower> towerList;
        int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private Vector2 position;
        private Vector2 origin;

        private Texture2D mouseTexture; 
        private Texture2D towerTexture;
        private Texture2D baseTexture;

        private MouseState mouseState;
        private MouseState oldState;

        Tower tower;
        #endregion

        #region Constructor
        public Player(int level,Map map, ref List<Tower> towerList)
        {
            int mapLevel = 1;
            this.level = level;
            this.towerList = towerList;
        }
        #endregion

        public void LoadContent(ContentManager content)
        {
            towerTexture = content.Load<Texture2D>("tower__000");
            baseTexture = content.Load<Texture2D>("base");
        }

        #region Hàm update
        public void Update(GameTime gameTime, List<Enemy> enemyList)
        {
            mouseState = Mouse.GetState();
            if (MouseManager.buildTower)
            {
                tower = new Tower(1, MouseManager.position, baseTexture, towerTexture);
                towerList.Add(tower);
            }
        }
        #endregion

        #region Hàm Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towerList)
            {
                tower.Draw(spriteBatch);
            }
        }
        #endregion

    }
}
