using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Electric_Potatoe_TD
{
    class ElectricityManager
    {
        static public Boolean linkNode(Node src, Node dest)
        {
            if (src.addLink(dest) == true)
            {
                if (dest.addLink(src) == true)
                {
                   Pair<Vector2, Vector2> tmp = new Pair<Vector2,Vector2>();
                   tmp.First = src._position;
                   tmp.Second = dest._position;
                   src.getGame().nodeLink.Add(tmp);
                   return true;
                }
                else
                {
                    src._peerOut.Remove(dest);
                }
            }
            return false;
        }

        static public Boolean unlinkNode(Node src, Node dest)
        {
            src._peerOut.Remove(dest);
            dest._peerOut.Remove(src);
            int i = 0;
            while (i < src.getGame().nodeLink.Count)
            {
                Pair<Vector2, Vector2> buf = src.getGame().nodeLink.ElementAt(i);
                if ((src._position == buf.First && dest._position == buf.Second)
                        || (src._position == buf.First && dest._position == buf.Second))
                    src.getGame().nodeLink.Remove(buf);
                else
                    i++;
             }
            return true;
        }

        static public void ElecticityUpdate(Game game, Node center)
        {
            List<Node> emp = new List<Node>();
            Node tmp = center;
            int Volt = game.getScore();
            int In = game.getScore();
            ElectricityCalcul(tmp, ref Volt, In, true, ref emp);
        }

        static void ElectricityCalcul(Node actual, ref int VoltageColector, int Intensity, bool previous, ref List<Node>Visited)
        {
            if (VoltageColector <= 0 || previous ==  false)
            {
                actual._activated = false;
            }
            else 
            {
                if (VoltageColector < actual.getCost())
                {
                   actual._activated = false;
                }
                else
                {
                   actual._activated = true;
                   actual._intensity = Intensity;
                   VoltageColector = VoltageColector - actual.getCost();
                }
            }
            Visited.Add(actual);
            List<Node> tmp = Visited;
            int localVoltage  = VoltageColector;
            actual._peerOut.ForEach(delegate(Node other)
            {
                bool visit = false;
                foreach (Node buf in tmp)
                {
                    if (buf == other)
                        visit = true;
                }
                 if (visit == false)
                     ElectricityCalcul(other, ref localVoltage, (int)actual.energyDiv(), actual._activated, ref tmp); 
                });
            VoltageColector = localVoltage;
            Visited.AddRange(tmp);
        }
    }
}
