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

namespace Electric_Potatoe_TD
{
    class Strenght : Tower
    {
        const float POWER = 20;
        const float SPEED = 10;
        const float RANGE = 100;

        const double COEF_POWER = 1.4;
        const double COEF_SPEED = 1.1;
        const double COEF_RANGE = 1.1;
        const double COEF_RESIST = 1.0;

        const int COST = 500;
        const int RESIST = 50;

        public Strenght(float xPos, float yPos, int resistor, int cost) : base(xPos, yPos, resistor, cost)
        {
            _multPowerAtt = POWER;
            _multSpeedAtt = SPEED;
            _range = RANGE;
            _coef_power = COEF_POWER;
            _coef_range = COEF_RANGE;
            _coef_speed = COEF_SPEED;
            _cost = COST;
            _resistor = RESIST;
        }
    }
}
