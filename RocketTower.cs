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
    class RocketTower:Tower
    {

        List<Enemy> oldTargetList = new List<Enemy>();  //Danh sách chưa các target ra khỏi bán kính nhưng chưa chết
        public RocketTower(Texture2D texture, int level, Vector2 position, Texture2D baseTexture, Texture2D bulletTexture, Texture2D explosionTexture) :
            base(level,position,baseTexture,texture,bulletTexture, explosionTexture)
        {
            radius = Container.radiusList[4];
            attack = Container.attackList[4];
            price = Container.priceList[4];
            smallestRange = radius;
            timer = 2000f;
            interval = 2000f;
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
                if (frame < frameMax - 1)      
                {
                    if (target != null)
                    {
                        //Tạo bullet
                        isTargetAttacked = true;
                        Bullet bullet = new Bullet(position, level, bulletTexture, target);
                        bulletList.Add(bullet);
                        target.isLockedByRocket = true;
                        target.Update(gameTime);
                        Game1.sm.towerShoot[4].Play();
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
                        bullet.FollowTarget(); //Nếu đã ra khỏi tower thì đuổi theo target (Đối với tên lửa)
                }
                if(bullet.Collision())               //Trường hợp bullet đụng target
                {
                    bullet.target.CurrentHP -= attack;
                    bullet.target.isLockedByRocket = false;
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
            if (target != null)
            {
                if (!IsInRange(target) && target.IsAlive)
                    oldTargetList.Add(target);
            }
            for (int i = 0; i < oldTargetList.Count;i++)        //Cập nhật hoặc xóa oldTarget
            {
                if (oldTargetList[i].IsAlive == false)
                {
                    oldTargetList.RemoveAt(i);
                    i--;
                }
            }
           sourceRectangle = new Rectangle(frame * Container.towerSize, 0, Container.towerSize, Container.towerSize);      //Cập nhật frame của Tower
           base.Update(gameTime);
        }

    }
}
