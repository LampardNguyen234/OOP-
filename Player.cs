﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseOOP
{
    class Player
    {
        #region Khai báo

        int[,] map = new int[Container.MapHeight,Container.MapWidth];    //Bản đồ trò chơi
        List<Vector2> roadCenters = new List<Vector2>();//Danh sách các vector2 chứa các center của các ô có đường đi
        List<Vector2> rockCenters = new List<Vector2>();//Danh sách các vector2 chứa các center của các ô có đá/rừng
        int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private Vector2 position;
        private Vector2 origin;

        private Texture2D mouseTexture;     //Texture dùng để check vị trí xây dựng tower có hợp lệ không

        private MouseState mouseState;
        private MouseState oldState;

        #endregion

        //Constructor
        public Player(Map map, int level)
        {
            this.map = map.MapList[level - 1];              //Tùy vào level sẽ có map khác nhau
            this.level = level;                             //Level của màn chơi
            origin = new Vector2(Container.towerSize / 2, Container.towerSize / 2);
        }

        //Hàm lấy vào tất cả các ô nằm trên đường đi add vào roadCenters và các ô chứa đá add vào rockCenters
        public void addPositions()
        {
            Vector2 temp = new Vector2(Container.towerSize / 2, Container.towerSize / 2);
            for(int y=0;y<Container.MapHeight;y++)
            {
                for(int x=0;x<Container.MapWidth;x++)
                {
                    if(map[y,x]==1)//Chỉ số các ô nằm trên đường đi bằng 1
                    {
                        Vector2 temp2 = new Vector2(x, y) * Container.towerSize + temp;//Lấy vị trí tâm của ô 
                        roadCenters.Add(temp2);  //Thêm vào list
                    }
                    if (map[y, x] == 2)//Chỉ số các ô chứa đá bằng 2
                    {
                        Vector2 temp2 = new Vector2(x, y) * Container.towerSize + temp;//Lấy vị trí tâm của ô 
                        rockCenters.Add(temp2);  //Thêm vào list
                    }
                }
            }
        }

        //Hàm kiểm tra xem có thể xây Tower ở vị trí nhập vào không

        public bool isTowerAvailable(Vector2 position)
        {
            int width = Container.MapWidth;
            int height = Container.MapHeight;
            foreach (Vector2 roadCenter in roadCenters)
            {
                if (Vector2.Distance(position, roadCenter) < (float)Container.tileSize)
                    return false;
            }
            foreach (Vector2 rockCenter in rockCenters)
            {
                if (Vector2.Distance(position, rockCenter) < (float)Container.tileSize * 2)
                    return false;
            }
            return true;
        }


        //Hàm LoadContent
        public void LoadContent(ContentManager content)
        {
            mouseTexture = content.Load<Texture2D>("hover");        //Load mouseTexture 
            addPositions();                                         
        }

        //Hàm update
        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            position = new Vector2(mouseState.X, mouseState.Y);

            oldState = mouseState;
        }

        //Hàm Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            if (isTowerAvailable(position))
                color = Color.Red;
            else
                color = Color.Blue;
            spriteBatch.Draw(mouseTexture, position, null, color*0.2f, 0f, origin, (float)Container.tileSize / (float)mouseTexture.Width, SpriteEffects.None, 0f);
        }
    }
}