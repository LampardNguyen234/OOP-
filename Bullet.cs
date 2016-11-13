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
    class Bullet
    {
        #region Khai báo
        Vector2 position;
        Vector2 velocity;
        Vector2 origin;
        Vector2 center;
        public Enemy target;

        private Rectangle boundingBox;

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
        }

        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        Texture2D texture;

        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private int level;
        private int speed;

        private float rotation;

        #endregion

        public Bullet(Vector2 position, int level, Texture2D bulletTexture, Enemy target)
        {
            texture = bulletTexture;
            this.position = position;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.level = level;
            this.speed = 5;
            rotation = 0;
            age = 0;
            velocity = new Vector2(0, 0);
            this.target = target;
        }

        public bool IsDead()
        {
            return age > 100;
        }

        public void Kill()
        {
            age = 200;
        }

        //Tính góc quay
        public void SetRotation(float value)
        {
            rotation = value;

            velocity = Vector2.Transform(new Vector2(-speed, 0)
                , Matrix.CreateRotationZ(rotation));//Need Fixing
        }

        //Đuổi theo target
        public void FollowTarget()
        {
            if (target == null)
                return;
            Vector2 distance = target.Center - center;
            distance.Normalize();
            rotation = -(float)Math.Atan2(distance.X, distance.Y) - (float)Math.PI/2;
            velocity = Vector2.Transform(new Vector2(-speed, 0)
                , Matrix.CreateRotationZ(rotation));//Need Fixing
        }

        public bool Collision()     //Kiểm tra sự đụng độ giữa bullet và target
        {
            if (target == null)
                return false;
            if (boundingBox.Intersects(target.BoundingBox))
                return true;
            return false;
        }

        //Hàm Update
        public void Update(GameTime gameTime)
        {
            position += velocity;
            center = position + new Vector2(texture.Width / 2, texture.Height / 2);
            boundingBox = new Rectangle((int)center.X, (int)center.Y, texture.Width, texture.Height);
            origin = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead())
            {
                spriteBatch.Draw(texture, boundingBox, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
            }
            else
                return;
        }
    }
}
