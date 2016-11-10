using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TowerDefenseOOP
{
    class Container
    {
        public static int tileSize = 60;
        public static int roadSize = 60;
        public static int scrWidth = 1200;
        public static int scrHeight = 600;
        public static float PI= 3.14159265358979f;
        public static int animationWidth = 50;
        public static int animationHeight = 50;
        public static int enemyTextureSize = 50;
        public static int healthBarHeight = 3;
        public static int healthBarWidth = 20;
        public static float enemyTextureScale = 0.8f;
        public static int MapWidth = 15;
        public static int MapHeight = 10;

        public static float timeBetweenEnemy = 200f;

        public static int towerSize = 50;

        public static int bulletSize = 8;

        public static int numberOfEnemies = 7;

        // Tu update 10:17 31/10 // Button size
        //button
        //<Start>

        public enum GameState
        {
            MainMenu = 0,
            Intro,
            Playing,
            Tacgia,
            Pausing
        }

        
        public static Vector2 btnMenuGameSize = new Vector2(200, 50);
        public static Vector2 btnTowerSize = new Vector2(50, 50);
        public static Vector2 btnBackSize = new Vector2(80, 80);
        public static Vector2 btnPauseSize = new Vector2(50, 50);

        public static Vector2 btnPlayPosition = new Vector2(500, 300);

        public static Vector2 btnIntroPosition = new Vector2(500, 200);

        public static Vector2 btnTacGiaPosition = new Vector2(500, 400);

        public static Vector2 btnBackPosition = new Vector2(750, 150);

        public static Rectangle playMenuPosition = new Rectangle(900, 0, 300, 600);

        public static Vector2 btnTower1Position = new Vector2(1000, 200);
        public static Vector2 btnTower2Position = new Vector2(1100, 200);
        public static Vector2 btnTower3Position = new Vector2(1000, 300);
        public static Vector2 btnTower4Position = new Vector2(1100, 300);
        public static Vector2 btnTower5Position = new Vector2(1000, 400);
        public static Vector2 btnTower6Position = new Vector2(1100, 400);

        public static Vector2 btnPausePosition = new Vector2(1000, 100);
        public static Vector2 btnResumePosition = new Vector2(1000, 100);
        public static Vector2 btnOffMusicPosition = new Vector2(1100, 100);
        public static Vector2 btnOnMusicPosition = new Vector2(1100, 100);

        public static Vector2 btnSpecialSkill1Button = new Vector2(1000, 500);
        public static Vector2 btnSpecialSkill2Button = new Vector2(1070, 500);
        public static Vector2 btnSpecialSkill3Button = new Vector2(1140, 500);

        public static float radiusMax = 300;
        public static int attackMax = 50;
        public static float intervalmax = 4000f;

        #region Input Enemy
        public static int enemyStartingHP = 10;
        public static int enemyPerWave;
        public static float timeBetweenWave;
        public static int waveQuantity;
        public static List<int[]> enemyEachWave = new List<int[]>();
        #endregion

        #region Input Player
        public static float basicEnemySpeed = 0.5f;
        public static int numberOfTowers = 6;
        public static int Tower5Radius = 150;
        public static int Tower5Attack = 0;
        public static int Tower5Price = 600;

        public static int Tower4Radius = 300;
        public static int Tower4Attack = 70;
        public static int Tower4Price = 1200;

        public static int Tower3Radius = 210;
        public static int Tower3Attack = 15;
        public static int Tower3Price = 500;

        public static int Tower2Radius = 150;
        public static int Tower2Attack = 10;
        public static int Tower2Price = 250;

        public static int Tower1Radius = 120;
        public static int Tower1Attack = 1;
        public static int Tower1Price = 100;

        public static int Tower0Radius = 150;
        public static int Tower0Attack = 1;
        public static int Tower0Price = 70;
        public static int HP = 100;
        #endregion
    }
}
