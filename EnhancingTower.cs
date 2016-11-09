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
    /// <summary>
    /// Tháp tăng cường sức mạnh
    /// </summary>
    class EnhancingTower:Tower
    {
        public EnhancingTower(Texture2D texture, int level, Vector2 position, Texture2D baseTexture, Texture2D bulletTexture, Texture2D explosionTexture) :
            base(level,position,baseTexture,texture,bulletTexture, explosionTexture)
        {
            radius = Container.radiusMax /2;
            attack = 0;
            price = 400;
            smallestRange=0;
            timer = 0f; 
            interval = 0f;
        }

        #region Nâng cấp các tower nằm trong bán kinh

        //Upgrade AckAck
        public void Enhancing(List<ackackTower> aaList)
        {
            foreach (ackackTower aa in aaList)
            {
                if (Vector2.Distance(aa.Position, position) <= radius)
                {
                    if (aa.IsUpgradedByTower == false)
                    {
                        aa.Upgrade();
                        aa.IsUpgradedByTower = true;
                    }
                }
            }
        }

        //Upgrade DualGun
        public void Enhancing(List<DualMachineGunTower> dmList)
        {
            foreach (DualMachineGunTower dm in dmList)
            {
                if (Vector2.Distance(dm.Position, position) <= radius)
                {
                    if (dm.IsUpgradedByTower == false)
                    {
                        dm.Upgrade();
                        dm.IsUpgradedByTower = true;
                    }
                }
            }
        }

        //Upgrade LazerTower
        public void Enhancing(List<LazerTower> lzList)
        {
            foreach (LazerTower lz in lzList)
            {
                if (Vector2.Distance(lz.Position, position) <= radius)
                {
                    if (lz.IsUpgradedByTower == false)
                    {
                        lz.Upgrade();
                        lz.IsUpgradedByTower = true;
                    }
                }
            }
        }

        //Upgrade RocketTower
        public void Enhancing(List<RocketTower> rkList)
        {
            foreach (RocketTower rk in rkList)
            {
                if (Vector2.Distance(rk.Position, position) <= radius)
                {
                    if (rk.IsUpgradedByTower == false)
                    {
                        rk.Upgrade();
                        rk.IsUpgradedByTower = true;
                    }
                }
            }
        }

        #endregion

    }
}
