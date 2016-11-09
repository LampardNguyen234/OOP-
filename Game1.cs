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
        int goldHave = 0;

        Container.GameState CurrentState = Container.GameState.MainMenu;
        //button o menu game
        
        MouseState mouse;
        buttonManager btM;
        
        //
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Container.scrWidth;
            graphics.PreferredBackBufferHeight = Container.scrHeight;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            btM = new buttonManager(graphics);

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
            for (int i = 0; i < Container.numberOfEnemies; i++)
            {
                Texture2D enemy = Content.Load<Texture2D>("enemy_0" + i.ToString("d2"));
                enemyTextureList.Add(enemy);
            }
            timer = 0;
            enemyPerWave = Container.enemyPerWave;
            btM.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {

           
            //Tu update MainMenu
            //<Start>
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

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
