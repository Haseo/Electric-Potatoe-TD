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
        public EMap[,] filled;
        public List<Wave> ListOfWaves;

        public MapLoader()
        {
            ListOfWaves = new List<Wave>();
        }

        public void Load(int level)
        {
            switch (level)
            {
                case 1: LoadMap1(); break;
            }
        }

        public EMap[,] getMap()
        {
            return filled;
        }

        public int[] getSize()
        {
            return size;
        }
        /*this.map = new EMap[,]
{
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},                
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_TOPLEFT, EMap.CANYON_BOTRIGHT, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CENTRAL, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
    {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND},
};*/
    }
}
