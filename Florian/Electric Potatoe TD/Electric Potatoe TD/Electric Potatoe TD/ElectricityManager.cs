using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Electric_Potatoe_TD
{
    class ElectricityManager
    {
        static void ElecticityUpdate(Game game)
        {
            List<Node> emp = new List<Node>();
            Node tmp = game.TurretList.First<Node>();
            ElectricityCalcul(tmp, game.getScore(), game.getScore(), true, emp);
        }

        static void ElectricityCalcul(Node actual, int VoltageColector, int Intansity, bool previous, List<Node>Visited)
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
                   VoltageColector = -actual.getCost();
                }
            }
            Visited.Add(actual);
            actual._peerOut.ForEach(delegate(Node other)
            {
                bool visit = false;
                Visited.ForEach(delegate(Node buf)
                {
                    if (buf == other)
                        visit = true;
                });
                 if (visit == false)
                    ElectricityCalcul(other, VoltageColector, (int)actual.energyDiv(), actual._activated, Visited); 
                });
        }
    }
}
