using System;
using System.Collections.Generic;
using System.Linq;
using LineBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TowerDefenseOOP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Animation> explosionAnimation;
        Map map = new Map();
        Texture2D healthbar;
        Player player;
        MouseState mouse;
        buttonManager btM;          //quản lý các button
        HUD hud;            //In điểm
        Input input = new Input();      //Nhập input từ file text vào

        List<Enemy> enemyList;
        List<Texture2D> enemyTextureList = new List<Texture2D>();
        Level level;
        public static SpriteFont playerScoreFont;
        public static float nextWaveTimer;
        public float timer;
        public static int enemyPerWave;
        public static int goldHave;
        public static int wave = 0;       //Thể hiện lượt tấn công
        public static SoundManager sm = new SoundManager();
        Container.GameState CurrentState = Container.GameState.MainMenu;
        //button o menu game

        public static Texture2D baseButton;
        int temp = 1;       
        //
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Container.scrWidth;
            graphics.PreferredBackBufferHeight = Container.scrHeight;
            graphics.IsFullScreen = false;
            this.IsMouseVisible = true;
            this.Window.Title = "Tower Defense";
            Content.RootDirectory = "Content";
            btM = new buttonManager(graphics);
        }

        protected override void Initialize()
        {
            Mouse.WindowHandle = Window.Handle;
            SpriteBatchEx.GraphicsDevice = GraphicsDevice;  //Khởi tạo công cụ vẽ
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LoadGame(Content, Container.gameLevel);
            Container.radiusTexture = Content.Load<Texture2D>("radius");
            baseButton = Content.Load<Texture2D>("base_build");
            healthbar = Content.Load<Texture2D>("healthbar");
            for (int i = 0; i < Container.numberOfEnemies; i++)
            {
                Texture2D enemy = Content.Load<Texture2D>("enemy_0" + i.ToString("d2"));
                enemyTextureList.Add(enemy);
            }
            timer = 0;
            enemyPerWave = Container.enemyPerWave;
            btM.LoadContent(Content);
            hud = new HUD(new Vector2(mouse.X, mouse.Y), playerScoreFont, new Vector2(0, 50),healthbar);   //Xóa;
            sm.LoadContent(Content);    //Load âm thanh
            SoundEffect.MasterVolume = 0.1f;
            enemyPerWave = Container.enemyPerWave;
            playerScoreFont = Content.Load<SpriteFont>("algerianRegular");      //MỚi thêm
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Mouse.SetPosition(Container.scrWidth/2, Container.scrHeight/2);
        }

        protected override void Update(GameTime gameTime)
        {
            //Tu update MainMenu
            //<Start>
            mouse = Mouse.GetState();
            btM.Update(CurrentState, mouse, goldHave, gameTime);
            CurrentState = btM.GameState;
            // check Exit
            if (CurrentState == Container.GameState.Exit)
                Exit();

            if(CurrentState==Container.GameState.MainMenu)
            {
                //Load nhạc nền menu
                if (temp > 0)
                {
                    MediaPlayer.Play(sm.mainMenu);
                    MediaPlayer.Volume = 0.5f;
                    temp--;
                }
            }

            if (CurrentState == Container.GameState.Playing)
            {
                buttonReturn();
                //Tắt nhạc nền menu, bật nhạc nền epic
                if (temp == 0)
                {
                    MediaPlayer.Play(sm.bgMusic);
                    MediaPlayer.Volume = 0.5f;
                    temp--;
                }
                #region     Thông tin về đợt tấn công
                if (enemyPerWave > 0)           //Nếu chưa hết một lượt tấn công
                {
                    timer += 3;
                    if (timer > Container.timeBetweenEnemy)
                    {
                        if (wave <= Container.waveQuantity)
                        {
                            int sttEnemy = 0;
                            while (true)
                            {
                                if (Container.enemyEachWave[wave-1][sttEnemy] > 0)
                                {
                                    enemyList.Add(new Enemy(enemyTextureList[sttEnemy], healthbar, sttEnemy+1));
                                    Container.enemyEachWave[wave-1][sttEnemy]--;
                                    timer = 0;
                                    break;
                                }
                                else
                                {
                                    sttEnemy++;
                                }
                            }
                            enemyPerWave--;
                            if (enemyPerWave == 0)
                                timer = 0;
                        }
                    }
                }
                #endregion

                #region  Hết một lượt tấn công, chuyển sang lượt tấn công khác
                else
                {
                    nextWaveTimer -= 5;
                    if (nextWaveTimer<0)
                    {
                        wave++;
                        enemyPerWave = Container.enemyPerWave;
                        nextWaveTimer = Container.timeBetweenWave;
                    }
                }
                #endregion 

                #region Tạo enemy
                foreach (Enemy enemy in enemyList)
                {
                    if (enemy.isWayPointsSet == false)      //Nếu chưa setwaypoint thì bắt đầu set
                    {
                        Random rand = new Random();
                        int k = rand.Next(0, 20);
                        level.LoadWayPoint(k, enemy.isNavy);
                        enemy.SetWaypoints(level.WayPoints);
                        enemy.isWayPointsSet = true;
                    }
                }
                #endregion

                #region Update enemyList
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].IsAlive)
                        enemyList[i].Update(gameTime);
                    else
                    {
                        //sm.explodeSound.Play();
                        enemyList.RemoveAt(i);
                        i--;
                    }
                }
                if(enemyList.Count<=0)          //Kiểm tra thua
                {
                    if (wave == Container.waveQuantity && enemyPerWave==0)
                        wave++;
                }
                #endregion

                #region Update hiệu ứng explosion
                for(int i=0;i<explosionAnimation.Count;i++)
                {
                    if(explosionAnimation[i].IsVisible)
                        explosionAnimation[i].Update(gameTime);
                    else
                    {
                        explosionAnimation.RemoveAt(i);
                        i--;
                    }
                }
                #endregion

                player.Update(gameTime, enemyList, btM.RetButton);

                base.Update(gameTime);
            }

            if(CurrentState==Container.GameState.Win)          //Trạng thái thua
            {
                #region Update hiệu ứng explosion
                for (int i = 0; i < explosionAnimation.Count; i++)
                {
                    if (explosionAnimation[i].IsVisible)
                        explosionAnimation[i].Update(gameTime);
                    else
                    {
                        explosionAnimation.RemoveAt(i);
                        i--;
                    }
                }
                #endregion
            }
            hud = new HUD(new Vector2(mouse.X, mouse.Y), playerScoreFont, new Vector2(0,50),healthbar);
            hud.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Tu update MainMenu
            spriteBatch.Begin();

            #region GameState = Playing
            if (CurrentState == Container.GameState.Playing)
            {
                level.Draw(spriteBatch);
                player.Draw(spriteBatch);
                foreach (Enemy e in enemyList)
                    e.Draw(spriteBatch);
                foreach (Animation a in explosionAnimation)
                    a.Draw(spriteBatch);
            }
            #endregion

            #region GameState = Pausing
            if (CurrentState == Container.GameState.Pausing)
            {
                level.Draw(spriteBatch);
                player.Draw(spriteBatch);
                foreach (Enemy e in enemyList)
                    e.Draw(spriteBatch);
                foreach (Animation a in explosionAnimation)
                    a.Draw(spriteBatch);
            }
            #endregion

            #region GameState = GameOver
            if (CurrentState==Container.GameState.GameOver)
            {
                level.Draw(spriteBatch);
                player.Draw(spriteBatch);
            }
            #endregion

            #region GameState = Win
            if (CurrentState == Container.GameState.Win)
            {
                level.Draw(spriteBatch);
                player.Draw(spriteBatch);
                foreach (Enemy e in enemyList)
                    e.Draw(spriteBatch);
                foreach (Animation a in explosionAnimation)
                    a.Draw(spriteBatch);
            }
            #endregion

            //Vẽ các thanh máu, tiền
            hud.Draw(spriteBatch);

            //Vẽ các button
            btM.Draw(spriteBatch, CurrentState);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Load Input Game
        public void LoadGame(ContentManager Content,int k)
        {
            enemyList = new List<Enemy>();      //Khởi tạo lại danh sách Enemy
            Container.enemyEachWave = new List<int[]>();    
            Input.LoadInput(k);     //Đọc các thông tin màn chơi từ file
            sm.LoadContent(Content);    //Load âm thanh
            level = new Level(map, k);      //Vẽ bản đồ màn chơi
            level.LoadContent(Content);
            player = new Player(map, k);
            player.LoadContent(Content);
            nextWaveTimer = Container.timeBetweenWave;
            wave = 1;
            Container.HP = 100;
            explosionAnimation = new List<Animation>();
        }

       void buttonReturn()
        {
            if (btM.RetButton == 14)        //Nếu nút replay được nhấn
            {
                LoadContent();
                btM.RetButton = -1;
                btM.replayButton.isClicked = false;
            }

           if(btM.RetButton==13)
           {
               btM.RetButton = -1;
               btM.nextWave.isClicked = false;
               if(enemyPerWave<=0)
               {
                   nextWaveTimer = 0;
               }
           }
           if(btM.RetButton==12)        //Nút tắt âm thanh được nhấn
           {
               btM.RetButton = -1;
               MediaPlayer.Volume = 0.5f;
               SoundEffect.MasterVolume = 0.5f;
           }
           if(btM.RetButton==11)
           {
               btM.RetButton = -1;
               MediaPlayer.Volume = 0f;
               SoundEffect.MasterVolume = 0f;
           }
           if(btM.RetButton==8)      //Skill 3, Giết toàn bộ enemy
           {
               btM.RetButton = -1;
               btM.specialSkill3Button.isClicked = false;
               if (goldHave > Container.skill3Price)
               {
                   foreach (Enemy e in enemyList)
                   {
                       e.IsAlive = false;
                       Animation a=new Animation(e.Center,Content.Load<Texture2D>("animation"));
                       a.IsVisible = true;
                       explosionAnimation.Add(a);
                   }
                   goldHave -= Container.skill3Price;
               }
           }
        }
    }
}
