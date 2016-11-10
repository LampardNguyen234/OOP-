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
    class towerButton
    {
        public static int Price;
        Texture2D texture;
        Vector2 position, origin;
        Rectangle rectangle;
        Texture2D buttondetail;
        Color colour = new Color(255, 255, 255, 255);
        public Vector2 size;
        public towerButton(Texture2D newTexture,Texture2D buttonDetail, GraphicsDevice graphics, Vector2 tempSize)
        {
            texture = newTexture;
            buttondetail = buttonDetail;
            //scrW = 900 scrH 600
            //imW      imH
            size = tempSize;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
        bool hover;
        public bool isClicked;
        public void Update(MouseState mouse,int goldHave)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
       

            if (mouseRectangle.Intersects(rectangle))
            {
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
                hover = true;
               
            }
            else
            {   
                isClicked = false;
                hover = false;
            }
        }
        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.baseButton, position, null, colour, 0, origin, 0.7f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, position, null,colour,0,origin,1f,SpriteEffects.None,0f);
            if (hover)
            {
                spriteBatch.Draw(Game1.baseButton, position, null, colour, 0, origin, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture, position, null, colour, 0, origin, 1f/0.7f, SpriteEffects.None, 0f);
                spriteBatch.Draw(buttondetail, Container.towerDetailPosition, null, colour, 0, new Vector2(0,0), 1f, SpriteEffects.None, 0f);
            }
        }
        public void setPrice(int price)
        {
            Price = price;
        }
    }
}
