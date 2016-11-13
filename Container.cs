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
    class Container
    {
        public static int tileSize = 60;
        public static int roadSize = 60;
        public static int scrWidth = 1100;
        public static int scrHeight = 600;
        public static float PI= 3.14159265358979f;
        public static int animationWidth = 50;
        public static int animationHeight = 50;
        public static int enemyTextureSize = 50;
        public static int healthBarHeight = 3;
        public static int healthBarWidth = 20;
        public static float enemyTextureScale = 1f;
        public static int MapWidth = 15;
        public static int MapHeight = 10;

        public static float timeBetweenEnemy = 250f;

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
            Pausing,
            Exit, 
            GameOver,
            Win
        }
        public static int gameLevel = 4;        //Level của màn chơi

        public static Texture2D radiusTexture;

        #region Thông tin về menu chơi
        public static Rectangle playMenuPosition = new Rectangle(900, 0, 200, 600);     //Vị trí của menu chơi
        public static Rectangle infoBlock = new Rectangle(900, 0, 200, 150);        //Vị trí của để in thông tin của các tower
        public static Rectangle towerBlock = new Rectangle(900, 150, 200, 300);       //Vị trí chứa các 
        public static Rectangle skillBlock = new Rectangle(900, 450, 200, 100);       //Vị trí chứa các skill đặc biệt
        public static Vector2 btnSpecialSkill1Button = new Vector2(912, 500);  //Vị trí chức năng đặc biệt 1
        public static Vector2 btnSpecialSkill2Button = new Vector2(975, 500);  //Vị trí chức năng đặc biệt 2
        public static Vector2 btnSpecialSkill3Button = new Vector2(1037, 500);  //Vị trí chức năng đặc biệt 3
        public static int skill1Price = 1000;
        public static int skill2Price = 2000;
        public static int skill3Price = 3000;
        #endregion

        #region Thanh hiển thị máu, tiền, lượt chơi
        public static Rectangle HUD = new Rectangle(0, 0, 900, 20);
        public static Vector2 HPPos = new Vector2(250, 10);        //Vị trí thanh máu 
        public static Rectangle Wave = new Rectangle(0, 0, 300, 20);        //Vị trí wave
        public static Rectangle Money = new Rectangle(700, 0, 200, 20);

        #endregion
        public static Vector2 towerDetailPosition = new Vector2(900, 0);
       
        public static List<Vector2> btnTowerPosList = new List<Vector2>(6)  //Danh sách vị trí các button của tower
        { new Vector2(950, 200),
          new Vector2(1050, 200), 
          new Vector2(950, 300), 
          new Vector2(1050, 300), 
          new Vector2(950, 400), 
          new Vector2(1050, 400) 
        };

        #region Các thông số về các button
        public static Vector2 btnMenuGameSize = new Vector2(200, 50);
        public static Vector2 btnTowerSize = new Vector2(50, 50);
        public static Vector2 cButtonSize = new Vector2(50, 50);

        public static Vector2 btnPlayPosition = new Vector2(500, 200);
        public static Vector2 btnIntroPosition = new Vector2(500, 300);
        public static Vector2 btnTacGiaPosition = new Vector2(500, 400);
        public static Vector2 btnBackPosition = new Vector2(10, 550);

        public static Vector2 btnPausePosition = new Vector2(1050, 550);         //Vị trí nút pause
        public static Vector2 btnResumePosition = new Vector2(1050, 550);       //Vị trí nút resume
        public static Vector2 btnOffMusicPosition = new Vector2(950, 550);     //Vị trí nút tắt âm thanh
        public static Vector2 btnOnMusicPosition = new Vector2(950, 550);      //Vị trí nút bật âm thanh
        public static Vector2 btnReplayPosition = new Vector2(1000, 550);              //Vị trí button đợt tấn công tiếp theo
        public static Vector2 btnNextWavePosition = new Vector2(900, 550);      //Vị trí nút bật âm thanh
        
        #endregion

        #region Input Enemy
        public static int enemyStartingHP = 10;
        public static int enemyPerWave;
        public static float timeBetweenWave;
        public static int waveQuantity;
        public static List<int[]> enemyEachWave = new List<int[]>();
        #endregion

        #region Input Player
        public static float basicEnemySpeed = 0.5f;
        public static List<int> priceList = new List<int>(6) { 70, 100, 250, 500, 1200, 600 };      //giá lần lượt của các tower
        public static List<int> radiusList = new List<int>(6) { 90,90,120,120,200,100 };            //bán kính tấn công lần lượt của các tower
        public static List<int> attackList = new List<int>(6) { 2,3,15,20,50,0 };                   //attack của các tower
        public static int numberOfTowers = 6;
        public static int HP = 100;
        #endregion
    }
}
