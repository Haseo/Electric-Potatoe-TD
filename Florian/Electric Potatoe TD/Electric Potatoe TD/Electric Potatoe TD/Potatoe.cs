using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Electric_Potatoe_TD
{
    class Potatoe : Node
    {
        private int _capital;
        
        public Potatoe(float xPos, float yPos, Game data)
            : base(xPos, yPos, 0, 0, data)
        {
            _capital = 1000;
        }

        public void takeDamage(int value)
        {
            _capital -= value;
        }

        public int getScore()
        {
            return (_capital);
        }

        public int getCapital()
        {
            return (_capital);
        }
    }
}
