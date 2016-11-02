﻿using System;
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
        public static int animationFrameXMax = 8;
        public static int animationFrameYMax = 8;

        public static int enemyTextureSize = 50;
        public static int healthBarHeight = 3;
        public static int healthBarWidth = 20;
        public static int enemyStartingHP = 100;
        public static float enemyTextureScale = 0.65f;

        public static int MapWidth = 15;
        public static int MapHeight = 10;

        public static float timeBetweenEnemy = 100f;

        public static int towerSize = 50;

        public static int enemyPerWave = 50;

        public static int bulletSize = 8;

        public static int basicEnemySpeed = 1;

        public static int numberOfTowers = 4;

        public static int numberOfEnemies = 4;

        // Tu update 10:17 31/10 // Button size
        //button
        //<Start>
        public static Vector2 btnMenuGameSize = new Vector2(100, 20);
        public static Vector2 btnTowerSize = new Vector2(50, 50);
        public static Vector2 btnBackSize = new Vector2(80, 80);

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

        public static float radiusMax = 300;
        public static int attackMax = 50;
        public static float intervalmax = 4000f;
        public static int tower1Price = 50;
        //<End>
    }
}
