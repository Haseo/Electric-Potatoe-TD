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
        public List<Shoot> listBullet = new List<Shoot>();

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
            if ((System.Math.Sqrt(System.Math.Pow((mob.MobPos.X - _position.X), 2)
                + System.Math.Pow((mob.MobPos.Y - _position.Y), 2))) <= _range)
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
            List<Mob.Mob> mobs = new List<Mob.Mob>();
            int i, j;

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
                    shoot(listTarget[listTarget.Count]);
                }
                _counter++;
            }
            checkBulletHit();
        }

        public void checkBulletHit()
        {
            int i, j;

            i = 0;
            while (i < listBullet.Count)
            {
                if (listBullet[i].update() == true)
                {
                    j = 0;
                    while (j < listBullet[i].Target.Count)
                    {
                        if (listBullet[i].Target[j].IsDead())
                        {
                            listBullet[j].Target.RemoveAt(j);
                            _game.mobIsDead(listBullet[i].Target[j]);
                        }
                        j++;
                    }
                }
                i++;
            }
        }

        public void shoot(Mob.Mob mob)
        {
            Bullet bullet = new Bullet(mob, this);
            listBullet.Add(bullet);
        }

        public void removeMobCorpse(Mob.Mob mob)
        {
            int i = 0;

            while (i <= listTarget.Count)
            {
                if (mob == listTarget[i])
                    listTarget.RemoveAt(i);
                i++;
            }
        }

        public Game getGame()
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
