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
using Microsoft.Phone.Controls;

namespace Electric_Potatoe_TD
{
    public partial class MapLoader
    {
        public int size_case;
        public int[]  size;
        public EMap[,] filled;
        public List<Wave> ListOfWaves;
        public List<Vector2> WayPoints;

        public MapLoader()
        {
            ListOfWaves = new List<Wave>();
            WayPoints = new List<Vector2>();
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

        public void setSize(int i)
        {
            this.size_case = i;
            WayPoints.Add(new Vector2(0 * size_case, 2 * size_case));
            WayPoints.Add(new Vector2(4 * size_case, 2 * size_case));
            WayPoints.Add(new Vector2(4 * size_case, 1 * size_case));
            WayPoints.Add(new Vector2(7 * size_case, 1 * size_case));
            WayPoints.Add(new Vector2(7 * size_case, 2 * size_case));
            WayPoints.Add(new Vector2(9 * size_case, 2 * size_case));
        }

        public int[] getSize()
        {
            return size;
        }

        public List<Vector2> GetWayPoints()
        {
            return WayPoints;
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
