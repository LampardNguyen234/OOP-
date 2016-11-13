using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TowerDefenseOOP
{
    class ackackTower:Tower
    {
        public ackackTower(Texture2D texture, int level, Vector2 position, Texture2D baseTexture, Texture2D bulletTexture, Texture2D explosionTexture) :
            base(level,position,baseTexture,texture,bulletTexture,explosionTexture)
        {
            radius = Container.radiusList[0];
            attack = Container.attackList[0];
            price = Container.priceList[0];
            smallestRange = radius;
            timer = 500f;
            interval = 500f;
        }

        //Update
        public void Update(GameTime gameTime, List<Enemy> enemyList)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (target == null)
            {
                GetClosestEnemy(enemyList); //Tìm enemy gần nhất
            }

            if (timer > interval)
            {
                if (target != null)
                {
                    isTargetAttacked = true;
                    //Tạo bullet
                    Bullet bullet = new Bullet(position, level, bulletTexture, target);
                    bulletList.Add(bullet);
                    Game1.sm.towerShoot[0].Play();
                }
                timer = 0;
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                if (target == null)
                    bullet.Kill();
                if (!CheckBulletOutOfTower(bullet))
                    bullet.SetRotation(rotation);
                if (Collision(bullet))
                {
                    target.CurrentHP -= attack;
                    bullet.Kill();
                }

               
                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
                bullet.Update(gameTime);
            }

            FaceTarget();//Quay tower hướng về target
            base.Update(gameTime);
        }
    }
}