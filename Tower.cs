using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDefenseOOP
{
    class Tower
    {
        #region Khai báo
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        protected   Vector2 origin, origin1;
        protected Vector2 center;
        protected Texture2D towerTexture, baseTexture, bulletTexture;
        protected bool isAlive;

        public  bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        protected float rotation;
        protected float radius;
        protected float smallestRange;

        protected List<Bullet> bulletList = new List<Bullet>();

       // Animation animation;

        public float Radius
        {
          get { return radius; }
        }
        protected int attack;
        protected int price;
        protected int level;
        protected int frame;   //Thay đổi hình ảnh của Tower
        protected int frameMaxX;      //Số frame theo chiều rộng (Width)
        public Vector2 Center
        {
            get { return this.center; }
            set { this.center = value; }
        }


        public Enemy target;

        public Enemy Target
        {
            get { return target; }
            set { target = value; }
        }
        protected float timer;
        protected float interval;
        protected Rectangle BoundingBox;   //Khung hình texture
        #endregion
        //Constructor

        public Tower(int level, Vector2 position, Texture2D baseTexture, Texture2D towerTexture, Texture2D bulletTexture)
        {
            this.bulletTexture = bulletTexture;
            this.baseTexture = baseTexture;
            this.towerTexture = towerTexture;
            isAlive = true;
            rotation = 0;
            this.position = position;
            price = 50 * level;
            origin = new Vector2(Container.towerSize / 2, Container.towerSize / 2); //origin của tower
            origin1 = new Vector2(baseTexture.Width / 2, baseTexture.Height / 2);  //origin của base
            smallestRange = radius = 0;
            this.level = level;
            frame = 0;
            frameMaxX = towerTexture.Width / Container.towerSize;
            BoundingBox = new Rectangle(frame * Container.towerSize, 0, Container.towerSize, Container.towerSize);
        }

        //Kiểm tra xem enemy có nằm trong bán kính không
        public bool IsInRange(Enemy target)
        {
            if (target == null)
                return false;
            if (Vector2.Distance(center, target.Center) <= radius)
                return true;

            return false;
        }

        //Tìm Enemy gần nhất
        public void GetClosestEnemy(List<Enemy> enemyList)
        {
            target = null;
            foreach (Enemy enemy in enemyList)
            {
                if (Vector2.Distance(position, enemy.Center) < radius)
                {
                    smallestRange = Vector2.Distance(position, enemy.Center);
                    target = enemy;
                    return;
                }
            }
        }

        //FaceTarget: Hướng về target
        public void FaceTarget()
        {
            if (target == null)
                return;
            Vector2 direction = position - target.Center;
            direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                spriteBatch.Draw(baseTexture, position, null, Color.White, 0f, origin1, 0.7f, SpriteEffects.None, 0f);
                spriteBatch.Draw(towerTexture, position, BoundingBox, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
            }
            foreach (Bullet bullet in bulletList)
            {
                if (CheckBulletOutOfTower(bullet))
                    bullet.Draw(spriteBatch);
            }
        }

        //Kiểm tra đạn đã bay ra khỏi tower chưa
        public bool CheckBulletOutOfTower(Bullet bullet)
        {
            if (Vector2.Distance(bullet.Center, position) < (float)Container.towerSize / 2)
                return false;
            return true;
        }

        //Kiểm tra xem đạn đã trúng target chưa
        public bool Collision(Bullet bullet)
        {
            if (target == null)
                return false;
            Vector2 distance = bullet.Center - target.Center;
            if (Math.Abs(distance.Y) <= Container.enemyTextureSize * Container.enemyTextureScale / 2)
                if (Math.Abs(distance.X) <= Container.enemyTextureSize * Container.enemyTextureScale / 2)
                    return true;
            return false;
        }

        //Kiểm tra xem đạn đã bay qua khỏi target chưa?
        public bool CheckBulletPassTarget(Bullet bullet)
        {
            if (target == null)
                return true;
            float tempX = (position.X - target.Center.X) * (bullet.Center.X - target.Center.X);
            float tempY = (position.Y - target.Center.Y) * (bullet.Center.Y - target.Center.Y);

            if (tempX >= 0 && tempY >= 0)
                return false;
            return true;
        }
    }
}
