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
using Microsoft.Phone.Controls;


namespace Electric_Potatoe_TD.Mob
{
    public class Tank : Mob
    {
        public Tank(List<Vector2> NewWay)
        {
            this.mobMaxPV = 300;
            this.mobPV = 300;
            this.mobSpeed = 8;
            this.mobName = "Tank";
            this.Waypoint = NewWay;
            this.mobAttack = 5;
            this.mobType = EMobType.TANK;
            if (NewWay != null && NewWay.Count > 0)
                this.mobPos = NewWay[0];
        }
        public override EMobType GetMobType()
        {
            return EMobType.TANK;
        }
    }
}
