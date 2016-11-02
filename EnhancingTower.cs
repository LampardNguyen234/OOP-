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
    /// <summary>
    /// Tháp tăng cường sức mạnh
    /// </summary>
    class EnhancingTower:Tower
    {
        
        public EnhancingTower(Texture2D texture, int level, Vector2 position, Texture2D baseTexture, Texture2D bulletTexture) :
            base(level,position,baseTexture,texture,bulletTexture)
        {
            radius = Container.radiusMax /2;
            attack = 0;
            price = 500;
            smallestRange=0;
            timer = 0f; 
            interval = 0f;
        }


    }
}
