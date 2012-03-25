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
        public void update_RageMetter()
        {
            if (RageMetter_flag >= 20 && RageMetter > 0)
            {
                RageMetter--;
                RageMetter_flag -= 5;
            }
            if (RageMetter_flag < 20)
                RageMetter_flag++;
        }

        public void update(GameTime gameTime)
        {
            game_loop(gameTime);
            update_RageMetter();
            if (AccAllow)
                mvtBonus();
            TouchPanelCapabilities touchCap = TouchPanel.GetCapabilities();
            if (touchCap.IsConnected)
            {
                TouchCollection touches = TouchPanel.GetState();
                if (touches.Count >= 1)
                {
                    Vector2 PositionTouch = touches[0].Position;

                    if (_zoom == false && _moveTouch == false && touches[0].State == TouchLocationState.Pressed &&
                        (PositionTouch.X >= _position[0].X && PositionTouch.X <= (_position[0].X + _position[0].Width)) &&
                        (PositionTouch.Y >= _position[0].Y && PositionTouch.Y <= (_position[0].Y + _position[0].Height)))
                    {
                        _origin.change_statut(Game1.Game_Statut.Menu_Ig);
                        _ValueTouch = 0;
                    }
                    else if (touches[0].State == TouchLocationState.Pressed && _moveTouch == false)
                    {
                        Touch = ident_pos(PositionTouch);
                        if (Touch.X != -1 && Touch.Y != -1)
                            _ValueTouch = 1;
                    }
                    if (_zoom == true && _moveTouch == false && _ValueTouch > 10 && touches[0].State == TouchLocationState.Moved && _ValueTouch > 1 &&
                         (Touch.X != ident_pos(PositionTouch).X || Touch.Y != ident_pos(PositionTouch).Y))
                    {
                        _TouchFlag = -1;
                        _ValueTouch = 0;
                        _moveTouch = init_wayTouch();
                    }
                    else if (_moveTouch == false)
                    {
                        if (_moveTouch == false && touches[0].State == TouchLocationState.Pressed && _TouchFlag > 0 &&
                            (Touch.X == ident_pos(PositionTouch).X && Touch.Y == ident_pos(PositionTouch).Y) &&
                            Touch.X != -1 && Touch.Y != -1)
                        {
                            if (_zoom == false)
                                _zoom = calc_posZoom();
                            else
                                _zoom = false;
                            _TouchFlag = -1;
                            _moveTouch = false;
                        }
                        else if (touches[0].State == TouchLocationState.Released)
                        {
                            if (_ValueTouch < 10)
                            {
                                Touch = ident_pos(PositionTouch);
                                if (Touch.X != -1 && Touch.Y != -1)
                                {
                                    _ValueTouch = 0;
                                    _TouchFlag = 6;
                                    _moveTouch = false;
                                }
                                else
                                    _TouchFlag = -1;
                            }
                            else if (_zoom == true)
                            {
                                Select_Node();
                                _ValueTouch = 0;
                                _TouchFlag = -1;
                                _moveTouch = false;
                            }
                        }
                        else if (_ValueTouch > 20 && _zoom == true)
                        {
                            Select_Node();
                            _ValueTouch = 0;
                            _TouchFlag = -1;
                            _moveTouch = false;
                        }
                    }
                    else if (_moveTouch == true)
                    {
                        if (_zoom == true && touches[0].State == TouchLocationState.Moved)
                        {
                            Touch = ident_pos(PositionTouch);
                            if (Touch.X != -1 && Touch.Y != -1)
                                add_wayTouch();
                            _TouchFlag = -1;
                        }
                        if (_zoom == true && touches[0].State == TouchLocationState.Released)
                        {
                            Touch = ident_pos(PositionTouch);
                            if (Touch.X != -1 && Touch.Y != -1)
                                end_wayTouch();
                            _moveTouch = false;
                            _TouchFlag = -1;
                        }
                    }
                }
                if (_ValueTouch > 0)
                    _ValueTouch++;
                if (_TouchFlag > 0)
                    _TouchFlag--;
            }
        }
    }
}