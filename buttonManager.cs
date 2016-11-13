using System;
using System.Collections.Generic;
using System.Linq;
using LineBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace TowerDefenseOOP
{
    /// <summary>
    /// Class đặc xử lý các button có trong game
    /// </summary>
    class buttonManager:Microsoft.Xna.Framework.Game
    {
          cButton playButton;
          cButton introButton;
          cButton creditButton;
          cButton backButton;

            //Các buttonTower có trong menu
          List<towerButton> towerButtonList;

        Texture2D mainMenuTexture;
        Texture2D huongDanTexture;
        Texture2D tacGiaTexture;
        Texture2D pauseStateTexture;

        //Các texture trong menu field
        Texture2D menuField;
        Texture2D infoBlock;
        Texture2D towerBlock;
        Texture2D skillBlock;

        public cButton pauseButton;              //Nút dừng
        public cButton resumeButton;             //Nút tiếp tục
        public cButton offMusicButton;           //Nút tắt âm thanh
        public cButton onMusicButton;            //Nút bật âm thanh
        public cButton nextWave;                //Nút đợt tấn công tiếp theo
        public cButton replayButton;               //Nút replay

        public cButton specialSkill1Button;
        public cButton specialSkill2Button;
        public cButton specialSkill3Button;

          public Container.GameState GameState;

          int retButton;

          public int RetButton
          {
              get { return this.retButton; }
              set { this.retButton = value; }
          }
          bool isSoundOn;
          bool isPauseSet;

        GraphicsDeviceManager graphics;
        public buttonManager(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            isSoundOn = true;
            isPauseSet = false;
        }
        public  void LoadContent(ContentManager Content)
        {
             //Tu update Button
            //<Start>
            // Load button
            towerButtonList = new List<towerButton>(6);
            playButton = new cButton(Content.Load<Texture2D>("buttonPlay"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            playButton.SetPosition(Container.btnPlayPosition);

            introButton = new cButton(Content.Load<Texture2D>("buttonHD"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            introButton.SetPosition(Container.btnIntroPosition);

            creditButton = new cButton(Content.Load<Texture2D>("buttonTG"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            creditButton.SetPosition(Container.btnTacGiaPosition);

            backButton = new cButton(Content.Load<Texture2D>("buttonBack"), graphics.GraphicsDevice, Container.cButtonSize);
            backButton.SetPosition(Container.btnBackPosition);


            pauseButton = new cButton(Content.Load<Texture2D>("pause"), graphics.GraphicsDevice, Container.cButtonSize);
            pauseButton.SetPosition(Container.btnPausePosition);

            resumeButton = new cButton(Content.Load<Texture2D>("resume"), graphics.GraphicsDevice, Container.cButtonSize);
            resumeButton.SetPosition(Container.btnResumePosition);

            offMusicButton = new cButton(Content.Load<Texture2D>("turnOffMusic"), graphics.GraphicsDevice, Container.cButtonSize);
            offMusicButton.SetPosition(Container.btnOffMusicPosition);

            onMusicButton = new cButton(Content.Load<Texture2D>("turnOnMusic"), graphics.GraphicsDevice, Container.cButtonSize);
            onMusicButton.SetPosition(Container.btnOnMusicPosition);

            replayButton = new cButton(Content.Load<Texture2D>("replay"), graphics.GraphicsDevice, Container.cButtonSize);
            replayButton.SetPosition(Container.btnReplayPosition);

            nextWave = new cButton(Content.Load<Texture2D>("nextWave"), graphics.GraphicsDevice, Container.cButtonSize);
            nextWave.SetPosition(Container.btnNextWavePosition);

            specialSkill1Button = new cButton(Content.Load<Texture2D>("specialSkill1"), graphics.GraphicsDevice, Container.btnTowerSize);
            specialSkill1Button.SetPosition(Container.btnSpecialSkill1Button);

            specialSkill2Button = new cButton(Content.Load<Texture2D>("skill2"), graphics.GraphicsDevice, Container.btnTowerSize);
            specialSkill2Button.SetPosition(Container.btnSpecialSkill2Button);

            specialSkill3Button = new cButton(Content.Load<Texture2D>("skill3"), graphics.GraphicsDevice, Container.btnTowerSize);
            specialSkill3Button.SetPosition(Container.btnSpecialSkill3Button);

            mainMenuTexture = Content.Load<Texture2D>("MainMenu");

            huongDanTexture = Content.Load<Texture2D>("HuongDan");

            tacGiaTexture = Content.Load<Texture2D>("TacGia");

            pauseStateTexture = Content.Load<Texture2D>("pauseState");

            //Load các texture trong menu filed
            menuField = Content.Load<Texture2D>("menuField");
            infoBlock = Content.Load<Texture2D>("infoBlock");
            towerBlock = Content.Load<Texture2D>("towerBlock");
            skillBlock = Content.Load<Texture2D>("skillBlock");
            //Load các tower texture
            for(int i=0;i<6;i++)
            {
                string s="tower" +(i+1).ToString()+"detail";
                towerButton tB= new towerButton(Content.Load<Texture2D>(i.ToString()), Content.Load<Texture2D>(s), graphics.GraphicsDevice, Container.btnTowerSize);
                tB.SetPosition(Container.btnTowerPosList[i]);
                tB.setPrice(Container.priceList[i]);
                towerButtonList.Add(tB);
            }

        }
        /* Giá trị của retButton
         0-tower1
         ...
         5-tower6
         6-special1
         ...
         8-special3
         9-pause
         10-resume
         11-onmusic
         12-offmusic
         13-nextwave
         14-replay
         -1-nothing
         */
        public void Update(Container.GameState CurrentState,MouseState mouse,int goldHave,GameTime gameTime)
        {
            GameState = CurrentState;
            KeyboardState state = new KeyboardState();
             state = Keyboard.GetState();
            if (Container.HP < 1)
                GameState = Container.GameState.GameOver;
            if (Game1.wave > Container.waveQuantity && Game1.enemyPerWave<=0)
                GameState = Container.GameState.Win;
            switch (GameState)
            {
                case Container.GameState.GameOver:          //Trường hợp thua
                    Container.HP = 0;
                    backButton.Update(mouse);
                    if (backButton.isClicked == true)       //Chơi lại
                    {
                        GameState = Container.GameState.MainMenu;
                        Container.HP = 100;
                        Game1.wave = 1;
                        goldHave = 400;
                    }
                    break;

                case Container.GameState.Win:           //Trường hợp thắng
                    backButton.Update(mouse);
                    if (backButton.isClicked == true)       //Chơi lại
                    {
                        GameState = Container.GameState.MainMenu;
                        Container.HP = 100;
                        Game1.wave = 1;
                        goldHave = 400;
                    }
                    break;

                case Container.GameState.MainMenu:          //Main menu
                    playButton.Update(mouse);
                    creditButton.Update(mouse);
                    introButton.Update(mouse);
                    backButton.Update(mouse);
                    if (playButton.isClicked == true)
                    {
                        GameState = Container.GameState.Playing;
                    }
                    else if (creditButton.isClicked == true)
                    {
                        GameState = Container.GameState.Tacgia;

                    }
                    else if (introButton.isClicked == true)
                    {
                        GameState = Container.GameState.Intro;
                    }
                    break;

                case Container.GameState.Intro:         //Giới thiệu
                    if (backButton.isClicked == true || state.IsKeyDown(Keys.Escape))
                        GameState = Container.GameState.MainMenu;
                    backButton.Update(mouse);
                    break;

                case Container.GameState.Tacgia:        //Tác giả
                    if (backButton.isClicked == true || state.IsKeyDown(Keys.Escape))
                        GameState = Container.GameState.MainMenu;
                    backButton.Update(mouse);
                    break;

                case Container.GameState.Playing:       //Đang chơi
                    retButton = -1;
                    if (pauseButton.isClicked == true || state.IsKeyDown(Keys.Escape))  //Nếu nhấn nut pause hoặc escape
                    {
                        if (isPauseSet != true)
                            retButton = 9;
                        isPauseSet = !isPauseSet;
                        GameState = Container.GameState.Pausing;
                        pauseButton.Update(mouse);
                    }
                    else
                    {
                        resumeButton.Update(mouse);

                        foreach (towerButton tB in towerButtonList)
                            tB.Update(mouse, goldHave);
                        onMusicButton.Update(mouse);
                        offMusicButton.Update(mouse);
                        nextWave.Update(mouse);
                        pauseButton.Update(mouse);
                        replayButton.Update(mouse);
                        specialSkill1Button.Update(mouse);
                        specialSkill2Button.Update(mouse);
                        specialSkill3Button.Update(mouse);

                        if (onMusicButton.isClicked == true)
                        {
                            if (isSoundOn)
                                retButton = 11;
                            else
                                retButton = 12;
                            isSoundOn = !isSoundOn;
                        }
                        for (int i = 0; i < towerButtonList.Count;i++ )
                        {
                            if (towerButtonList[i].isClicked == true)
                                retButton = i;
                        }
                        if (specialSkill1Button.isClicked == true)
                            RetButton = 6;
                        if (specialSkill2Button.isClicked == true)
                            RetButton = 7;
                        if (specialSkill3Button.isClicked == true)
                            RetButton = 8;
                        if (nextWave.isClicked==true)
                            RetButton = 13;
                        if (replayButton.isClicked==true)
                            RetButton = 14;
                    }
                    break;

                case Container.GameState.Pausing:               //Trường hợp pause
                    if (pauseButton.isClicked == true || state.IsKeyDown(Keys.Escape))
                    {
                        if(isPauseSet)
                            RetButton = 10;
                        isPauseSet = !isPauseSet;
                        GameState = Container.GameState.Playing;
                    }
                    pauseButton.Update(mouse);
                    replayButton.Update(mouse);
                    break;
            }
             base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch,Container.GameState GameState)
        {

            switch (GameState)
            {
                #region GameState = MainMenu
                case Container.GameState.MainMenu:
                spriteBatch.Draw(mainMenuTexture, new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);
                playButton.Draw(spriteBatch);
                introButton.Draw(spriteBatch);
                creditButton.Draw(spriteBatch);
                break;
                #endregion

                #region    GameState = Intro
                case Container.GameState.Intro:
                spriteBatch.Draw(mainMenuTexture, new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);
                spriteBatch.Draw(huongDanTexture, new Rectangle(200, 50, 800, 600), Color.White);
                backButton.Draw(spriteBatch);
                break;
                #endregion

                #region GameState = TacGia
                case Container.GameState.Tacgia:
                spriteBatch.Draw(mainMenuTexture, new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);
                spriteBatch.Draw(tacGiaTexture, new Rectangle(0, 0, tacGiaTexture.Width, tacGiaTexture.Height), Color.White);
                backButton.Draw(spriteBatch);
                break;
                #endregion

                #region GameState = Pausing || Playing || Win  || GameOver
                case Container.GameState.Pausing:
                case Container.GameState.Playing:
                case Container.GameState.Win:
                case Container.GameState.GameOver:
                    //Vẽ menu field
                    spriteBatch.Draw(menuField, Container.playMenuPosition, Color.White*0.1f);
                    spriteBatch.Draw(infoBlock, Container.infoBlock, Color.White*0.3f);
                    spriteBatch.Draw(towerBlock, Container.towerBlock, Color.White * 0.3f);
                    spriteBatch.Draw(skillBlock, Container.skillBlock, Color.White * 0.3f);
                    // Cac ban ve trong playing thi Update o day
                    //<Start>
                    //Vẽ các towerButton
                    foreach (towerButton item in towerButtonList)
                    {
                        item.Draw(spriteBatch);
                    }
                    if (!isSoundOn)
                        onMusicButton.Draw(spriteBatch);
                    else
                        offMusicButton.Draw(spriteBatch);
                    if (!isPauseSet)
                        pauseButton.Draw(spriteBatch);
                    else
                        resumeButton.Draw(spriteBatch);
                    replayButton.Draw(spriteBatch);
                    nextWave.Draw(spriteBatch);
                    specialSkill1Button.Draw(spriteBatch);
                    specialSkill2Button.Draw(spriteBatch);
                    specialSkill3Button.Draw(spriteBatch);
                    //<End>
                    break;
                #endregion
            }
        }
    }
}
