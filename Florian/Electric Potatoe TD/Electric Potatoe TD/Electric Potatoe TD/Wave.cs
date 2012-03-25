using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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
            Mob.Mob mob;
            ConstructorInfo method = ListOfMonster[0].GetConstructor(null);

            mob = method.Invoke(this, null) as Mob.Mob;
            return (mob);
        }
    }
}
