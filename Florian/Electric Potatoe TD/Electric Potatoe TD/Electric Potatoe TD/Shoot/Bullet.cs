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
    public class Bullet : Shoot
    {
        public  Bullet(Mob.Mob newTarget, Tower tower)
        {
            this._dmg = 20;
            this._speed = 2;
            this._game = tower.getGame();
            this._coord.X = tower.getPosition().X * tower.getGame().size_case;
            this._coord.Y = tower.getPosition().Y * tower.getGame().size_case;
            this._target = new List<Mob.Mob>();
            this._target.Add(newTarget);
            this._area = 0;
            this.loopLifeMax = 200;
        }

        public override EBulletType GetType()
        {
            return (EBulletType.BULLET);
        }
    }
}
