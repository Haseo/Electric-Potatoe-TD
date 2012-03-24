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
        // Les setters etaient en private
        protected int _level { get; set; }
        protected double _coef_power { get; set; }
        protected double _coef_speed { get; set; }
        protected double _coef_range { get; set; }

        protected double _multPowerAtt { get; set; }
        protected double _multSpeedAtt { get; set; }
        protected double _range { get; set; }

        protected int _lastShoot;
        protected Boolean _bactivated { set; get; }

        public List<Electric_Potatoe_TD.Mob.Mob> listTarget = new List<Electric_Potatoe_TD.Mob.Mob>();

        public Tower(float xPos, float yPos, int resistor, int cost, Game game)
            : base(xPos, yPos, resistor, cost, game)
        {
        }

        /*
         * Le level up coute son prix initial multiplie par le level actuel
         * Pour chaque level, la puissance d'attaque, la vitesse d'attaque et la portee augmente
         * Le coeficient est determine par les classes filles
         * 
         * */

        public Boolean levelUp(int capital)
        {
            if (_cost > capital)
                return false;
            capital -= _cost * _level;
            _level += 1;
            _multPowerAtt *= _coef_power;
            _multSpeedAtt *= _coef_speed;
            _range *= _coef_range;
            return true;
        }

        public double get { get; set; }

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
    }
}
