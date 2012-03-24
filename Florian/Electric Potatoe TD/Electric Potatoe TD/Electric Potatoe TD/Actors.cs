using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectricPotato
{
    abstract class Actor
    {
        float _positionX { get; private set; }
        float _positiony { get; private set; }
        int _width;
        int _height;

        public Actor(float xPos, float yPos, int width, int height)
        {
            _positionX = xPos;
            _positiony = yPos;
            _width = width;
            _height = height;
        }
    }
}
