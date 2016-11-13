using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LineBatch;
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
        Texture2D healthbarTexture;
        //Constructor
        public HUD(Vector2 position,SpriteFont playerScoreFont, Vector2 hudPos, Texture2D healthbarTexture)
        {
            this.healthbarTexture = healthbarTexture;
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
            {   
                //Vẽ khung hình chứa máu, tiền, lượt tấn công
                Color color = Color.Purple;
                Color color2 = Color.Black;
                int width = 3;
                spriteBatch.DrawLine(new Vector2(0, 30), new Vector2(897, 30), color, width);       //Lượt tấn công
                spriteBatch.DrawLine(new Vector2(150, 0), new Vector2(150, 30), color, width);      //Máu
                spriteBatch.DrawLine(new Vector2(900, 0), new Vector2(900, 30), color, width);      //
                spriteBatch.DrawLine(new Vector2(600, 0), new Vector2(600, 30), color, width);      //Tiền
                spriteBatch.DrawLine(new Vector2(700, 0), new Vector2(700, 30), color, width);      //Thời gian lượt tiếp theo
                spriteBatch.DrawLine(Container.HPPos, Container.HPPos + new Vector2(0, 13), color2, width);
                spriteBatch.DrawLine(Container.HPPos, Container.HPPos + new Vector2(320, 0), color2, width);
                spriteBatch.DrawLine(Container.HPPos + new Vector2(0, 13), Container.HPPos + new Vector2(320, 13), color2, width);
                spriteBatch.DrawLine(Container.HPPos + new Vector2(320 +width, 0), Container.HPPos + new Vector2(320+width, 13), color2, width);
                spriteBatch.Draw(healthbarTexture, new Rectangle(250,13,Container.HP*320/100,10), Color.Green);
                spriteBatch.Draw(healthbarTexture, new Rectangle(250 + Container.HP * 320 / 100, 13, 320 - Container.HP * 320 / 100, 10), Color.Red);

                int wave = Game1.wave;
                if (wave > Container.waveQuantity)
                    wave--;
                spriteBatch.DrawString(playerScoreFont, "LƯỢT: " + wave + "/" + Container.waveQuantity, new Vector2(10, 5), Color.Yellow);
                spriteBatch.DrawString(playerScoreFont, "MÁU: " + Container.HP, new Vector2(155, 5), Color.Yellow);
                spriteBatch.DrawString(playerScoreFont, "$$: " + Game1.goldHave, new Vector2(605, 5), Color.YellowGreen);
                spriteBatch.DrawString(playerScoreFont, "LƯỢT TIẾP: " + Game1.nextWaveTimer, new Vector2(705, 5), Color.YellowGreen);
            }
        }
    }
}
