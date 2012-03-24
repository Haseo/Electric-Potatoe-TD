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
    public class Peon : Mob
    {
        public Peon(List<Vector2> NewWay)
        {
            this.mobMaxPV = 100;
            this.mobPV = 100;
            this.mobSpeed = 4;
            this.mobName = "peon";
            this.Waypoint = NewWay;
            this.mobAttack = 10;
            this.mobType = EMobType.PEON;
            if (NewWay != null && NewWay.Count > 0)
                this.mobPos = NewWay[0];
        }
    }
}
