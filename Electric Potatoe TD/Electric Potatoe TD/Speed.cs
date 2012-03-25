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
    class Speed : Tower
    {
        const float POWER = 30;
        const float SPEED = 14;
        const float RANGE = 400;

        const double COEF_POWER = 1.1;
        const double COEF_SPEED = 1.3;
        const double COEF_RANGE = 1.2;
        const double COEF_RESIST = 1.1;

        const int COST = 500;
        const int RESIST = 50;

        public Speed(float xPos, float yPos, int resistor, int cost, Game game)
            : base(xPos, yPos, resistor, cost, game)
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
        
        public override EType getType()
        {
            return (EType.SPEED);
        }
    }
}