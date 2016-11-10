using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDefenseOOP
{
    class MouseManager
    {
        #region Khai báo
        public static Vector2 position;
        MouseState mouseState;
        Texture2D mouseTexture;
        List<Texture2D> towerTextureList = new List<Texture2D>();
        Texture2D radiusTexture;
        MouseState oldState;
        List<Tower> towerList;
        List<Enemy> enemyList;
        private Vector2 origin;
        private bool isBuildingTower;
        int level;
        float scale;
        public static int buildTower;
        private int tempBuildTower;
        bool isTowerAvailable;
        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        #endregion

        List<Vector2> roadCenters = new List<Vector2>();
        List<Vector2> rockCenters = new List<Vector2>();
        int[,] map = new int[Container.MapHeight, Container.MapWidth];
        private int Level_2;


        //Constructor
        public MouseManager(int[,] map, int level)
        {
            this.map = map;
            this.level = level;
            this.scale = Container.enemyTextureScale;
            origin = new Vector2(Container.towerSize / 2, Container.towerSize / 2);
            buildTower = -1;
            tempBuildTower = -1;
            isBuildingTower = false;
            Mouse.SetPosition(0, 0);
            isTowerAvailable = false;
        }


        //Đưa vị trí cái waypoint và rock vào list
        public void addToMap()
        {
            Vector2 temp = new Vector2(Container.roadSize / 2, Container.roadSize / 2);
            for (int y = 0; y < Container.MapHeight; y++)
            {
                for (int x = 0; x < Container.MapWidth; x++)
                {
                    if (map[y, x] == 1)//Chỉ số các ô nằm trên đường đi bằng 1
                    {
                        Vector2 temp2 = new Vector2(x, y) * Container.roadSize + temp;//Lấy vị trí tâm của ô 
                        roadCenters.Add(temp2);  //Thêm vào list
                    }
                    if (map[y, x] == 2)//Chỉ số các ô chứa đá bằng 2
                    {
                        Vector2 temp2 = new Vector2(x, y) * Container.roadSize + temp;//Lấy vị trí tâm của ô 
                        rockCenters.Add(temp2);  //Thêm vào list
                    }
                }
            }
        }


        //Kiểm tra vị trí trỏ chuột có thể xây tower không
        public bool checkTowerAvailable(Vector2 position)
        {
            int width = Container.MapWidth;
            int height = Container.MapHeight;
            if (position.X > 875) return false;
            foreach (Vector2 roadCenter in roadCenters)
            {
                if (Vector2.Distance(position, roadCenter) < (float)Container.tileSize * (scale + 0.05f))
                    return false;
            }
            foreach (Vector2 rockCenter in rockCenters)
            {
                if (Vector2.Distance(position, rockCenter) < (float)Container.towerSize * 2)
                    return false;
            }
            return true;
        }

        //Load content
        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < 6; i++)
            {
                string s = "tower_00" + i.ToString() ;
                Texture2D sTemp = content.Load<Texture2D>(s);
                towerTextureList.Add(sTemp);
            }
            mouseTexture = content.Load<Texture2D>("base");
            radiusTexture = content.Load<Texture2D>("radius");
            addToMap();
        }

        //Kiểm tra xem vị trí có xây được tower không
        public bool checkTowerAvailable(Vector2 position, List<ackackTower> ackackTowerList, List<DualMachineGunTower> dualMachineTowerList,
        List<MachineGunTower> machineGunTowerList, List<LazerTower> lazeTowerList, List<RocketTower> rocketTowerList, List<EnhancingTower> enhancingTowerList)
        {
            int width = Container.MapWidth;
            int height = Container.MapHeight;
            if (position.X > 870) return false;
            foreach (Vector2 roadCenter in roadCenters)
            {
                if (Vector2.Distance(position, roadCenter) < (float)Container.tileSize * Container.enemyTextureScale*0.75)
                    return false;
            }
            foreach (Vector2 rockCenter in rockCenters)
            {
                if (Vector2.Distance(position, rockCenter) < (float)Container.towerSize * 2)
                    return false;
            }
            foreach (ackackTower a in ackackTowerList)
            {
                if (Vector2.Distance(position, a.Position) < Container.towerSize)
                    a.Hover = true;
                else
                    a.Hover = false;
                if (Vector2.Distance(position, a.Position) < (float)Container.towerSize * Container.enemyTextureScale)
                    return false;
            }
            foreach (DualMachineGunTower d in dualMachineTowerList)
            {
                if (Vector2.Distance(position, d.Position) < Container.towerSize)
                    d.Hover = true;
                else
                    d.Hover = false;
                if (Vector2.Distance(position, d.Position) < (float)Container.towerSize * Container.enemyTextureScale)
                    return false;
            }
            foreach (MachineGunTower m in machineGunTowerList)
            {
                if (Vector2.Distance(position, m.Position) < Container.towerSize)
                    m.Hover = true;
                else
                    m.Hover = false;
                if (Vector2.Distance(position, m.Position) < (float)Container.towerSize * Container.enemyTextureScale)
                    return false;
            }
            foreach (LazerTower l in lazeTowerList)
            {
                if (Vector2.Distance(position, l.Position) < Container.towerSize)
                    l.Hover = true;
                else
                    l.Hover = false;
                if (Vector2.Distance(position, l.Position) < Container.towerSize * Container.enemyTextureScale)
                    return false;
            }
            foreach (RocketTower r in rocketTowerList)
            {
                if (Vector2.Distance(position, r.Position) < Container.towerSize)
                    r.Hover = true;
                else
                    r.Hover = false;
                if (Vector2.Distance(position, r.Position) < (float)Container.towerSize * Container.enemyTextureScale)
                    return false;
            }
            foreach (EnhancingTower e in enhancingTowerList)
            {
                if (Vector2.Distance(position, e.Position) < Container.towerSize)
                    e.Hover = true;
                else
                    e.Hover = false;
                if (Vector2.Distance(position, e.Position) < (float)Container.towerSize * Container.enemyTextureScale)
                    return false;
            }
            return true;
        }

        //Update
        public void Update(GameTime gameTime, int retButton, List<ackackTower> ackackTowerList, List<DualMachineGunTower> dualMachineTowerList,
         List<MachineGunTower> machineGunTowerList, List<LazerTower> lazeTowerList, List<RocketTower> rocketTowerList, List<EnhancingTower> enhancingTowerList)
        {
            mouseState = Mouse.GetState();
            position = new Vector2(mouseState.X, mouseState.Y);
            buildTower = -1;

            if (checkTowerAvailable(position, ackackTowerList, dualMachineTowerList, machineGunTowerList, lazeTowerList, rocketTowerList, enhancingTowerList))
                isTowerAvailable = true;
            else
                isTowerAvailable = false;
            //Hàm lấy giá trị và trạng thái chọn tháp để mang vào map xây
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed && position.X > 900 && retButton!=-1)
            {
                tempBuildTower = retButton;//Giá trị trả về từ hàm isClick() trong Menu
                switch(retButton)
                {
                    case 0:
                        if (CheckBuildingAvailable(Container.Tower0Price))
                            isBuildingTower = true;
                        break;
                    case 1:
                        if (CheckBuildingAvailable(Container.Tower1Price))
                            isBuildingTower = true;
                        break;
                    case 2:
                        if (CheckBuildingAvailable(Container.Tower2Price))
                            isBuildingTower = true;
                        break;
                    case 3:
                        if (CheckBuildingAvailable(Container.Tower3Price))
                            isBuildingTower = true;
                        break;
                    case 4:
                        if (CheckBuildingAvailable(Container.Tower4Price))
                            isBuildingTower = true;
                        break;
                    case 5:
                        if (CheckBuildingAvailable(Container.Tower5Price))
                            isBuildingTower = true;
                        break;
                    default:
                        isBuildingTower = true;
                        break;
                }
            }

            //RightClick hoặc nhấn ESC để hủy việc đang chọn Tower
            KeyboardState state = Keyboard.GetState();
            if (mouseState.RightButton == ButtonState.Released && oldState.RightButton == ButtonState.Pressed && isBuildingTower || state.IsKeyDown(Keys.Escape))
            {
                isBuildingTower = false;
                tempBuildTower = -1;
            }


            //Check vị trí của Tower
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed && isBuildingTower)
            {
                if (isTowerAvailable)
                {
                    buildTower = tempBuildTower;
                    tempBuildTower = -1;
                    isBuildingTower = false;
                }
            }
            oldState = mouseState;
        }

        //Kiểm tra xem còn đủ tiền đề mua không
        public bool CheckBuildingAvailable(int price)
        {
            if (price > Game1.goldHave)
                return false;
            return true;
        }
        //Hàm vẽ
        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            if (isTowerAvailable)
                color = Color.White;
            else
                color = Color.Red;
            if (isBuildingTower)
            {
                spriteBatch.Draw(mouseTexture, position, null, color,0f, origin, scale, SpriteEffects.None, 0f);
                if (tempBuildTower != -1 && tempBuildTower<6)
                    spriteBatch.Draw(towerTextureList[tempBuildTower], position, null, color, 0f, origin, scale, SpriteEffects.None, 0f);
                float radiusScale;
                //Vẽ đúng bán kính của từng Tower
                switch (tempBuildTower)
                {
                    case 0:
                        radiusScale = 2 * (float)Container.Tower0Radius / (float)radiusTexture.Width;
                        break;
                    case 1:
                        radiusScale = 2 * (float)Container.Tower1Radius / (float)radiusTexture.Width;
                        break;
                    case 2:
                        radiusScale = 2 * (float)Container.Tower2Radius / (float)radiusTexture.Width;
                        break;
                    case 3:
                        radiusScale = 2 * (float)Container.Tower3Radius / (float)radiusTexture.Width;
                        break;
                    case 4:
                        radiusScale = 2 * (float)Container.Tower4Radius / (float)radiusTexture.Width;
                        break;
                    case 5:
                        radiusScale = 2 * (float)Container.Tower5Radius / (float)radiusTexture.Width;
                        break;
                    default:
                        radiusScale = (float)Container.towerSize / (float)radiusTexture.Width;
                        break;
                }
                spriteBatch.Draw(radiusTexture, position, null, Color.Green * 0.2f, 0f, new Vector2(224 / 2, 224 / 2), radiusScale, SpriteEffects.None, 0f);
            }
        }
    }
}