﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LineBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TowerDefenseOOP
{
    class LazerTower:Tower
    {
        public LazerTower(Texture2D texture, int level, Vector2 position, Texture2D baseTexture, Texture2D bulletTexture, Texture2D explosionTexture) :
            base(level,position,baseTexture,texture,bulletTexture,explosionTexture)
        {
            radius = 5*Container.radiusMax /10;
            attack = 7 * Container.attackMax / 10 ;
            price = 500;
            smallestRange=0; 
            interval =Container.intervalmax /3;
            timer = interval;
        }

        //Hàm update
        public void Update(GameTime gameTime, List<Enemy> enemyList)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (target == null) //Nếu target bằng null, tìm target mới
            {
                GetClosestEnemy(enemyList);
            }
            if (timer > interval)
            {
                if (target != null)
                {
                    isTargetAttacked = true;
                    target.CurrentHP -= attack;
                    timer = 0;
                }
            }
            FaceTarget();           //Quay tower về phía target
            base.Update(gameTime);
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsInRange(target))      //Nếu target nằm trong bán kính thì vẽ
                spriteBatch.DrawLine(position, target.Center, Color.Red);
            base.Draw(spriteBatch);
        }
    }
}