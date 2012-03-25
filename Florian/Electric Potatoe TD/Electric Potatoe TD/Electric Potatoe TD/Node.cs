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
        protected int _cost { get; set; }
        const double POWER_GIVEN_BY_LEVEL = 0.1;

        protected Game _game;
        protected int _nodeLvl;

        protected int _resistor { get; set; }
        protected double _volt { get; set; }
        public double _intensity { get; set; }

        public List<Node> _peerOut { get; set; }
        public Boolean _activated { set; get; }

        public int getCost()
        {
            return _cost;
        }

        public int getCostNode()
        {
            return _cost * (_nodeLvl + 1);
        }

        public virtual int getCostTower()
        {
            return _cost;
        }

        public int getNodeLevel()
        {
            return _nodeLvl;
        }

        public virtual int getTowerLevel()
        {
            return 0;
        }

        public virtual Game getGame()
        {
            return _game;
        }

        public virtual void levelUpTower()
        {

        }

        public void levelUpNode()
        {
            _nodeLvl++;
        }

        public Node(float xPos, float yPos, int resistor, int cost, Game data)
            : base(xPos, yPos)
        {
            _resistor = resistor;
            _cost = cost;
            _nodeLvl = 0;
            _game = data;
            _activated = false;
            _peerOut = new List<Node>();
        }

        public Boolean addLink(Node contact)
        {
            if (_peerOut.Count > 3)
                return false;
            _peerOut.Add(contact);
            return true;
        }

        public Boolean CanaddLink()
        {
            if (_peerOut.Count > 3)
                return false;
            return true;
        }

        public virtual void update()
        {
        }

        public virtual double energyDiv()
        {
            double tmp = _intensity / _peerOut.Count;
            if (_nodeLvl == 1)
                return tmp;
            return tmp + (POWER_GIVEN_BY_LEVEL * _nodeLvl * tmp);
        }

        public virtual EType getType()
        {
            return EType.NODE;
        }

        public virtual void putInRange(Mob.Mob mob)
        {
        }

        public virtual void removeMobCorpse(Mob.Mob mob)
        {
        }
    }
}
