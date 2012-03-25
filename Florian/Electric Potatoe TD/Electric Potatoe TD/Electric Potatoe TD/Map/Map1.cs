using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Electric_Potatoe_TD;

namespace Electric_Potatoe_TD
{
    public partial class MapLoader
    {
        private void LoadMap1()
        {
            size = new int[2];
            size[0] = 5;
            size[1] = 12;

            Wave wav = new Wave();
            wav.AddMonsters(typeof(Mob.Peon), 1);
            wav.AddMonsters(typeof(Mob.Peon), 1);
            wav.AddMonsters(typeof(Mob.Peon), 1);
            wav.AddMonsters(typeof(Mob.Peon), 1);
            wav.AddMonsters(typeof(Mob.Berserk), 1);
            wav.AddMonsters(typeof(Mob.Tank), 1);
           // wav.AddMonsters(typeof(Mob.Boss), 1);
            wav.AddMonsters(typeof(Mob.Speed), 1);

            ListOfWaves.Add(wav);
            ListOfWaves.Add(wav);
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