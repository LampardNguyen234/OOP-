using System;
using System.Collections.Generic;
using System.Linq;
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
        Player player;
        Enemy enemy;
        Level level;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Container.scrWidth;
            graphics.PreferredBackBufferHeight = Container.scrHeight;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
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
            Texture2D enemyTexture = Content.Load<Texture2D>("enemy_009");
            Texture2D healthbar = Content.Load<Texture2D>("healthbar");
            enemy = new Enemy(enemyTexture, healthbar, 1);
            level.LoadWayPoint(10);
            enemy.SetWaypoints(level.WayPoints);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            player.Update(gameTime);
            enemy.Update(gameTime);
                base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
