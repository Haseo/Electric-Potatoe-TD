using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Electric_Potatoe_TD
{
    public partial class MapLoader
    {
        public int[]  size;
        public Electric_Potatoe_TD.Game.EMap[,] filled;

        public void Load()
        {
            LoadMap1();
        }

    }
}
