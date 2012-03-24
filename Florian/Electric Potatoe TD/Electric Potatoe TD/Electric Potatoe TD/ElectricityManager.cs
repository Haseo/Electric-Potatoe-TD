using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Electric_Potatoe_TD
{
    class ElectricityManager
    {
        static public void ElectricityCalcul(Node actual, int VoltageColector, int Intansity, bool previous)
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
            actual._peerOut.ForEach(delegate(Node other)
            {
                ElectricityCalcul(other, VoltageColector, (int)actual.energyDiv(), actual._activated); 
            });
        }
    }
}
