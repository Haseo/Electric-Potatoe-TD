using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Devices.Sensors;
using Electric_Potatoe_TD.Mob;

namespace Electric_Potatoe_TD
{
    public partial class Game
    {

        public void turretFiller()
        {
            int capital = 6000;
            TurretList = new List<Node>();
            TurretList.Add(new Node(0, 1, 10, 10, this));
            TurretList.Add(new Strenght(8, 4, 10, 10, this));
            TurretList.Add(new Speed(4, 2, 10, 10, this));
            TurretList.Add(new Shooter(0, 0, 10, 10, this));
           /* TurretList[2].levelUpNode(ref capital);
            TurretList[2].levelUpNode(ref capital);
            TurretList[3].levelUpNode(ref capital);
            TurretList[3].levelUpTower(ref capital);
            TurretList[3].levelUpTower(ref capital);
            TurretList[3].levelUpTower(ref capital);*/
            TurretList[2].levelUpNode();
            TurretList[2].levelUpNode();
            TurretList[3].levelUpNode();
            TurretList[3].levelUpTower();
            TurretList[3].levelUpTower();
            TurretList[3].levelUpTower();
        }

        public void FakeModFiller()
        {
            MobList = new List<Mob.Mob>();
            MobList.Add(new Mob.Speed(WayPoints));
            MobList.Add(new Mob.Peon(WayPoints));
            MobList.Add(new Mob.Tank(WayPoints));
        }

        public void FakeBulletFiller()
        {
            BulletList.Add(new Bullet(MobList[0], (Tower)TurretList[1]));
        }

        public void mapFiller()
        {
            //MapLoader NewMap = new MapLoader();
            NewMap.Load(1);
            int[] size = NewMap.getSize();
            this.mapX = size[1];
            this.mapY = size[0];
            pos_map.X = 10;
            pos_map.Y = 10;
            if ((((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) / mapX) <=
                (((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10) / mapY))
            {
                size_case = (((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) / mapX);
            }
            else
            {
                size_case = (((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10) / mapY);
            }
            if ((((_origin.graphics.PreferredBackBufferWidth * 8 / 10) - 10) / 7) <=
                (((_origin.graphics.PreferredBackBufferHeight * 8 / 10) - 10) / 5))
            {
                size_caseZoom = (((_origin.graphics.PreferredBackBufferWidth * 8 / 10) - 10) / 7);
            }
            else
            {
                size_caseZoom = (((_origin.graphics.PreferredBackBufferHeight * 8 / 10) - 10) / 5);
            }
            NewMap.setSize(size_case);
            this.WayPoints = NewMap.GetWayPoints();
            this.map = NewMap.getMap();
        }
    }
}
