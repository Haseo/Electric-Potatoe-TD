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
    public class Boss : Mob
    {
        public Boss(List<Vector2> NewWay)
        {
            this.mobMaxPV = 500;
            this.mobPV = 500;
            this.mobSpeed = 5;
            this.mobName = "Boss";
            this.Waypoint = NewWay;
            this.mobAttack = 50;
            this.mobType = EMobType.BOSS;
            if (NewWay != null && NewWay.Count > 0)
                this.mobPos = NewWay[0];
        }
        public override EMobType GetMobType()
        {
            return EMobType.BOSS;
        }
    }
}
