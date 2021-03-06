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
using Electric_Potatoe_TD.Mob;

namespace Electric_Potatoe_TD
{
    public class Tower : Node
    {
        protected int _level { get; set; }
        protected double _coef_power { get; set; }
        protected double _coef_speed { get; set; }
        protected double _coef_range { get; set; }

        protected double _multPowerAtt { get; set; }
        protected double _multSpeedAtt { get; set; }
        protected double _range { get; set; }

        protected int _counter;
        protected int _lastShoot;
        protected Boolean _bactivated { set; get; }

        public List<Electric_Potatoe_TD.Mob.Mob> listTarget = new List<Electric_Potatoe_TD.Mob.Mob>();
        //public List<Shoot> listBullet = new List<Shoot>();

        public Tower(float xPos, float yPos, int resistor, int cost, Game game)
            : base(xPos, yPos, resistor, cost, game)
        {
        }

        public override void levelUpTower()
        {
            _level += 1;
            _multPowerAtt *= _coef_power;
            _multSpeedAtt *= _coef_speed;
            _range *= _coef_range;
        }

        public override int getCostTower()
        {
            return _cost * (_level + 1);
        }

        public override int getTowerLevel()
        {
            return _level;
        }

        public double get { get; set; }

        public override void putInRange(Mob.Mob mob)
        {
            foreach (Mob.Mob m in listTarget)
            {
                if (mob == m)
                    return;
            }
            if ((System.Math.Sqrt(System.Math.Pow((mob.MobPos.X - (_position.X * _game.size_case)), 2)
                + System.Math.Pow((mob.MobPos.Y - (_position.Y * _game.size_case)), 2))) <= _range)
            {
                listTarget.Add(mob);
            }
        }

        public void checkOutOfRange()
        {
            int i = 0;

            while (i <= listTarget.Count)
            {
                if ((System.Math.Sqrt(System.Math.Pow((listTarget[i].MobPos.X - _position.X), 2)
                        + System.Math.Pow((listTarget[i].MobPos.Y - _position.Y), 2))) > _range)
                {
                    listTarget.RemoveAt(i);
                }
                i++;
            }
        }

        public override void update()
        {
            // Test
            _bactivated = true;
            //

            List<Mob.Mob> mobs = new List<Mob.Mob>();
            int i;

            i = 0;
            if (!_bactivated)
                return;
            while (i < listTarget.Count)
            {
                if (listTarget[i].IsDead())
                    listTarget.RemoveAt(i);
                i++;
            }
            if (listTarget.Count > 0)
            {
                if (_counter >= _multSpeedAtt)
                {
                    _counter = 0;
                    shoot(listTarget[listTarget.Count - 1]);
                }
                _counter++;
            }
            /*
            foreach (Shoot s in _game.BulletList)
            {
                s.update();
            }*/
            if (_game.BulletList.Count > 0)
                checkBulletHit();
        }

        public void checkBulletHit()
        {
            int i, j;

            i = 0;
            while (i < _game.BulletList.Count - 1)
            {
                if (_game.BulletList[i].update() == true)
                {
                    j = 0;
                    while (j < _game.BulletList[i].Target.Count - 1)
                    {
                        if (_game.BulletList[i].Target[j].IsDead())
                        {
                            _game.BulletList[j].Target.RemoveAt(j);
                            _game.mobIsDead(_game.BulletList[i].Target[j]);
                        }
                        j++;
                    }
                    _game.BulletList.RemoveAt(i);
                }
                i++;
            }
        }

        public void shoot(Mob.Mob mob)
        {
            Shoot bullet;

            switch (getType())
            {
                case EType.SPEED: {bullet = new Fast(mob, this); break;}
                case EType.STRENGHT: { bullet = new Spread(mob, this); break; }
                default: {bullet = new Bullet(mob, this); break;}
            }
            _game.BulletList.Add(bullet);
        }

        public override void removeMobCorpse(Mob.Mob mob)
        {
            int i = 0;

            while (i < listTarget.Count)
            {
                if (mob == listTarget[i])
                    listTarget.RemoveAt(i);
                i++;
            }
        }

        public override Game getGame()
        {
            return (_game);
        }

        public static int get_cost_tower(EType type)
        {
            switch (type)
            {
                case EType.GENERATOR:
                    return (50);
                case EType.SHOOTER:
                    return (30);
                case EType.SPEED:
                    return (100);
                case EType.STRENGHT:
                    return (100);
            }
            return (0);
        }
    }
}
