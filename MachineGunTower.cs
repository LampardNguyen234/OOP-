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
    class MachineGunTower:Tower
    {

        //Constructor
        public MachineGunTower(Texture2D texture, int level, Vector2 postition, Texture2D baseTexture, Texture2D bulletTexture) :
            base(level,postition,baseTexture,texture,bulletTexture)
        {
            radius = 100f;
            attack = 5;
            price = 70;
            smallestRange = radius;
            timer = 400f;
            interval = 300f;
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
                    //Tạo bullet
                    Bullet bullet = new Bullet(position, level, bulletTexture, target.Center);
                    bulletList.Add(bullet);
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
            if (!IsInRange(target) || target.IsAlive == false)
                target = null;
        }
    }
}
