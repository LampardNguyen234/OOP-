using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseOOP
{
    class Player
    {
        #region Khai báo

        int[,] map = new int[Container.MapHeight,Container.MapWidth];    //Bản đồ trò chơi
        List<Vector2> roadCenters = new List<Vector2>();//Danh sách các vector2 chứa các center của các ô có đường đi
        List<Vector2> rockCenters = new List<Vector2>();//Danh sách các vector2 chứa các center của các ô có đá/rừng
        List<Texture2D> bulletTextureList = new List<Texture2D>();
        List<Texture2D> towerTextureList = new List<Texture2D>();
        List<Texture2D> enemyTextureList = new List<Texture2D>();
        List<RocketTower> rocketTowerList = new List<RocketTower>();
        List<Enemy> enemyList = new List<Enemy>();

        private Texture2D baseTexture;
        float scale;
        int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private Vector2 position;
        private Vector2 origin;

        MouseManager mouseManager;

        #endregion

        //Constructor
        public Player(Map map, int level)
        {
            this.map = map.MapList[level - 1];              //Tùy vào level sẽ có map khác nhau
            this.level = level;                             //Level của màn chơi
            origin = new Vector2(Container.towerSize / 2, Container.towerSize / 2);
            this.scale = Container.enemyTextureScale;
        }

        //Hàm LoadContent
        public void LoadContent(ContentManager content)
        {
            mouseManager = new MouseManager(map, Level);
            mouseManager.LoadContent(content);
            for (int i = 0; i < Container.numberOfTowers;i++ )
            {
                Texture2D bullet = content.Load<Texture2D>("bullet_00" + i.ToString());
                bulletTextureList.Add(bullet);
                Texture2D tower = content.Load<Texture2D>("tower_00" + i.ToString());
                towerTextureList.Add(tower);
            }
            baseTexture=content.Load<Texture2D>("base");
            for (int i = 0; i < Container.numberOfEnemies; i++)
            {
                Texture2D enemy = content.Load<Texture2D>("enemy_0" + i.ToString("d2"));
                enemyTextureList.Add(enemy);
            }                                       
        }

        //Hàm update
        public void Update(GameTime gameTime,List<Enemy>enemyList)
        {
            mouseManager.Update(gameTime);
            if (MouseManager.buildTower == 1)
            {
                RocketTower rT = new RocketTower(towerTextureList[3], 4, MouseManager.position, baseTexture, bulletTextureList[3]);
                rocketTowerList.Add(rT);
            }
           

            //Update Towers
            for (int i = 0; i < rocketTowerList.Count;i++ )
            {
                if (rocketTowerList[i].IsAlive)
                    rocketTowerList[i].Update(gameTime,enemyList);
                else
                {
                    rocketTowerList.RemoveAt(i);
                    i--;
                }
            }
        }

        //Hàm Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            mouseManager.Draw(spriteBatch);
            foreach(RocketTower rk in rocketTowerList)
            {
                if(rk.IsAlive)
                {
                    rk.Draw(spriteBatch);
                }
            }
            //Vẽ enemy
            foreach (Enemy e in enemyList)
            {
                if (e.IsAlive)
                {
                    e.Draw(spriteBatch);
                }
            }
        }
    }
}
