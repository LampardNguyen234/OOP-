using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace TowerDefenseOOP
{
    public class SoundManager
    {
        public SoundEffect  explodeSound;
        public List<SoundEffect> towerShoot = new List<SoundEffect>();
        public Song bgMusic, mainMenu;
        public SoundManager()
        {
            explodeSound = null;
            bgMusic = null;
            mainMenu = null;
        }
        //Load Content
        public void LoadContent(ContentManager Content)
        {
            for(int i=0;i<5;i++)
            {
                string s = "Sound\\bullet_00" + i.ToString();
                towerShoot.Add(Content.Load<SoundEffect>(s));
            }
            explodeSound = Content.Load<SoundEffect>("Sound\\explode");
            bgMusic = Content.Load<Song>("Sound\\theme");
            mainMenu = Content.Load<Song>("Sound\\mainMenu1");
        }
    }
}
