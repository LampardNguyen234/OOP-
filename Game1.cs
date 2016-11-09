﻿using System;
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
        public static SpriteFont playerScoreFont;
        float timer;
        int enemyPerWave;
        public static int goldHave = Container.startingScore;
        public static SoundManager sm = new SoundManager();
        Container.GameState CurrentState = Container.GameState.MainMenu;
        //button o menu game
        
        MouseState mouse;
        buttonManager btM;          //quản lý các button
        HUD hud;            //In điểm

        Input input = new Input();
        int wave = 0;
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
            Mouse.SetPosition(0, 0);
            btM = new buttonManager(graphics);
            Input.LoadInput(1);
        }

        protected override void Initialize()
        {
            SpriteBatchEx.GraphicsDevice = GraphicsDevice;  //Khởi tạo công cụ vẽ
            base.Initialize();
            Mouse.SetPosition(0, 0);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            enemyPerWave = Container.enemyPerWave;
            playerScoreFont = Content.Load<SpriteFont>("georgia");      //MỚi thêm
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mouse=Mouse.GetState();
            int maplevel = 1;
            level = new Level(map, maplevel);
            level.LoadContent(Content);
            player = new Player(map, maplevel);
            player.LoadContent(Content);
            healthbar = Content.Load<Texture2D>("healthbar");
            for (int i = 0; i < Container.numberOfEnemies; i++)
            {
                Texture2D enemy = Content.Load<Texture2D>("enemy_0" + i.ToString("d2"));
                enemyTextureList.Add(enemy);
            }
            timer = 0;
            enemyPerWave = Container.enemyPerWave;
            btM.LoadContent(Content);
            hud = new HUD(new Vector2(mouse.X, mouse.Y), playerScoreFont, new Vector2(0, 50));   //Xóa;

            sm.LoadContent(Content);    //Load âm thanh
            SoundEffect.MasterVolume = 0.1f;
        }

        protected override void Update(GameTime gameTime)
        {
            //Tu update MainMenu
            //<Start>
            mouse = Mouse.GetState();
            btM.Update(CurrentState, mouse, goldHave, gameTime);
            CurrentState = btM.GameState;
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
                //Tắt nhạc nền menu
                MediaPlayer.Stop();
                if (enemyPerWave > 0)           //Nếu chưa hết một lượt tấn công
                {
                    timer += 3;
                    if (timer > Container.timeBetweenEnemy)
                    {
                        if (wave < Container.waveQuantity)
                        {
                            int sttEnemy = 0;
                            while (true)
                            {
                                if (Container.enemyEachWave[wave][sttEnemy] > 0)
                                {
                                    enemyList.Add(new Enemy(enemyTextureList[sttEnemy], healthbar, sttEnemy+1));
                                    Container.enemyEachWave[wave][sttEnemy]--;
                                    timer = 0;
                                    break;
                                }
                                else
                                    sttEnemy++;
                            }
                            enemyPerWave--;
                            if (enemyPerWave == 0)
                                timer = 0;
                        }
                    }
                }
                else
                {
                    timer += 2;
                    if (timer > Container.timeBetweenWave)
                    {
                        wave++;
                        enemyPerWave = Container.enemyPerWave;
                    }
                }
                foreach (Enemy enemy in enemyList)
                {
                    if (enemy.isWayPointsSet == false)      //Nếu chưa setwaypoint thì bắt đầu set
                    {
                        Random rand = new Random();
                        int k = rand.Next(0, 20);
                        level.LoadWayPoint(k);
                        enemy.SetWaypoints(level.WayPoints);
                        enemy.isWayPointsSet = true;
                    }
                }

                player.Update(gameTime, enemyList, btM.RetButton);
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
                base.Update(gameTime);
                player.Update(gameTime, enemyList, btM.RetButton);
            }
            hud = new HUD(new Vector2(mouse.X, mouse.Y), playerScoreFont, new Vector2(0,50));
            hud.Update(gameTime);
            base.Update(gameTime);

        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Tu update MainMenu

            spriteBatch.Begin();
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
            hud.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
