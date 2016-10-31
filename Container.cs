using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace TowerDefenseOOP
{
    class Container
    {
       
        public static int tileSize = 60;
        public static int roadSize = 60;
        public static int scrWidth = 1100;
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

        public static int towerSize = 60;

        public static int enemyPerWave = 50;

        public static int bulletSize = 8;

        public static int basicEnemySpeed = 1;

        public static int numberOfTowers = 4;

    }
}
