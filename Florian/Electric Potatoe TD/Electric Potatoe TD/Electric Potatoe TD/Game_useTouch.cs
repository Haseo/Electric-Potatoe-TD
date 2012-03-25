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
using Microsoft.Devices.Sensors;
using Electric_Potatoe_TD.Mob;

namespace Electric_Potatoe_TD
{
    public partial class Game
    {
        public bool calc_posZoom()
        {
            test = "calc_posZoom" + Touch.X + "/" + Touch.Y;
            int x = (int)Touch.X, y = (int)Touch.Y;

            if (x > 0)
                x--;
            if (y > 0)
                y--;
            while ((x + 6) >= mapX && x > 0)
                x--;
            while ((y + 4) >= mapY && y > 0)
                y--;
            Zoom.X = x;
            Zoom.Y = y;
            return (true);
        }


        public bool Select_Node()
        {
            test = "Select_Node" + Touch.X + "/" + Touch.Y;
            return (true);
        }


        public bool Active_Desactive_Node()
        {
            test = "Active_Desactive_Node" + Touch.X + "/" + Touch.Y;
            return (true);
        }

        public bool init_wayTouch()
        {
            if (map[(int)Touch.X, (int)Touch.Y] == EMap.BACKGROUND)
            {
                bool stand = false;

                foreach (Node turret in TurretList)
                {
                    if (turret._position.X == Touch.X && turret._position.Y == Touch.Y && turret.CanaddLink() == true)
                    {
                        stand = true;
                        break;
                    }
                }

                if (stand == true)
                {
                    _ListWay.Add(Touch);
                    _ValidWay.Add(true);
                    return (true);
                }
            }
            test = "init_wayTouch : " + Touch.X + "/" + Touch.Y; ;
            return (false);
        }

        public bool add_wayTouch()
        {
            int x = -1;
            int stand = 0;

            foreach (Vector2 pos in _ListWay)
            {
                if (x == -1 && pos.X == Touch.X && pos.Y == Touch.Y)
                    x = stand;
                stand++;
            }
            if (x != -1)
            {
                if (x < _ListWay.Count)
                {
                    while ((x + 1) < _ListWay.Count && x > 0)
                    {
                        _ValidWay.RemoveAt(_ListWay.Count - 1);
                        _ListWay.RemoveAt(_ListWay.Count - 1);
                    }
                }
            }
            else
            {
                bool value = true;
                _ListWay.Add(Touch);
                if (map[(int)Touch.X, (int)Touch.Y] == EMap.BACKGROUND) // || ((int)Touch.X == _central.getPos().X && (int)Touch.Y == _central.getPos().Y))
                {
                    foreach (Node turret in TurretList)
                    {
                        if (turret._position.X == Touch.X && turret._position.Y == Touch.Y)
                        {
                            value = false;
                            break;
                        }
                    }
                }
                else
                    value = false;
                _ValidWay.Add(value);
            }
            test = "Add_wayTouch : " + Touch.X + "/" + Touch.Y;
            return (true);
        }

        public bool end_wayTouch()
        {
            if (can_access() == true && _ListWay.Count > 0)
            {
                Vector2 stand = _ListWay.Last<Vector2>();

                TurretList.Add(new Node(stand.X, stand.Y, 10, 10, this));
                make_connect(stand, _ListWay.First<Vector2>());
                test += "End_wayTouch : " + TurretList.Count.ToString() + "  ==  " + TurretList.Last<Node>()._position.ToString();
            }
            _ListWay.Clear();
            _ValidWay.Clear();
            return (true);
        }
    }
}
