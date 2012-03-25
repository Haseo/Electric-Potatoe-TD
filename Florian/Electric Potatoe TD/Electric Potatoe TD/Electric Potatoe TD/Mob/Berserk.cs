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

    public class Berserk : Mob
    {
        protected float modificator;
        protected float newSpeed;
        protected float newAttack;

        public Berserk(List<Vector2> NewWay)
        {
            this.mobMaxPV = 75;
            this.mobPV = 75;
            this.mobSpeed = 4;
            this.mobName = "Berserk";
            this.Waypoint = NewWay;
            this.mobAttack = 5;
            this.mobType = EMobType.BERSERK;
            if (NewWay != null && NewWay.Count > 0)
                this.mobPos = NewWay[0];
            modificator = 1;
            this.newAttack = 10;
            this.newSpeed = 4;
        }
        public override EMobType GetMobType()
        {
            return EMobType.BERSERK;
        }
        protected virtual void BerserkModicator()
        {
            this.modificator = ((this.mobPV * 100) / this.mobMaxPV);
            this.modificator = this.modificator / 100;
        }

        protected override int Attack()
        {
            if (this.modificator != 0)
                this.newAttack = this.mobAttack / this.modificator;
            else
                this.newAttack = 1/10;
            if (this.Waypoint != null && this.Waypoint.Count < 0)
                if (this.Waypoint.Count == 1 && this.Waypoint[0] == this.mobPos)
                {
                    Console.WriteLine("le mob attaque la central : ");
                    return (int)this.newAttack;
                }
            return (0);
        }

        protected override bool isMoving()
        {
            this.newSpeed = this.modificator * this.mobSpeed;
            if (this.currentLoop >= this.newSpeed)
            {
                this.currentLoop = 0;
                return true;
            }
            return false;
        }
        public override int update()
        {
            this.BerserkModicator();
            return base.update();
        }
    }
}