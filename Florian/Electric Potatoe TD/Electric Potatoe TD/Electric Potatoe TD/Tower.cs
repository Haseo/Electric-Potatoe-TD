using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectricPotato
{
    abstract class Tower : Node
    {
        int _level { get; private set; }

        float _multPowerAtt;
        float _multSpeedAtt;
        float _range;

        int _cost;
        int _lastShoot;
        Boolean _bactivated { set; get; }

        public Boolean levelUp(int capital)
        {
            if (_cost > capital)
                return false;
            capital -= _cost;
            return true;
        }

        public Tower(float xPos, float yPos, int width, int height, int resistor, int coast) : base(xPos, yPos, width, height, resistor, coast)
        {
            

        }
}
