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
    class buttonManager:Microsoft.Xna.Framework.Game
    {
          cButton playButton;
          cButton introButton;
          cButton creditButton;
          cButton backButton;
          towerButton tower1Button;
          towerButton tower2Button;
          towerButton tower3Button;
          towerButton tower4Button;
          towerButton tower5Button;
          towerButton tower6Button;

        Texture2D mainMenuTexture;
        Texture2D huongDanTexture;
        Texture2D tacGiaTexture;
        Texture2D playMenuTexture;
        Texture2D pauseStateTexture;

          cButton pauseButton;
          cButton resumeButton;
          cButton offMusicButton;
          cButton onMusicButton;

          towerButton specialSkill1Button;
          towerButton specialSkill2Button;
          towerButton specialSkill3Button;


    
          GraphicsDeviceManager graphics;
        public buttonManager(GraphicsDeviceManager Graphics)
        {
   
            graphics = Graphics;
        }
        public  void LoadContent(ContentManager Content)
        {
             //Tu update Button
            //<Start>
            // Load button
            
            playButton = new cButton(Content.Load<Texture2D>("buttonPlay"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            playButton.SetPosition(Container.btnPlayPosition);

            introButton = new cButton(Content.Load<Texture2D>("buttonHD"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            introButton.SetPosition(Container.btnIntroPosition);

            creditButton = new cButton(Content.Load<Texture2D>("buttonTG"), graphics.GraphicsDevice, Container.btnMenuGameSize);
            creditButton.SetPosition(Container.btnTacGiaPosition);

            backButton = new cButton(Content.Load<Texture2D>("buttonBack"), graphics.GraphicsDevice, Container.btnBackSize);
            backButton.SetPosition(Container.btnBackPosition);


            tower1Button = new towerButton(Content.Load<Texture2D>("tower__000"), Content.Load<Texture2D>("tower1detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            tower1Button.SetPosition(Container.btnTower1Position);
            tower1Button.setPrice(Container.tower1Price);

            tower2Button = new towerButton(Content.Load<Texture2D>("tower2"), Content.Load<Texture2D>("tower2detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            tower2Button.SetPosition(Container.btnTower2Position);
            tower2Button.setPrice(Container.tower2Price);

            tower3Button = new towerButton(Content.Load<Texture2D>("tower3"), Content.Load<Texture2D>("tower3detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            tower3Button.SetPosition(Container.btnTower3Position);
            tower3Button.setPrice(Container.tower3Price);

            tower4Button = new towerButton(Content.Load<Texture2D>("tower4"), Content.Load<Texture2D>("tower4detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            tower4Button.SetPosition(Container.btnTower4Position);
            tower4Button.setPrice(Container.tower4Price);

            tower5Button = new towerButton(Content.Load<Texture2D>("tower5"), Content.Load<Texture2D>("tower5detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            tower5Button.SetPosition(Container.btnTower5Position);
            tower5Button.setPrice(Container.tower5Price);

            tower6Button = new towerButton(Content.Load<Texture2D>("tower6"), Content.Load<Texture2D>("tower6detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            tower6Button.SetPosition(Container.btnTower6Position);
            tower6Button.setPrice(Container.tower6Price);

            pauseButton = new cButton(Content.Load<Texture2D>("pause"), graphics.GraphicsDevice, Container.btnPauseSize);
            pauseButton.SetPosition(Container.btnPausePosition);

            resumeButton = new cButton(Content.Load<Texture2D>("resume"), graphics.GraphicsDevice, Container.btnPauseSize);
            resumeButton.SetPosition(Container.btnResumePosition);

            offMusicButton = new cButton(Content.Load<Texture2D>("turnOffMusic"), graphics.GraphicsDevice, Container.btnPauseSize);
            offMusicButton.SetPosition(Container.btnOffMusicPosition);

            onMusicButton = new cButton(Content.Load<Texture2D>("turnOnMusic"), graphics.GraphicsDevice, Container.btnPauseSize);
            onMusicButton.SetPosition(Container.btnOnMusicPosition);

            specialSkill1Button = new towerButton(Content.Load<Texture2D>("specialSkill1"), Content.Load<Texture2D>("special1detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            specialSkill1Button.SetPosition(Container.btnSpecialSkill1Button);

            specialSkill2Button = new towerButton(Content.Load<Texture2D>("specialSkill2"), Content.Load<Texture2D>("special2detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            specialSkill2Button.SetPosition(Container.btnSpecialSkill2Button);

            specialSkill3Button = new towerButton(Content.Load<Texture2D>("specialSkill3"), Content.Load<Texture2D>("special3detail"), graphics.GraphicsDevice, Container.btnTowerSize);
            specialSkill3Button.SetPosition(Container.btnSpecialSkill3Button);

            mainMenuTexture = Content.Load<Texture2D>("MainMenu");

            huongDanTexture = Content.Load<Texture2D>("HuongDan");

            tacGiaTexture = Content.Load<Texture2D>("TacGia");

            pauseStateTexture = Content.Load<Texture2D>("pauseState");

            playMenuTexture = Content.Load<Texture2D>("PlayMenu");

        }
        public void Update(Container.GameState GameState,MouseState mouse,int goldHave,GameTime gameTime)
        {
            
            switch (GameState)
            {
                case Container.GameState.MainMenu:
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
                    playButton.Update(mouse);
                    creditButton.Update(mouse);
                    introButton.Update(mouse);
                    backButton.Update(mouse);
                    break;
                case Container.GameState.Intro:
                    if (backButton.isClicked == true)
                        GameState = Container.GameState.MainMenu;
                    backButton.Update(mouse);
                    break;
                case Container.GameState.Tacgia:
                    if (backButton.isClicked == true)
                        GameState = Container.GameState.MainMenu;
                    backButton.Update(mouse);
                    break;
                case Container.GameState.Playing:
                    if (pauseButton.isClicked == true)
                        GameState = Container.GameState.Pausing;
                    pauseButton.Update(mouse);
                    if (resumeButton.isClicked == true)
                        GameState = Container.GameState.Playing;
                    resumeButton.Update(mouse);
                    tower1Button.Update(mouse, goldHave);
                    tower2Button.Update(mouse, goldHave);
                    tower3Button.Update(mouse, goldHave);
                    tower4Button.Update(mouse, goldHave);
                    tower5Button.Update(mouse, goldHave);
                    tower6Button.Update(mouse, goldHave);
                    specialSkill1Button.Update(mouse, goldHave);
                    specialSkill2Button.Update(mouse, goldHave);
                    specialSkill3Button.Update(mouse, goldHave);
                    break;
                    
            }
            base.Update(gameTime);
        }
        /*
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
         -1 - nothing
         */
        public int getCliked()
        {
            if (tower1Button.isClicked == true)
                return 0;
            if (tower2Button.isClicked == true)
                return 1;
            if (tower3Button.isClicked == true)
                return 2;
            if (tower4Button.isClicked == true)
                return 3;
            if (tower5Button.isClicked == true)
                return 4;
            if (tower6Button.isClicked == true)
                return 5;
            if (specialSkill1Button.isClicked == true)
                return 6;
            if (specialSkill2Button.isClicked == true)
                return 7;
            if (specialSkill3Button.isClicked == true)
                return 8;
            if (pauseButton.isClicked == true)
                return 9;
            if (resumeButton.isClicked == true)
                return 10;
            if (onMusicButton.isClicked == true)
                return 11;
            if (offMusicButton.isClicked == true)
                return 12;
            return -1;
        }

        public void Draw(SpriteBatch spriteBatch,Container.GameState GameState)
        {

            switch (GameState)
            {
                case Container.GameState.MainMenu:
                spriteBatch.Draw(mainMenuTexture, new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);
                playButton.Draw(spriteBatch);
                introButton.Draw(spriteBatch);
                creditButton.Draw(spriteBatch);
                break;
                case Container.GameState.Intro:
                spriteBatch.Draw(mainMenuTexture, new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);
                spriteBatch.Draw(huongDanTexture, new Rectangle(200, 50, 800, 600), Color.White);
                backButton.Draw(spriteBatch);
                break;
                case Container.GameState.Tacgia:
                spriteBatch.Draw(mainMenuTexture, new Rectangle(0, 0, Container.scrWidth, Container.scrHeight), Color.White);
                spriteBatch.Draw(tacGiaTexture, new Rectangle(200, 50, 800, 600), Color.White);
                backButton.Draw(spriteBatch);

                break;
                case Container.GameState.Pausing:
                spriteBatch.Draw(playMenuTexture, Container.playMenuPosition, Color.White);
                spriteBatch.Draw(pauseStateTexture, new Rectangle(100, 50, 800, 600), Color.White);
                tower1Button.Draw(spriteBatch);
                tower2Button.Draw(spriteBatch);
                tower3Button.Draw(spriteBatch);
                tower4Button.Draw(spriteBatch);
                tower5Button.Draw(spriteBatch);
                tower6Button.Draw(spriteBatch);

                onMusicButton.Draw(spriteBatch);
                resumeButton.Draw(spriteBatch);
                specialSkill1Button.Draw(spriteBatch);
                specialSkill2Button.Draw(spriteBatch);
                specialSkill3Button.Draw(spriteBatch);
                break;
                case Container.GameState.Playing:

                    spriteBatch.Draw(playMenuTexture, Container.playMenuPosition, Color.White);
                   
                    //animation.Draw(spriteBatch);
                    // Cac ban ve trong playing thi Update o day
                    //<Start>
                    
                    tower1Button.Draw(spriteBatch);
                    tower2Button.Draw(spriteBatch);
                    tower3Button.Draw(spriteBatch);
                    tower4Button.Draw(spriteBatch);
                    tower5Button.Draw(spriteBatch);
                    tower6Button.Draw(spriteBatch);
                    onMusicButton.Draw(spriteBatch);
                    pauseButton.Draw(spriteBatch);
                    specialSkill1Button.Draw(spriteBatch);
                    specialSkill2Button.Draw(spriteBatch);
                    specialSkill3Button.Draw(spriteBatch);

                    //<End>
                    break;
            }
            
            
        }
    }
}
