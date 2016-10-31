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
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);
        public Vector2 size;
        public towerButton(Texture2D newTexture, GraphicsDevice graphics, Vector2 tempSize)
        {
            texture = newTexture;
            //scrW = 900 scrH 600
            //imW      imH
            size = tempSize;
        }
        bool down;
        public bool isClicked;
        public void Update(MouseState mouse,int goldHave)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (goldHave < Price)
                colour = colour;
         
            //if (mouseRectangle.Intersects(rectangle))
            //{
            //    if (colour.A == 255) down = false;
            //    if (colour.A == 0) down = true;
            //    if (down) colour.A += 3; else colour.A -= 3;
            //    if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;

            //}
            //else if (colour.A < 255)
            //{
            //    colour.A += 3;
            //    isClicked = false;
            //}
        }
        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
        public void setPrice(int price)
        {
            Price = price;
        }
    }
}
