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
        protected int _resistor { get; set; }
        protected int _cost { get; set; }

        protected float _volt { get; set; }
        protected float _int { get; set; }

        protected Boolean _isIn = false;
        protected List<Node> _peerOut { get; set; }
        protected Node _peerIn;

        public Node(float xPos, float yPos, int resistor, int cost) : base(xPos, yPos)
        {
            _resistor = resistor;
            _cost = cost;
        }

        Boolean addOutput(Node contact)
        {
            if (_peerOut.Count > 3)
                return false;
            _peerOut.Add(contact);
            return true;
        }

        Boolean addInput(Node contact)
        {
            if (!_isIn)
                return false;
            _peerIn = contact;
            return true;
        }
    }
}
