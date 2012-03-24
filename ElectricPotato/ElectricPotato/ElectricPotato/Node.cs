using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectricPotato
{
    abstract class Node : Building
    {
        Boolean _isIn = false;
        List<Node> _peerOut { get; }
        Node _peerIn;

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

        Node(float xPos, float yPos, int width, int height, int resistor, int coast)
            : base(xPos, yPos, width, height, resistor, coast)
        {

        }
    }
}
