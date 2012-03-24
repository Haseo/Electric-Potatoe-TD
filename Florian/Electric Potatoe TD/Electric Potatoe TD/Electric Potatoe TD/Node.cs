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
    public class Node : Actor
    {
        const double POWER_GIVE_BY_LEVEL = 0.1;

        protected Game _game;
        protected int _nodeLvl { get; set; }

        protected int _resistor { get; set; }
        protected double _volt { get; set; }
        protected double _intensity { get; set; }

        protected List<Node> _peerOut { get; set; }
        protected Boolean _activated = false;
        protected int _cost { get; set; }

        public Node(float xPos, float yPos, int resistor, int cost, Game data) : base(xPos, yPos)
        {
            _resistor = resistor;
            _cost = cost;
            _nodeLvl = 1;
            _game = data;
        }

        public Boolean addLink(Node contact)
        {
            if (_peerOut.Count > 3)
                return false;
            _peerOut.Add(contact);
            return true;
        }

        public double energyDiv()
        {
            double tmp = _intensity / _peerOut.Count;
            if (_nodeLvl == 1)
                return tmp;
            return tmp + (POWER_GIVE_BY_LEVEL * _nodeLvl * tmp);
        }
    }
}
