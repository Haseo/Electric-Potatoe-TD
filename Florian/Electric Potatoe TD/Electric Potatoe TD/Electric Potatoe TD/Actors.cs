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
    public enum EType
    {
        NODE,
        SHOOTER,
        STRENGHT,
        SPEED,
        GENERATOR
    }

    public abstract class Actor
    {
        // Les setters etaient en private
        public Vector2 _position;

        public Actor(float xPos, float yPos)
        {
            _position.X = xPos;
            _position.Y = yPos;
        }

        public Vector2 getPosition()
        {
            return (_position);
        }
    }
}
