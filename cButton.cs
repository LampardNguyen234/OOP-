﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseOOP
{
    /// <summary>
    /// 
    /// </summary>
    class cButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        MouseState oldMouseState;
        Color colour = new Color(255, 255, 255, 255);
        public Vector2 size;
        public cButton(Texture2D newTexture, GraphicsDevice graphics, Vector2 tempSize)
        {
            texture = newTexture;
            //scrW = 900 scrH 600
            //imW      imH
            size = tempSize;
        }
        bool down;
        public bool isClicked;
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
                if (mouse.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
                    isClicked = true;
                else
                    isClicked = false;

            }
            else 
            {
                if (colour.A < 255)
                    colour.A += 3;
                isClicked = false;
            }
            oldMouseState = mouse;
        }
        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }
}