using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Electric_Potatoe_TD
{
    class Generator : Node
    {
        const int GENERATOR__COST = 200;
        public override double energyDiv()
        {
           return _game.getScore() / 2;
        }

        public Generator(float xPos, float yPos, Game data) : base(xPos, yPos, 200, 200, data)
        {
        }

        public override EType getType()
        {
            return EType.Generator;
        }
    }
}
