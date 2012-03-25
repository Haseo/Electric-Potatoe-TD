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
        this._speed = 5;
        this._game = tower.getGame();
        this._coord = tower.getPosition();
        this._target = newTarget;
        this._area = 0;
    }

    }
}
