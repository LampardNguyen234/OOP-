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
    class RocketTower:Tower
    {

        public RocketTower(Texture2D texture, int level, Vector2 position, Texture2D baseTexture, Texture2D bulletTexture):
            base(level,position,baseTexture,texture,bulletTexture)
        {
            radius = (float)350;
            attack = 50;
            price = 1200;
            smallestRange = radius;
            timer = 5000f; 
            interval = 5000f;
        }


        //Update
        public void Update(GameTime gameTime, List<Enemy>enemyList)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(target==null)
            {
                GetClosestEnemy(enemyList);   
            }
            
            if (timer > interval)
            {
                if (frame < frameMaxX - 1)      
                {
                    if (target != null)
                    {
                        //Tạo bullet
                        Bullet bullet = new Bullet(position, level, bulletTexture, target.Center);
                        bulletList.Add(bullet);
                        //Thay đổi khung hình
                        frame++;

                    }
                    else
                        frame = 0;
                    timer = 0;      
                }
                else
                    frame = 0;
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                if (target==null)         //nếu target chết, thì Kill bullet
                    bullet.Kill();
                if (!CheckBulletOutOfTower(bullet))  //Khi chưa ra khỏi tower thì hướng về target
                    bullet.SetRotation(rotation);
                else
                {
                    if (target != null)
                        bullet.FollowTarget(target.Center); //Nếu đã ra khỏi tower thì đuổi theo target (Đối với tên lửa)
                }
                if(Collision(bullet))               //Trường hợp bullet đụng target
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
            
            FaceTarget();           //Quay tower về phía target
            if (!IsInRange(target) || target.IsAlive==false)
                target = null;
            BoundingBox = new Rectangle(frame * Container.towerSize, 0, Container.towerSize, Container.towerSize);      //Cập nhật frame của Tower
        }

    }
}
