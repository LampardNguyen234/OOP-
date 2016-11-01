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
        Vector2 target;
        Vector2 center;

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

        public Bullet(Vector2 position, int level, Texture2D bulletTexture, Vector2? target)
        {
            texture = bulletTexture;
            this.position = position;
            this.level = level;
            this.speed = 3;
            rotation = 0;
            age = 0;
            velocity = new Vector2(0, 0);
            if(target.HasValue)
                 this.target = target.Value;
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
        public void FollowTarget(Vector2 target)
        {
            Vector2 distance = target - center;
            distance.Normalize();
            rotation = -(float)Math.Atan2(distance.X, distance.Y) - (float)Math.PI/2;

            velocity = Vector2.Transform(new Vector2(-speed, 0)
                , Matrix.CreateRotationZ(rotation));//Need Fixing
        }
        
        //Hàm Update
        public void Update(GameTime gameTime)
        {
            position += velocity;
            center = position + new Vector2(texture.Width / 2, texture.Height / 2);
            origin = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead())
            {
                spriteBatch.Draw(texture, center, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
            }
            else
                return;
        }
    }
}
