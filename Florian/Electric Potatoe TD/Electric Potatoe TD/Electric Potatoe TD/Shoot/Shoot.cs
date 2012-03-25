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

namespace Electric_Potatoe_TD
{
    public abstract class Shoot
    {
        protected int _dmg;
        protected int _speed;
        protected Game _game;
        protected Vector2 _coord;
        protected List<Mob.Mob> _target;
        protected int _area;
        protected Vector2 lastTargetPos;

        public int Dmg { get { return _dmg; } }
        public int Speed { get { return _speed; } }
        public Vector2 Coord { get { return _coord; } }
        public List<Mob.Mob> Target { get { return _target; } }

        protected virtual void move()
        {
            double  d;
            double  angle;
            int move_x;
            int move_y;
            double test1;
            double test2;
            Vector2 triangle = new Vector2();

            getTargetCoord();
            getdistance(triangle);
            triangle.X = Math.Abs(lastTargetPos.X - _coord.X);
            triangle.Y = Math.Abs(lastTargetPos.Y - _coord.Y);
            if (lastTargetPos != _coord)
            {
            d = Math.Sqrt(Math.Pow(triangle.X, 2) + Math.Pow(triangle.Y, 2));
            angle = Math.Acos(triangle.X / d);
            angle = angle *( 180 / Math.PI);
            test1 = (Math.Cos(angle) * _speed);
            test2 = (Math.Sin(angle) * _speed);
            move_x = (int)test1;
            move_y = (int)test2;
            CalcNewPostion(move_x, move_y);
            }
        }

        protected virtual void CalcNewPostion(int move_x, int move_y)
        {
            if (lastTargetPos.X < _coord.X)
            {
                _coord.X -= move_x;
                if (lastTargetPos.X > _coord.X)
                    _coord.X = lastTargetPos.X;
            }
            else if (lastTargetPos.X > _coord.X)
            {
                _coord.X += move_x;
                if (lastTargetPos.X < _coord.X)
                    _coord.X = lastTargetPos.X;
            }
            if (lastTargetPos.Y < _coord.Y)
            {
                _coord.Y -= move_y;
                if (lastTargetPos.Y > _coord.Y)
                    _coord.Y = lastTargetPos.Y;
            }
            else if (lastTargetPos.Y > _coord.Y)
            {
                _coord.Y += move_y;
                if (lastTargetPos.Y < _coord.Y)
                    _coord.Y = lastTargetPos.Y;
            }
        }

        protected virtual void getdistance(Vector2 triangle)
        {
            triangle.X = Math.Abs(lastTargetPos.X - _coord.X);
            triangle.Y = Math.Abs(lastTargetPos.Y - _coord.Y);
        }

        protected virtual void getTargetCoord()
        {
            if (this._target != null && _target.Count > 0)
                this.lastTargetPos = this._target[0].MobPos;
        }

        public virtual bool update()
        {
            this.move();
            return this.hit();
        }

        protected virtual bool hit()
        {
           // getTargetCoord();
            if (lastTargetPos.X <= _coord.X + (_game.size_case / 3) && lastTargetPos.X >= _coord.X - (_game.size_case / 3) &&
                lastTargetPos.Y <= _coord.Y + (_game.size_case / 3) && lastTargetPos.Y >= _coord.Y - (_game.size_case / 3))
            {
                this.touch();
                return true;
            }
            return false;
        }

        protected virtual void touch()
        {
            int idx;

            idx = 0;
            while (idx < _target.Count)
            {
                _target[idx].TakeDamage(_dmg);
                idx++;
            }
        }

        public virtual EBulletType GetType()
        {
            return (EBulletType.BULLET);
        }
    }
}
