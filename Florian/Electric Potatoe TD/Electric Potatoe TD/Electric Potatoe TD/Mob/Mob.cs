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
    public enum EMobType
    {
        PEON,
        SPEED,
        TANK,
        BERSERK,
        BOSS
    };

    public abstract class Mob
    {
        #region Propriétés

        protected String           mobName;
        protected int              mobPV;
        protected int              mobMaxPV;
        protected int              mobSpeed;
        protected int              mobAttack;
        protected Vector2          mobPos;
        protected int               currentLoop;
        protected List<Vector2>   Waypoint;
        protected EMobType            mobType;
        #endregion

        #region asseceurs

        public EMobType MobType { get { return this.mobType; } }
        public String ModName {get{return this.mobName;}}
        public int MobPV { get { return this.mobPV; } }
        public int MobMaxPV{ get {return this.mobMaxPV; }}
        public int MobSpeed { get { return this.mobSpeed; } }
        public int MobAttack { get { return this.mobAttack; } }
        public Vector2 MobPos { get { return this.mobPos; } }
        
        #endregion

        #region Methode

        #region Attack-defence
        public virtual bool TakeDamage(int damage)
        {
            this.mobPV -= damage;
            return this.IsDead();
        }

        public virtual bool IsDead()
        {
            if (this.mobPV <= 0)
                return true;
            return false;
        }

        protected virtual int Attack()
        {
            if (this.Waypoint != null && this.Waypoint.Count < 0)
                if (this.Waypoint.Count == 1 && this.Waypoint[0] == this.mobPos)
                {
                    Console.WriteLine("le mob attaque la central : ");
                    return this.mobAttack;
                }
            return 0;
        }
        #endregion
        #region move
        protected virtual void CalcNewCoord()
        {
            if(this.Waypoint != null && this.Waypoint.Count > 0)
            {
                if (this.Waypoint.Count == 1 && this.Waypoint[0] == this.mobPos)
                    return;
                if (this.Waypoint[0] == this.mobPos)
                this.Waypoint.RemoveAt(0);
                else
                {
                    if (this.Waypoint[0].X < this.mobPos.X)
                    {
                        this.mobPos.X--;
                        if (this.Waypoint[0].X > this.mobPos.X)
                            this.mobPos.X = this.Waypoint[0].X;
                    }
                    else if (this.Waypoint[0].X > this.mobPos.X)
                    {
                        this.mobPos.X++;
                        if (this.Waypoint[0].X < this.mobPos.X)
                            this.mobPos.X = this.Waypoint[0].X;
                    }
                    else if (this.Waypoint[0].Y < this.mobPos.Y)
                    {
                        this.mobPos.Y--;
                        if (this.Waypoint[0].Y > this.mobPos.Y)
                            this.mobPos.Y = this.Waypoint[0].Y;
                    }
                    else if (this.Waypoint[0].Y > this.mobPos.Y)
                    {
                        this.mobPos.Y++;
                        if (this.Waypoint[0].Y < this.mobPos.Y)
                            this.mobPos.Y = this.Waypoint[0].Y;
                    }
                }
           }
        }


        protected virtual bool isMoving()
        {
            if (this.currentLoop >= this.mobSpeed)
            {
                this.currentLoop = 0;
                return true;
            }
            return false;
        }

        #endregion
        #region update

        public virtual int update()
        {
            if (this.isMoving())
            {
                this.CalcNewCoord();
                Console.WriteLine(this.mobPos.ToString());
                return (this.Attack());
                
            }
            else
                this.currentLoop++;
            return (-1);
        }

        #endregion
        #endregion
    };
}
