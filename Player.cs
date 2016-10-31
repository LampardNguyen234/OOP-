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
        private int money;

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        private Level level;

        private int Round;

        List<Tower> towerList;

        Tower tower;

        private MouseState mouseState;
        private MouseState oldState;

        private Texture2D towerTexture;
        private Texture2D bulletTexture;
        private Texture2D baseTexture;
        private Texture2D animationTexture;

        private int cellX;

        public int CellX
        {
            get { return cellX; }
            set { cellX = value; }
        }

        private int cellY;

        public int CellY
        {
            get { return cellY; }
            set { cellY = value; }
        }

        private int tileX;
        private int tileY;
        #endregion

        #region Constructor
        public Player(Level level,Map map, ref List<Tower> towerList)
        {
            int mapLevel = 1;
            this.level = level;
            this.towerList = towerList;
        }
        #endregion

        #region Kiểm tra xem vị trí có thể xây Tower được không
        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && cellX < level.Width && cellY < level.Height;

            bool spaceClear = true;

            foreach (Tower tower in towerList)
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                    break;
            }

            bool onPath = (level.GetIndex(cellY, cellX) != 1);
            return inBounds && spaceClear && onPath;
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
