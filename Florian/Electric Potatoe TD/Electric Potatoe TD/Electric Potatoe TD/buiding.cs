using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectricPotato
{
    abstract class Building : Actor
    {
        int _resistor { get; private set; }
        int _coast { get; private set; }

        float _volt { get; set; }
        float _int { get; set; }

        public Building(float xPos, float yPos, int width, int height, int resistor, int coast)  : base(xPos, yPos, width, height)
        {
            _resistor = resistor;
            _coast = coast;
        }
    }
}
