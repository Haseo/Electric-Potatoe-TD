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

        public void updateSelected_item(Vector2 PositionTouch)
        {
            if ((_selectFlag == 1 || _selectFlag == 2) && _node != null)
            {
                if ((PositionTouch.X >= _position[7].X && PositionTouch.X <= (_position[7].X + _position[7].Width)) &&
                (PositionTouch.Y >= _position[7].Y && PositionTouch.Y <= (_position[7].Y + _position[7].Height)))
                {
                   // _selectFlag = 0;
                    if (_node.getNodeLevel() < 3 && _node.getCost() < _central.getCapital())
                    {
                        _node.levelUpNode();
                        if (_selectFlag == 2 && _turret.getNodeLevel() < 3)
                        {
                            _turret.levelUpNode();
                        }
                      //  test = "Level UP !!!";
                        //  _central.
                    }
                }
            }
            if (_selectFlag == 2 && _turret != null)
            {
                if ((PositionTouch.X >= _position[10].X && PositionTouch.X <= (_position[10].X + _position[10].Width)) &&
                (PositionTouch.Y >= _position[10].Y && PositionTouch.Y <= (_position[10].Y + _position[10].Height)))
                {
                    if (_turret.getTowerLevel() < 4 && _turret.getCost() < _central.getCapital())
                    {
                        _turret.levelUpTower();
                        //  _central.
                    }
                }
            }
            if (_selectFlag == 1 || _selectFlag == 2)
            {
                if ((PositionTouch.X >= _position[11].X && PositionTouch.X <= (_position[11].X + _position[11].Width)) &&
                (PositionTouch.Y >= _position[11].Y && PositionTouch.Y <= (_position[11].Y + _position[11].Height)))
                {
                  //  _selectFlag = 0;
                    if (_selectFlag == 1 && _node != null)
                        _node._activated = true;
                    if (_selectFlag == 2)
                        _turret._activated = true;
                }
                if ((PositionTouch.X >= _position[12].X && PositionTouch.X <= (_position[12].X + _position[12].Width)) &&
                (PositionTouch.Y >= _position[12].Y && PositionTouch.Y <= (_position[12].Y + _position[12].Height)))
                {
                   // _selectFlag = 0;
                    if (_selectFlag == 1 && _node != null)
                        _node._activated = false;
                    if (_selectFlag == 2)
                        _turret._activated = false;
                }
            }
            if ((PositionTouch.X >= _position[13].X && PositionTouch.X <= (_position[13].X + _position[13].Width)) &&
            (PositionTouch.Y >= _position[13].Y && PositionTouch.Y <= (_position[13].Y + _position[13].Height)))
            {
                _selectFlag = 0;
            }
            if (_selectFlag == 3)
            {
                if ((PositionTouch.X >= _position[15].X && PositionTouch.X <= (_position[15].X + _position[15].Width)) &&
                (PositionTouch.Y >= _position[15].Y && PositionTouch.Y <= (_position[15].Y + _position[15].Height)))
                {
                    _selectFlag = 0;
                    create_tower(EType.SHOOTER);
                }
                if ((PositionTouch.X >= _position[16].X && PositionTouch.X <= (_position[16].X + _position[16].Width)) &&
                (PositionTouch.Y >= _position[16].Y && PositionTouch.Y <= (_position[16].Y + _position[16].Height)))
                {
                    _selectFlag = 0;
                    create_tower(EType.STRENGHT);
                }
                if ((PositionTouch.X >= _position[17].X && PositionTouch.X <= (_position[17].X + _position[17].Width)) &&
                (PositionTouch.Y >= _position[17].Y && PositionTouch.Y <= (_position[17].Y + _position[17].Height)))
                {
                    _selectFlag = 0;
                    create_tower(EType.SPEED);
                }
                if ((PositionTouch.X >= _position[18].X && PositionTouch.X <= (_position[18].X + _position[18].Width)) &&
                (PositionTouch.Y >= _position[18].Y && PositionTouch.Y <= (_position[18].Y + _position[18].Height)))
                {
                    _selectFlag = 0;
                    create_tower(EType.GENERATOR);
                }

                Node new_turret = TurretList.Last<Node>();

                while (new_turret.getNodeLevel() < _node.getNodeLevel())
                    new_turret.levelUpNode();
            }
            if (_selectFlag == 1)
            {

                if ((PositionTouch.X >= _position[8].X && PositionTouch.X <= (_position[8].X + _position[8].Width)) &&
                (PositionTouch.Y >= _position[8].Y && PositionTouch.Y <= (_position[8].Y + _position[8].Height)))
                {
                    _selectFlag = 3;
                }
            }
        }

        public void create_tower(EType type_tower)
        {
            switch (type_tower)
            {
                case EType.GENERATOR :
                    this.TurretList.Add(new Generator(_node._position.X, _node._position.Y, this));
                    break;
                case EType.SHOOTER :
                    this.TurretList.Add(new Shooter(_node._position.X, _node._position.Y, 10, 10, this));
                    break;
                case EType.SPEED :
                    this.TurretList.Add(new Speed(_node._position.X, _node._position.Y, 10, 10, this));
                    break;
                case EType.STRENGHT :
                    this.TurretList.Add(new Strenght(_node._position.X, _node._position.Y, 10, 10, this));
                    break;
            }
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

                    if (_selectFlag > 0 && _zoom == true && _moveTouch == false && touches[0].State == TouchLocationState.Pressed)
                        updateSelected_item(PositionTouch);
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
                        _selectFlag = 0;
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
                            _selectFlag = 0;
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
                        else if (_ValueTouch > 15 && _zoom == true)
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
                            else
                                _moveTouch = false;
                            _TouchFlag = -1;
                        }
                        if (_zoom == true && touches[0].State == TouchLocationState.Released)
                        {
                            Touch = ident_pos(PositionTouch);
                            if (Touch.X != -1 && Touch.Y != -1)
                                end_wayTouch();
                            else
                                _moveTouch = false;
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
