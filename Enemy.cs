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
    class Enemy
    {
        #region Khai báo
        private Vector2 position;       //Vị trí    
        private Vector2 center;         //Tâm
        private Vector2 origin;
        private Rectangle boundingBox;
        private Rectangle healthBarRect;//Thanh hiển thị máu

        private float rotation;         //góc quay, dùng khi vẽ
        private int attack;
        private int currentHP;          //Máu hiện tại
        private int startingHP;         //Tổng máu
        private int bountyGiven;        //Tiền thưởng
        private int frameX, frameY;     //Dùng để làm animation cho enemy
        private int level;              //Level của enemy, level càng cao, enemy càng mạnh
        private float speed;            //Tốc độ di chuyển của enemy
        private float textureInterval;  //Tốc độ thay đổi hình ảnh texture
        private float healthPercent;
        private float timer;            //Biến đếm thời gian thay đổi hình ảnh texture
        private bool isAlive;           //Biến kiểm tra enemy còn sống hay đã chết

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }


        private Texture2D texture;
        private Texture2D healthBarTexture;

        private Queue<Vector2> wayPoints = new Queue<Vector2>();    //Đường đi của enemy
        private Queue<Vector2> copywayPoints = new Queue<Vector2>();
        private Enemy enemy;
        private Texture2D healthBar;
        private int maplevel;

        #endregion 


        //Constructor
        public Enemy(Texture2D texture,Texture2D healthBarTexture,int level)
        {
            isAlive = true;
            this.healthBarTexture = healthBarTexture;
            this.texture = texture;
            this.level = level;
            rotation = 0;
            position = new Vector2(0, 0);
            center = new Vector2(0, 0);
            origin = new Vector2(Container.enemyTextureSize / 2);
            attack = 5 * level;
            startingHP = Container.enemyStartingHP;
            currentHP = startingHP;
            bountyGiven = 25 * level;
            frameX = 0;
            frameY = 0;
            speed = Container.basicEnemySpeed* level;
            textureInterval = 20f;
            timer = 0;
            healthPercent = (float)currentHP / (float)startingHP;
            healthBarRect = new Rectangle(((int)center.X + (int)position.X) / 2, (int)center.Y, (int)(Container.healthBarWidth * healthPercent), Container.healthBarHeight);
            boundingBox = new Rectangle(frameX * Container.enemyTextureSize, frameY * Container.enemyTextureSize, Container.enemyTextureSize, Container.enemyTextureSize);
        }


        //Tính khoảng cách từ enemies tới waypoint tiếp theo
        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, wayPoints.Peek()); }
        }

        //Set Waypoints (đường mà enemies sẽ di theo), tham số truyền vào là một waypoints
        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
            {
                this.wayPoints.Enqueue(waypoint);
                this.copywayPoints.Enqueue(waypoint);
            }

            if (waypoints.Peek().X == 0)
            {
                rotation = -Container.PI / 2;
                this.position = this.wayPoints.Dequeue();
                this.copywayPoints.Dequeue();
            }
            else
            {
                rotation = 0;
                this.position = this.wayPoints.Dequeue();
                this.copywayPoints.Dequeue();
            }

        }


        //Hàm di chuyển enemy
        public void move()
        {
            Vector2 velocity;
            if (wayPoints.Count > 0)
            {
                if (DistanceToDestination < speed)
                {
                    position = wayPoints.Peek();
                    wayPoints.Dequeue();
                    copywayPoints.Dequeue();
                }
                else
                {
                    Vector2 direction = wayPoints.Peek() - position;
                    direction.Normalize();

                    velocity = Vector2.Multiply(direction, speed);

                    position += velocity;
                    center = new Vector2(position.X + Container.tileSize / 2, position.Y + Container.tileSize / 2);
                    origin = new Vector2(Container.enemyTextureSize / 2, Container.enemyTextureSize / 2);
                    healthBarRect = new Rectangle(((int)center.X + (int)position.X) / 2, (int)center.Y, (int)(Container.healthBarWidth * healthPercent), Container.healthBarHeight);//Cập nhật vị trí thanh máu
                }
            }
            else
                isAlive = false;
        }

        //Trạng thái quay của Texture
        public int rotationStatus()
        {
            Stack<Vector2> S1 = new Stack<Vector2>() ;
            Stack<Vector2> S2 = new Stack<Vector2>();

            while(copywayPoints.Count>0)
            {
                S1.Push(copywayPoints.Dequeue());
            }
            while (S1.Count > 0)
                S2.Push(S1.Pop());

            int X = (int)position.X / Container.tileSize;
            int Y = (int)position.Y / Container.tileSize;
            if (S2.Count >= 2)
            {   //Lấy hai điểm tiếp theo của wayPoints
                Vector2 temp1 = S2.Pop();
                Vector2 temp2 = S2.Pop() ;
                copywayPoints.Enqueue(temp1);
                copywayPoints.Enqueue(temp2);
                while (S2.Count > 0)
                    copywayPoints.Enqueue(S2.Pop());
                int X1 = (int)temp1.X / Container.tileSize;
                int Y1 = (int)temp1.Y / Container.tileSize;
                int X2 = (int)temp2.X / Container.tileSize;
                int Y2 = (int)temp2.Y / Container.tileSize;
                //Đi từ trái sang phải
                if (Y1 == Y && X1 > X)
                {
                    if (Y2 == Y)    //Đi thẳng
                        return 0;
                    if (Y2 > Y)     //Rẽ xuống
                        return 1;
                    return -1;      //Rẽ lên
                }
                //Đi từ trên xuống
                if (X1 == X && Y1 > Y)
                {
                    if (X2 == X)
                        return 0;   //Đi thẳng
                    if (X2 > X)
                        return -1;   //Rẽ phải
                    return 1;      //Rẽ trái
                }
                int X3 = (int)(position.X + Container.tileSize - 1) / 60;
                //Đi từ phải sang trái
                if (Y1 == Y && X1 < X3)
                {
                    if (Y2 == Y)    //Đi thẳng
                        return 0;
                    if (Y2 > Y)     //Đi xuống
                        return -1;
                    return 1;       //Đi lên
                }
                int Y3 = (int)(position.Y - Container.tileSize - 1) / 60;
                //Đi từ dưới lên
                if (X1 == X && Y1 < Y3)
                {
                    if (X2 == X)
                        return 0;   //Đi thẳng
                    if (X2 > X)
                        return 1;   //Rẽ phải
                    return -1;      //Rẽ trái
                }
                return 0;
            }
            else
            {
                if(S2.Count==1)
                    copywayPoints.Enqueue(S2.Pop());
                return 0;
            }
        }


        //Tính góc quay của texture
        public void calculateRotationAngle()
        {
            int k = rotationStatus();
            float rotationAdd = 0;
            float a = (float)(speed / 1.3);

            if (k == 0)
            {
                rotationAdd = 0f;
            }
            if (k == 1)
            {
                rotationAdd = ((float)a / 90) * Container.PI;
            }
            if (k == -1)
            {
                rotationAdd = -((float)a / 90) * Container.PI;
            }
            rotation += rotationAdd;
        }


        //Kiểm tra xem map[Y,X] có nằm trong waypoints ko
        bool isContained(Queue<Vector2> waypoints, int Y, int X)
        {
            foreach (Vector2 waypoint in waypoints)
            {
                if ((int)(waypoint.X / Container.tileSize) == X && (int)(waypoint.Y / Container.tileSize) == Y)
                    return true;
            }
            return false;
        }

        //Hàm Update
        public void Update(GameTime gameTime)
        {
            //Tính toán việc thay đổi hình ảnh texture
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if(timer>textureInterval)
            {
                if (frameX == texture.Width / Container.enemyTextureSize - 1)
                {
                    frameY++;
                    frameX = 0;
                }
                else
                    frameX++;
                if(frameY==texture.Height/Container.enemyTextureSize -1)
                {
                    frameY = frameX = 0;
                }
                timer = 0;
            }
            boundingBox = new Rectangle(frameX * Container.enemyTextureSize, frameY * Container.enemyTextureSize, Container.enemyTextureSize, Container.enemyTextureSize);
            
            //Di chuyển enemy
            move();
            //Tính toán góc quay của texture enemy
            calculateRotationAngle();
        }

        //Hàm Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if(isAlive)
            {
                spriteBatch.Draw(texture, center, boundingBox, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
                //Vẽ thanh máu
                spriteBatch.Draw(healthBarTexture, healthBarRect, Color.White);
            }
        }
    }
}
