﻿using System;
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
        List<Texture2D> bulletTextureList = new List<Texture2D>();
        List<Texture2D> towerTextureList = new List<Texture2D>();
        List<Texture2D> enemyTextureList = new List<Texture2D>();
        List<ackackTower> ackackTowerList = new List<ackackTower>();
        List<DualMachineGunTower> dualMachineTowerList = new List<DualMachineGunTower>();
        List<LazerTower> lazeTowerList = new List<LazerTower>();
        List<RocketTower> rocketTowerList = new List<RocketTower>();
        List<Enemy> enemyList = new List<Enemy>();

        private Texture2D mouseTexture;     //Texture dùng để check vị trí xây dựng tower có hợp lệ không
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

        private MouseState mouseState;
        private MouseState oldState;

        MouseManager mouseManager;

        #endregion

        //Constructor
        public Player(Map map, int level)
        {
            this.map = map.MapList[level - 1];              //Tùy vào level sẽ có map khác nhau
            this.level = level;                             //Level của màn chơi
            origin = new Vector2(Container.towerSize / 2, Container.towerSize / 2);
            this.scale = Container.enemyTextureScale;
            mouseState = oldState;
        }

        //Hàm LoadContent
        public void LoadContent(ContentManager content)
        {
            mouseTexture = content.Load<Texture2D>("hover");        //Load mouseTexture
            mouseManager = new MouseManager(map, Level);
            mouseManager.LoadContent(content);
            for (int i = 0; i < Container.numberOfTowers;i++ )
            {
                if (i < 5 &&i!=3)      //Chỉ có 4  tower có đạn
                {
                    Texture2D bullet = content.Load<Texture2D>("bullet_00" + i.ToString());
                    bulletTextureList.Add(bullet);
                }
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
        public void Update(GameTime gameTime, List<Enemy> enemyList)
        {
            mouseManager.Update(gameTime);
            switch (MouseManager.buildTower)
            {
                case 0:
                {
                    ackackTower ackTower = new ackackTower(towerTextureList[0], 0, MouseManager.position, baseTexture, bulletTextureList[0]);
                    ackackTowerList.Add(ackTower);
                    break;
                }
                    break;
                case 1:
                {
                    //Trường hợp súng máy hai nòng
                    DualMachineGunTower dmgt = new DualMachineGunTower(towerTextureList[1], 1, MouseManager.position, baseTexture, bulletTextureList[0]);
                    dualMachineTowerList.Add(dmgt);
                    break;
                }
                    break;
                case 2:
                    break;
                case 3:
                {
                    //Trường hợp súng laze
                    LazerTower lt = new LazerTower(towerTextureList[3], 3, MouseManager.position, baseTexture, bulletTextureList[0]);
                    lazeTowerList.Add(lt);
                    break;
                }
                case 4:
                {
                    //Trường hợp bệ phóng tên lửa
                    RocketTower rT = new RocketTower(towerTextureList[4], 4, MouseManager.position, baseTexture, bulletTextureList[3]);
                    rocketTowerList.Add(rT);
                    break;
                }
                case 5:
                    break;
            
            }

            //Update Towers
            towerUpdate(gameTime, enemyList);

        }

        //Hàm Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            mouseManager.Draw(spriteBatch);

            //Vẽ towers 
            towerDraw(spriteBatch);
            //Vẽ enemy
            foreach (Enemy e in enemyList)
            {
                if (e.IsAlive)
                {
                    e.Draw(spriteBatch);
                }
            }
        }

        public void towerUpdate(GameTime gameTime, List<Enemy> enemyList)
        {
            //Update ackackTower
            for (int i = 0; i < ackackTowerList.Count; i++)
            {
                if (ackackTowerList[i].IsAlive)
                    ackackTowerList[i].Update(gameTime, enemyList);
                else
                {
                    ackackTowerList.RemoveAt(i);
                    i--;
                }
            }

            //Update DualMachineGun
            for (int i = 0; i < dualMachineTowerList.Count; i++)
            {
                if (dualMachineTowerList[i].IsAlive)
                    dualMachineTowerList[i].Update(gameTime, enemyList);
                else
                {
                    dualMachineTowerList.RemoveAt(i);
                    i--;
                }
            }

            //Update LazerTower
            for (int i = 0; i < lazeTowerList.Count; i++)
            {
                if (lazeTowerList[i].IsAlive)
                    lazeTowerList[i].Update(gameTime, enemyList);
                else
                {
                    lazeTowerList.RemoveAt(i);
                    i--;
                }
            }

            //Update RocketTower
            for (int i = 0; i < rocketTowerList.Count; i++)
            {
                if (rocketTowerList[i].IsAlive)
                    rocketTowerList[i].Update(gameTime, enemyList);
                else
                {
                    rocketTowerList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void towerDraw(SpriteBatch spriteBatch)
        {
            //Draw ackackTower
            foreach (ackackTower lt in ackackTowerList)
            {
                if (lt.IsAlive)
                {
                    lt.Draw(spriteBatch);
                }
            }

            //Draw DualMachineGun
            foreach (DualMachineGunTower lt in dualMachineTowerList)
            {
                if (lt.IsAlive)
                {
                    lt.Draw(spriteBatch);
                }
            }

            //Draw LazerTower
            foreach (LazerTower lt in lazeTowerList)
            {
                if (lt.IsAlive)
                {
                    lt.Draw(spriteBatch);
                }
            }

            //Draw RocketTower
            foreach (RocketTower rk in rocketTowerList)
            {
                if (rk.IsAlive)
                {
                    rk.Draw(spriteBatch);
                }
            }

        }
    }
}
