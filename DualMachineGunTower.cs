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
    class DualMachineGunTower:Tower
    {
        float change;
        Vector2 changeT;
        Texture2D animation;
        //Constructor
        public DualMachineGunTower(Texture2D texture, int level, Vector2 position, Texture2D baseTexture, Texture2D bulletTexture, Texture2D explosionTexture) :
            base(level,position,baseTexture,texture,bulletTexture,explosionTexture)
        {
            radius = 100f;
            attack = 10;
            price = 100;
            smallestRange = radius;
            timer = 400f;
            interval = 400f;
            change = 10;
            changeT = Vector2.Transform(new Vector2(0, change)
                , Matrix.CreateRotationZ(rotation+0.1f));
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
                    changeT = Vector2.Transform(new Vector2(0, change)
                , Matrix.CreateRotationZ(rotation+0.5f));
                    //Tạo bullet
                    Game1.sm.towerShoot[1].Play();
                    Bullet bullet1 = new Bullet(position+changeT, level, bulletTexture, target);
                    bulletList.Add(bullet1);
                    Game1.sm.towerShoot[1].Play();
                    Bullet bullet2 = new Bullet(position-changeT, level, bulletTexture, target);
                    bulletList.Add(bullet2);
                    isTargetAttacked = true;
                }

                timer = 0;
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                if (target == null)
                    bullet.Kill();
                bullet.SetRotation(rotation);
                //Kiểm tra đạn collision với target
                if (Collision(bullet))
                {
                    target.CurrentHP -= attack;
                    bullet.Kill();
                }
                //Kiểm tra xem đạn đã chết chưa
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
