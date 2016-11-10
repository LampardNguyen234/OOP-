using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace TowerDefenseOOP
{
    class HUD
    {
        public int playerScore, screenWidth, screenHeight;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePos;
        public Vector2 position;
        public bool showHUD;

        //Constructor
        public HUD(Vector2 position,SpriteFont playerScoreFont, Vector2 hudPos)
        {
            this.playerScoreFont = playerScoreFont;
            this.position = position;
            playerScore = 0;
            showHUD = true;
            playerScoreFont = null;
            playerScorePos = hudPos;
        }


        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showHUD == true)
                spriteBatch.DrawString(playerScoreFont, "Score - " +  position.X +"," +position.Y, playerScorePos, Color.Red);
        }
    }
}
