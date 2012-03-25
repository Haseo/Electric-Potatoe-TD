using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Electric_Potatoe_TD
{
    public class Wave
    {
        public List<Type> ListOfMonster;

        public Wave()
        {
            ListOfMonster = new List<Type>();
        }

        public void AddMonsters(Type ty, int nb)
        {
            ListOfMonster.Add(ty);
        }

        public Mob.Mob SpawnMonster()
        {
            if (ListOfMonster.Count <= 0)
                return (null);
            Mob.Mob mob;
            Type[] myType = new Type[1];
            Object[] myParam = new Object[1];
            myParam[0] = new List<Vector2>();
            ((List<Vector2>)myParam[0]).Add(new Vector2(0, 0));

            myType[0] = typeof(List<Vector2>);
            ConstructorInfo method = ListOfMonster[0].GetConstructor(myType);

            mob = method.Invoke(myParam) as Mob.Mob;
            ListOfMonster.RemoveAt(0);
            return (mob);
        }
    }
}
