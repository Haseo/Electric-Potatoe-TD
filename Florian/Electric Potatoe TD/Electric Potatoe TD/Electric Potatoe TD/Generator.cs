using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Electric_Potatoe_TD
{
    class Generator : Node
    {
        const int GENERATOR__COST = 200;
        public double energyDiv()
        {
           return _game.getScore() / 2;
        }

        public Generator(float xPos, float yPos, int resistor, Game data) : base(xPos, yPos, resistor, 200, data)
        {
        }

        public EType getType()
        {
            return EType.GENERATOR;
        }
    }
}
