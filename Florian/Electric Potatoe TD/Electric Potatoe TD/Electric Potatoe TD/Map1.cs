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
        private void LoadMap1()
        {
            size = new int[2];
            size[0] = 5;
            size[1] = 12;

            Wave wav1 = new Wave();
            wav1.AddMonsters(typeof(Mob.Tank), 2);
            ListOfWaves.Add(wav1);
            filled = new EMap[12, 5]
            {
                {0,0,(EMap)1,0,0},
                {0,0,(EMap)1,0,0},
                {0,0,(EMap)1,0,0},
                {0,0,(EMap)1,0,0},
                {0,(EMap)6,(EMap)5,0,0},
                {0,(EMap)1,0,0,0},
                {0,(EMap)1,0,0,0},
                {0,(EMap)4,(EMap)7,0,0},
                {0,0,(EMap)1,0,0},
                {0,0,(EMap)1,0,0},
                {0,0,(EMap)2,0,0},
                {0,0,0,0,0},
            };
        }
    }
}