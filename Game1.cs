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
        Map map = new Map();
        Texture2D healthbar;
        Player player;
        List<Enemy> enemyList = new List<Enemy>();
        List<Texture2D> enemyTextureList = new List<Texture2D>();
        Level level;
        float timer;
        int enemyPerWave;
<<<<<<< HEAD
        int goldHave = 0;
=======
        // Tu update 10:17 31/10 Button
        //<Start>
        // Tien dang co!!!!!!!!!! Update se chuyen thanh bien luu gia tri tien.
        int goldHave = 0;
        // Phan button 
        enum GameState
        {
            MainMenu = 0,
            Intro,
            Playing,
            Tacgia
>>>>>>> origin/master

        }
        GameState CurrentState = GameState.MainMenu;
        //button o menu game
<<<<<<< HEAD
        
        MouseState mouse;
        buttonManager btM;
        
        //
=======

        cButton btnPlay;
        cButton btnHuongDan;
        cButton btnTacGia;
        cButton btnBack;

        // button trong game

        towerButton btnTower1;
        towerButton btnTower2;
        towerButton btnTower3;
        towerButton btnTower4;
        towerButton btnTower5;
        towerButton btnTower6;
        //<end>

>>>>>>> origin/master
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Container.scrWidth;
            graphics.PreferredBackBufferHeight = Container.scrHeight;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
<<<<<<< HEAD
            btM = new buttonManager(graphics);
=======
>>>>>>> origin/master

        }

        protected override void Initialize()
        {
            SpriteBatchEx.GraphicsDevice = GraphicsDevice;  //Khởi tạo công cụ vẽ
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            int maplevel = 1;
            level = new Level(map, maplevel);
            level.LoadContent(Content);
            player = new Player(map, maplevel);
            player.LoadContent(Content);
            healthbar = Content.Load<Texture2D>("healthbar");
            for (int i = 0; i < Container.numberOfEnemies;i++ )
            {
                Texture2D enemy = Content.Load<Texture2D>("enemy_0" + i.ToString("d2"));
                enemyTextureList.Add(enemy);
            }
            timer = 0;
            enemyPerWave = Container.enemyPerWave;
<<<<<<< HEAD
            btM.LoadContent(Content);
=======
            
            //Tu update Button
            //<Start>
            // Load button
            btnPlay = new cButton(Content.Load<Texture2D>("buttonPlay"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            btnPlay.SetPosition(Container.btnPlayPosition);

            btnHuongDan = new cButton(Content.Load<Texture2D>("buttonHD"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            btnHuongDan.SetPosition(Container.btnIntroPosition);

            btnTacGia = new cButton(Content.Load<Texture2D>("buttonTG"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            btnTacGia.SetPosition(Container.btnTacGiaPosition);

            btnBack = new cButton(Content.Load<Texture2D>("buttonBack"), graphics.GraphicsDevice, Container.btnBackSize);
            btnBack.SetPosition(Container.btnBackPosition);

            btnTower1 = new towerButton(Content.Load<Texture2D>("tower__000"), graphics.GraphicsDevice, Container.btnTowerSize);
            btnTower1.SetPosition(Container.btnTower1Position);
            btnTower1.setPrice(Container.tower1Price);
            btnTower2 = new towerButton(Content.Load<Texture2D>("tower2"), graphics.GraphicsDevice, Container.btnTowerSize);
            btnTower2.SetPosition(Container.btnTower2Position);

            btnTower3 = new towerButton(Content.Load<Texture2D>("tower3"), graphics.GraphicsDevice, Container.btnTowerSize);
            btnTower3.SetPosition(Container.btnTower3Position);

            btnTower4 = new towerButton(Content.Load<Texture2D>("tower4"), graphics.GraphicsDevice, Container.btnTowerSize);
            btnTower4.SetPosition(Container.btnTower4Position);

            btnTower5 = new towerButton(Content.Load<Texture2D>("tower5"), graphics.GraphicsDevice, Container.btnTowerSize);
            btnTower5.SetPosition(Container.btnTower5Position);

            btnTower6 = new towerButton(Content.Load<Texture2D>("tower6"), graphics.GraphicsDevice, Container.btnTowerSize);
            btnTower6.SetPosition(Container.btnTower6Position);
            //<End>
        }

        protected override void UnloadContent()
        {
            
>>>>>>> origin/master
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //Tu update MainMenu
            //<Start>
<<<<<<< HEAD
            mouse = Mouse.GetState();
            btM.Update(CurrentState, mouse, goldHave, gameTime);
            CurrentState = btM.GameState;
            if (CurrentState == Container.GameState.Playing)
            {
                timer += 2;
                if (timer > Container.timeBetweenEnemy)
                {
                    if (enemyPerWave > 0)
                    {
                        timer = 0f;
                        enemyList.Add(new Enemy(enemyTextureList[3], healthbar, 4));
                        enemyPerWave--;
                    }
                }
                foreach (Enemy enemy in enemyList)
                {
                    if (enemy.isWayPointsSet == false)
                    {
                        Random rand = new Random();
                        int k = rand.Next(0, 20);
                        level.LoadWayPoint(k);
                        enemy.SetWaypoints(level.WayPoints);
                        enemy.isWayPointsSet = true;
                    }
                }

                player.Update(gameTime, enemyList);
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].IsAlive)
                        enemyList[i].Update(gameTime);
                    else
                    {
                        enemyList.RemoveAt(i);
                        i--;
                    }
                }

                //animation.Update(gameTime);
                base.Update(gameTime);
                player.Update(gameTime, enemyList);
            }

=======
            switch (CurrentState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true)
                    {
                        CurrentState = GameState.Playing;

                    }
                    else if (btnTacGia.isClicked == true)
                    {
                        CurrentState = GameState.Tacgia;

                    }
                    else if (btnHuongDan.isClicked == true)
                    {
                        CurrentState = GameState.Intro;

                    }
                    btnPlay.Update(mouse);
                    btnTacGia.Update(mouse);
                    btnHuongDan.Update(mouse);
                    //btnBack.Update(mouse);
                    break;
                case GameState.Intro:
                    if (btnBack.isClicked == true)
                        CurrentState = GameState.MainMenu;
                    btnBack.Update(mouse);
                    break;
                case GameState.Tacgia:
                    if (btnBack.isClicked == true)
                        CurrentState = GameState.MainMenu;
                    btnBack.Update(mouse);
                    break;
                case GameState.Playing:
                    // Update trong Playing them o day
                    //<Start>
                        timer += 2;
                        if (timer > Container.timeBetweenEnemy)
                        {
                            if (enemyPerWave > 0)
                            {
                                timer = 0f;
                                enemyList.Add(new Enemy(enemyTextureList[3],healthbar,4));
                                enemyPerWave--;
                            }
                        }
                        foreach (Enemy enemy in enemyList)
                        {
                            if (enemy.isWayPointsSet == false)
                            {
                                Random rand = new Random();
                                int k = rand.Next(0, 20);
                                level.LoadWayPoint(k);
                                enemy.SetWaypoints(level.WayPoints);
                                enemy.isWayPointsSet = true;
                            }
                        }
        
                        player.Update(gameTime,enemyList);
                        for (int i = 0; i < enemyList.Count; i++)
                        {
                            if (enemyList[i].IsAlive)
                                enemyList[i].Update(gameTime);
                            else
                            {
                                enemyList.RemoveAt(i);
                                i--;
                            }
                        }

            //animation.Update(gameTime);
>>>>>>> origin/master
            base.Update(gameTime);
                    player.Update(gameTime,enemyList);
                    

                    

                    //<End>
                    btnTower1.Update(mouse, goldHave);
                    btnTower2.Update(mouse, goldHave);
                    btnTower3.Update(mouse, goldHave);
                    btnTower4.Update(mouse, goldHave);
                    btnTower5.Update(mouse, goldHave);
                    btnTower6.Update(mouse, goldHave);
                   
               
                    break;
             }
             base.Update(gameTime);
           
        }
        protected override void Draw(GameTime gameTime)
        {
            //Tu update MainMenu
            spriteBatch.Begin();
<<<<<<< HEAD
            //ButtonManager.Draw(spriteBatch, CurrentState);
            btM.Draw(spriteBatch, CurrentState);
            
            if (CurrentState == Container.GameState.Playing)
            {
                
                //animation.Draw(spriteBatch);
                // Cac ban ve trong playing thi Update o day
                //<Start>
                level.Draw(spriteBatch);
                player.Draw(spriteBatch);
                foreach (Enemy e in enemyList)
                    e.Draw(spriteBatch);
            }

=======
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch (CurrentState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);

                    btnPlay.Draw(spriteBatch);
                    btnHuongDan.Draw(spriteBatch);
                    btnTacGia.Draw(spriteBatch);
                    break;
                case GameState.Intro:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("HuongDan"), new Rectangle(200, 50, 800, 600), Color.White);
                    btnBack.Draw(spriteBatch);
                    break;
                case GameState.Tacgia:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("TacGia"), new Rectangle(200, 50, 800, 600), Color.White);
                    btnBack.Draw(spriteBatch);

                    break;
                case GameState.Playing:

                    spriteBatch.Draw(Content.Load<Texture2D>("PlayMenu"), Container.playMenuPosition, Color.White);
                   
                    //animation.Draw(spriteBatch);
                    // Cac ban ve trong playing thi Update o day
                    //<Start>
                    level.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    foreach (Enemy e in enemyList)
                        e.Draw(spriteBatch);
                   
                    btnTower1.Draw(spriteBatch);
                    btnTower2.Draw(spriteBatch);
                    btnTower3.Draw(spriteBatch);
                    btnTower4.Draw(spriteBatch);
                    btnTower5.Draw(spriteBatch);
                    btnTower6.Draw(spriteBatch);
                    //<End>
                    break;
            }
>>>>>>> origin/master
            spriteBatch.End();

            
            base.Draw(gameTime);
        }
    }
}
