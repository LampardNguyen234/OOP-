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
        List<Texture2D> bulletTextureList = new List<Texture2D>();
        List<Texture2D> towerTextureList = new List<Texture2D>();
        List<Texture2D> enemyTextureList = new List<Texture2D>();
        List<ackackTower> ackackTowerList = new List<ackackTower>();
        List<DualMachineGunTower> dualMachineTowerList = new List<DualMachineGunTower>();
        List<MachineGunTower> machineGunTowerList = new List<MachineGunTower>();
        List<LazerTower> lazeTowerList = new List<LazerTower>();
        List<RocketTower> rocketTowerList = new List<RocketTower>();
        List<EnhancingTower> enhancingTowerList = new List<EnhancingTower>();
        List<Enemy> enemyList = new List<Enemy>();
        private Texture2D explosionTexture;

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
            mouseState = oldState = Mouse.GetState();
        }

        //Hàm LoadContent
        public void LoadContent(ContentManager content)
        {
            mouseTexture = content.Load<Texture2D>("hover");        //Load mouseTexture
            mouseManager = new MouseManager(map, Level);
            mouseManager.LoadContent(content);
            explosionTexture = content.Load<Texture2D>("animation");
            for (int i = 0; i < Container.numberOfTowers;i++ )
            {
                if (i < 5)     
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
        public void Update(GameTime gameTime, List<Enemy> enemyList, int retButton)
        {
            mouseManager.Update(gameTime, retButton);
            #region Xây dựng các tower
            switch (MouseManager.buildTower)
            {
                case 2:
                    {
                        if (CheckBuildingAvailable(Container.Tower0Price))
                        {
                            //Trường hợp súng máy
                            MachineGunTower mGT = new MachineGunTower(towerTextureList[2], 3, MouseManager.position, baseTexture, bulletTextureList[0], explosionTexture);
                            machineGunTowerList.Add(mGT);
                            Game1.goldHave -= Container.Tower0Price;
                        }
                        break;
                    }
                case 1:
                {
                    //Trường hợp súng máy hai nòng
                    if (CheckBuildingAvailable(Container.Tower1Price))
                    {
                        DualMachineGunTower d = new DualMachineGunTower(towerTextureList[1], 2, MouseManager.position, baseTexture, bulletTextureList[0], explosionTexture);
                        dualMachineTowerList.Add(d);
                        Game1.goldHave -= Container.Tower1Price;
                    }
                    break;
                }
                case 0:
                {
                    if (CheckBuildingAvailable(Container.Tower2Price))
                    {
                        ackackTower ackTower = new ackackTower(towerTextureList[0], 1, MouseManager.position, baseTexture, bulletTextureList[2], explosionTexture);
                        ackackTowerList.Add(ackTower);
                        Game1.goldHave -= Container.Tower2Price;
                    }
                    break;
                }
                case 3:
                {
                    //Trường hợp súng laze
                    if (CheckBuildingAvailable(Container.Tower3Price))
                    {
                        LazerTower lt = new LazerTower(towerTextureList[3], 4, MouseManager.position, baseTexture, bulletTextureList[3], explosionTexture);
                        lazeTowerList.Add(lt);
                        Game1.goldHave -= Container.Tower3Price;
                    }
                    break;
                }
                case 4:
                {
                    //Trường hợp bệ phóng tên lửa
                    if (CheckBuildingAvailable(Container.Tower4Price))
                    {
                        RocketTower rT = new RocketTower(towerTextureList[4], 5, MouseManager.position, baseTexture, bulletTextureList[4], explosionTexture);
                        rocketTowerList.Add(rT);
                        Game1.goldHave -= Container.Tower4Price;
                    }
                    break;
                }
                case 5:
                {
                    //Trường hợp Tháp tăng cường năng lượng
                    if (CheckBuildingAvailable(Container.Tower5Price))
                    {
                        EnhancingTower eT = new EnhancingTower(towerTextureList[5], 6, MouseManager.position, baseTexture, null, null);
                        enhancingTowerList.Add(eT);
                        Game1.goldHave -= Container.Tower5Price;
                    }
                    break;
                }
            }
            #endregion
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

            //Update MachineGunTower
            for (int i = 0; i < machineGunTowerList.Count; i++)
            {
                if (machineGunTowerList[i].IsAlive)
                    machineGunTowerList[i].Update(gameTime, enemyList);
                else
                {
                    machineGunTowerList.RemoveAt(i);
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

            foreach(EnhancingTower eT in enhancingTowerList)
            {
                eT.Enhancing(ackackTowerList);
                eT.Enhancing(dualMachineTowerList);
                eT.Enhancing(lazeTowerList);
                eT.Enhancing(rocketTowerList);
                eT.Enhancing(machineGunTowerList);
                eT.Update(gameTime);
            }

        }

        public void towerDraw(SpriteBatch spriteBatch)
        {
            //Draw MachineGun
            foreach (MachineGunTower mcgt in machineGunTowerList)
            {
                if (mcgt.IsAlive)
                {
                    mcgt.Draw(spriteBatch);
                }
            }

            //Draw DualMachineGun
            foreach (DualMachineGunTower dmcgt in dualMachineTowerList)
            {
                if (dmcgt.IsAlive)
                {
                    dmcgt.Draw(spriteBatch);
                }
            }

            //Draw ackackTower
            foreach (ackackTower aat in ackackTowerList)
            {
                if (aat.IsAlive)
                {
                    aat.Draw(spriteBatch);
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

            //Draw EnhancingTower
            foreach (EnhancingTower eht in enhancingTowerList)
            {
                if (eht.IsAlive)
                {
                    eht.Draw(spriteBatch);
                }
            }
        }

        public bool CheckBuildingAvailable(int price)
        {
            if (price > Game1.goldHave)
                return false;
            return true;
        }
    }
}
