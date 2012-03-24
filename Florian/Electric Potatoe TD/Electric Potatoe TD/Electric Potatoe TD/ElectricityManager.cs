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
                   VoltageColector = -actual.getCost();
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
