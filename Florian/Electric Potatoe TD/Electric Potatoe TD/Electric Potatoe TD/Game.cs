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
using Electric_Potatoe_TD.Mob;

namespace Electric_Potatoe_TD
{
    public enum EMap
    {
        BACKGROUND = 0,
        CANYON_HORIZONTAL = 1,
        CENTRAL = 2,
        CANYON_VERTICAL = 3,
        CANYON_TOPLEFT = 4,
        CANYON_BOTLEFT = 5,
        CANYON_TOPRIGHT = 6,
        CANYON_BOTRIGHT = 7,
    };

    public enum EMapTexture
    {
        GROUND = 0,
        HORIZONTAL = 1,
        VERTICAL = 2,
        TOPLEFT = 3,
        BOTLEFT = 4,
        TOPRIGHT = 5,
        BOTRIGHT = 6,
    };

    public class Game
    {
        Game1 _origin;
        Texture2D Menu;
        Texture2D RageMetter_top;
        Texture2D RageMetter_mid;
        Texture2D RageMetter_bot;
        SpriteFont RageMetter_font;
        Rectangle[] _position;
        Dictionary<EType, Texture2D> TypeTexture;
        Dictionary<EMapTexture, Texture2D> MapTexture;
        Dictionary<int, Color> LevelColor;
        Dictionary<int, Texture2D> LevelTexture;
        public List<Node> TurretList;

        Potatoe _central;

        int RageMetter;
        int RageMetter_flag;

        // Map
        EMap[,] map;
        int mapX, mapY;
        Vector2 pos_map;
        int size_case;
        int size_caseZoom;

        Vector2 Touch;
        bool _moveTouch;
        int _TouchFlag;
        int _ValueTouch;

        Vector2 Zoom;
        bool _zoom;

        // Selection
        Node _node;
        String test = "";

        public List<Electric_Potatoe_TD.Mob.Mob> listTarget = new List<Electric_Potatoe_TD.Mob.Mob>();

        public Game(Game1 game)
        {
            _origin = game;
            RageMetter = 0;
            RageMetter_flag = 0;
            _TouchFlag = 0;
            _ValueTouch = 0;
            _moveTouch = false;
            _zoom = false;
            Zoom = new Vector2(0, 0);
            _central = new Potatoe();
            TypeTexture = new Dictionary<EType,Texture2D>();
            MapTexture = new Dictionary<EMapTexture, Texture2D>();
            LevelColor = new Dictionary<int, Color>();
            LevelTexture = new Dictionary<int, Texture2D>();
        }


        public void Oriented_changed()
        {
            if (RageMetter < 100)
                RageMetter = (RageMetter + 3);
            if (RageMetter > 100)
                RageMetter = 100;
            RageMetter_flag = 0;
        }


        public void Initialize()
        {
            int stand = (_origin.graphics.PreferredBackBufferHeight - (_origin.graphics.PreferredBackBufferHeight / 6)) / 55;

            _position = new Rectangle[]
             { new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, _origin.graphics.PreferredBackBufferHeight * 5 / 6, _origin.graphics.PreferredBackBufferWidth / 10, _origin.graphics.PreferredBackBufferHeight / 6),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, stand, _origin.graphics.PreferredBackBufferWidth / 10, stand * 2),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, stand * 2, _origin.graphics.PreferredBackBufferWidth / 10, stand),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, stand * 49, _origin.graphics.PreferredBackBufferWidth / 10, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 1 / 3, _origin.graphics.PreferredBackBufferHeight * 10 / 11, 0, 0),
             };
        }

        public void LoadContent()
        {
            Menu = _origin.Content.Load<Texture2D>("Menu");
            RageMetter_top = _origin.Content.Load<Texture2D>("RageMeterHigh");
            RageMetter_mid = _origin.Content.Load<Texture2D>("RageMeterMiddle");
            RageMetter_bot = _origin.Content.Load<Texture2D>("RageMeterLow");
            RageMetter_font = _origin.Content.Load<SpriteFont>("RageMetter");
            MapTexture[EMapTexture.GROUND] = _origin.Content.Load<Texture2D>("Ground");
            MapTexture[EMapTexture.HORIZONTAL] = _origin.Content.Load<Texture2D>("CanyonHorizontal");
            MapTexture[EMapTexture.VERTICAL] = _origin.Content.Load<Texture2D>("CanyonVertical");
            MapTexture[EMapTexture.TOPLEFT] = _origin.Content.Load<Texture2D>("CanyonTopLeft");
            MapTexture[EMapTexture.TOPRIGHT] = _origin.Content.Load<Texture2D>("CanyonTopRight");
            MapTexture[EMapTexture.BOTLEFT] = _origin.Content.Load<Texture2D>("CanyonBotLeft");
            MapTexture[EMapTexture.BOTRIGHT] = _origin.Content.Load<Texture2D>("CanyonBotRight");
            TypeTexture[EType.SPEED] = _origin.Content.Load<Texture2D>("TowerFast");
            TypeTexture[EType.SHOOTER] = _origin.Content.Load<Texture2D>("TowerNormal");
            TypeTexture[EType.STRENGHT] = _origin.Content.Load<Texture2D>("TowerHeavy");
            TypeTexture[EType.NODE] = _origin.Content.Load<Texture2D>("Node");
            TypeTexture[EType.GENERATOR] = _origin.Content.Load<Texture2D>("TowerGenerator");
            LevelColor[0] = Color.White;
            LevelColor[1] = Color.Green;
            LevelColor[2] = Color.Orange;
            LevelColor[3] = Color.Red;
            LevelTexture[0] = _origin.Content.Load<Texture2D>("Level1");
            LevelTexture[1] = _origin.Content.Load<Texture2D>("Level1");
            LevelTexture[2] = _origin.Content.Load<Texture2D>("Level2");
            LevelTexture[3] = _origin.Content.Load<Texture2D>("Level3");
            LevelTexture[4] = _origin.Content.Load<Texture2D>("Level4");
        }

        public void UnloadContent()
        {
        }

        public bool calc_posZoom()
        {
            int x, y;
            //  PosZoom;
            //  Touch;
            if (Touch.X >= ((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) ||
                Touch.Y >= ((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10))
                return (false);
            x = ((int)Touch.X - 10) / size_case;
            y = ((int)Touch.Y - 10) / size_case;
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
            test = "Select_Node";
            int x, y;

            if (Touch.X >= ((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) ||
                Touch.Y >= ((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10))
                return (false);
            x = ((int)Touch.X - 10) / size_caseZoom;
            y = ((int)Touch.Y - 10) / size_caseZoom;
            if (x > 0)
                x--;
            if (y > 0)
                y--;
            while ((x + 6) >= mapX && x > 0)
                x--;
            while ((y + 4) >= mapY && y > 0)
                y--;
            return (true);
        }


        public bool Active_Desactive_Node()
        {
            test = "Active_Desactive_Node";
            return (true);
        }

        public bool init_wayTouch()
        {
            test = "init_wayTouch : " + Touch.X + "/" + Touch.Y; ;
            return (true);
        }

        public bool add_wayTouch()
        {
            test = "Add_wayTouch : " + Touch.X + "/" + Touch.Y;
            return (true);
        }

        public bool end_wayTouch()
        {
            test = "End_wayTouch : " + Touch.X + "/" + Touch.Y; ;
            return (true);
        }

        public void update()
        {
            if (RageMetter_flag >= 20 && RageMetter > 0)
            {
                RageMetter--;
                RageMetter_flag -= 5;
            }
            if (RageMetter_flag < 20)
                RageMetter_flag++;

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
                        Touch = PositionTouch;
                        _ValueTouch = 1;
                    }

                    if (_zoom == true && _moveTouch == false && _ValueTouch > 10 && touches[0].State == TouchLocationState.Moved && _ValueTouch > 1)
                    {
                        _TouchFlag = -1; ;
                        _ValueTouch = 0;
                        _moveTouch = true;
                        init_wayTouch();
                    }
                    else if (_moveTouch == false)
                    {
                        if (touches[0].State == TouchLocationState.Pressed && _TouchFlag > 0 &&
                            PositionTouch.X == Touch.X && PositionTouch.Y == Touch.Y)
                        {
                            if (_zoom == false)
                                _zoom = calc_posZoom();
                            else
                                _zoom = false;
                            _TouchFlag = -1;
                            _moveTouch = false;
                        }
                        else if (_TouchFlag == 0 && _zoom == true)
                        {
                            Active_Desactive_Node();
                            _TouchFlag = -1;
                            _moveTouch = false;
                        }
                        else if (touches[0].State == TouchLocationState.Released)
                        {
                            if (_ValueTouch < 10)
                            {
                                Touch = PositionTouch;
                                _ValueTouch = 0;
                                _TouchFlag = 6;
                                _moveTouch = false;
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
                            Touch = PositionTouch;
                            add_wayTouch();
                        }
                        if (_zoom == true && touches[0].State == TouchLocationState.Released)
                        {
                            Touch = PositionTouch;
                            end_wayTouch();
                            _moveTouch = false;
                        }
                    }
                }
                if (_ValueTouch > 0)
                    _ValueTouch++;
                if (_TouchFlag > 0)
                    _TouchFlag--;
            }
        }

        public void draw()
        {
            if (_zoom == false)
            {
                draw_map();
                draw_content();
            }
            else
            {
                draw_mapZoom();
                draw_contentZoom();
            }

        }

        public void draw_mapZoom()
        {
            int x = (int)Zoom.X, y = (int)Zoom.Y;

            for (x = (int)Zoom.X; x < Zoom.X + 7 && x < mapX; x++)
            {
                for (y = (int)Zoom.Y; y < Zoom.Y + 5 && y < mapY; y++)
                {
                    switch (this.map[x, y])
                    {
                        case EMap.BACKGROUND: _origin.spriteBatch.Draw(MapTexture[EMapTexture.GROUND], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_HORIZONTAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.HORIZONTAL], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CENTRAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.GROUND], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.Blue); break;
                        case EMap.CANYON_VERTICAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.VERTICAL], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_TOPRIGHT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.TOPRIGHT], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_TOPLEFT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.TOPLEFT], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_BOTRIGHT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.BOTRIGHT], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_BOTLEFT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.BOTLEFT], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                    }
                }
            }
            foreach (Node myTurret in TurretList)
            {
                if ((int)myTurret.getPosition().X >= (int)Zoom.X && (int)myTurret.getPosition().X < (int)Zoom.X + 7 && (int)myTurret.getPosition().Y >= (int)Zoom.Y && (int)myTurret.getPosition().Y < (int)Zoom.Y + 5)
                {
                   _origin.spriteBatch.Draw(TypeTexture[myTurret.getType()], new Rectangle((int)pos_map.X + (size_caseZoom * ((int)myTurret.getPosition().X - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * ((int)myTurret.getPosition().Y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), LevelColor[myTurret.getNodeLevel()]);
                } 
            }
        }

        public void draw_map()
        {
            int x = 0, y = 0;
            for (x = 0; x < mapX; x++)
            {
                for (y = 0; y < mapY; y++)
                {
                    switch (this.map[x, y])
                    {
                        case EMap.BACKGROUND: _origin.spriteBatch.Draw(MapTexture[EMapTexture.GROUND], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_HORIZONTAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.HORIZONTAL], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CENTRAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.GROUND], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.Blue); break;
                        case EMap.CANYON_VERTICAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.VERTICAL], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_TOPLEFT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.TOPLEFT], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_TOPRIGHT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.TOPRIGHT], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_BOTLEFT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.BOTLEFT], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_BOTRIGHT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.BOTRIGHT], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                    }
                }
            }
            foreach (Node myTurret in TurretList)
            {
                _origin.spriteBatch.Draw(TypeTexture[myTurret.getType()], new Rectangle((int)pos_map.X + (size_case * (int)myTurret.getPosition().X), (int)pos_map.Y + (size_case * (int)myTurret.getPosition().Y), size_case, size_case), LevelColor[myTurret.getNodeLevel()]); 
                if (myTurret.getType() == EType.STRENGHT || myTurret.getType() == EType.SPEED || myTurret.getType() == EType.SHOOTER || myTurret.getType() == EType.GENERATOR)
                    _origin.spriteBatch.Draw(LevelTexture[myTurret.getTowerLevel()], new Rectangle((int)pos_map.X + (size_case * (int)myTurret.getPosition().X), (int)pos_map.Y + (size_case * (int)myTurret.getPosition().Y), size_case, size_case), LevelColor[myTurret.getNodeLevel()]);
            }
        }

        public void draw_content()
        {
            int i = 1;
            _origin.spriteBatch.Draw(Menu, _position[0], Color.White);
            _origin.spriteBatch.Draw(RageMetter_top, _position[1], (RageMetter > 99 ? Color.Red : Color.White));
            while (i < 98)
            {
                _origin.spriteBatch.Draw(RageMetter_mid, new Rectangle(_position[2].X, _position[2].Y + (_position[2].Height * (i / 2)), _position[2].Width, _position[2].Height),
                    (RageMetter >= (100 - i) ? Color.Red : Color.White));
                i++;
            }
            _origin.spriteBatch.Draw(RageMetter_bot, _position[3], (RageMetter > 0 ? Color.Red : Color.White));
            _origin.spriteBatch.DrawString(RageMetter_font, RageMetter.ToString(), new Vector2(_position[3].X + (_position[3].Width / 3), _position[3].Y + (_position[3].Height / 3)), Color.Black);
            _origin.spriteBatch.DrawString(RageMetter_font, "Capital : " + _central.getCapital().ToString(), new Vector2(_position[4].X, _position[4].Y), Color.Black);
        }

        public void draw_contentZoom()
        {
            //     _origin.spriteBatch.DrawString(RageMetter_font, "Capital : " + _central.getCapital().ToString(), new Vector2(_position[4].X, _position[4].Y), Color.Black);
            _origin.spriteBatch.DrawString(RageMetter_font, "test : " + test, new Vector2(_position[4].X, _position[4].Y), Color.Black);
        }

        public void Restart()
        {
            RageMetter = 0;
            RageMetter_flag = 0;
            _TouchFlag = 0;
            _ValueTouch = 0;
            Zoom = new Vector2(0, 0);
            _moveTouch = false;
            _zoom = false;
            mapFiller();
            turretFiller();
        }

        public int getScore()
        {
            return (_central.getScore());
        }

        public void mapFiller()
        {
            this.mapX = 14;
            this.mapY = 7;
            pos_map.X = 10;
            pos_map.Y = 10;
            if ((((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) / mapX) <=
                (((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10) / mapY))
            {
                size_case = (((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) / mapX);
            }
            else
            {
                size_case = (((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10) / mapY);
            }
            if ((((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) / 7) <=
                (((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10) / 5))
            {
                size_caseZoom = (((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) / 7);
            }
            else
            {
                size_caseZoom = (((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10) / 5);
            }
            this.map = new EMap[,]
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
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CENTRAL, EMap.CENTRAL, EMap.CENTRAL, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CENTRAL, EMap.CENTRAL, EMap.CENTRAL, EMap.BACKGROUND, EMap.BACKGROUND},
            };
        }

        public void turretFiller()
        {
            int capital = 6000;
            TurretList = new List<Node>();
            TurretList.Add(new Node(0, 1, 10, 10, this));
            TurretList.Add(new Strenght(2, 4, 10, 10, this));
            TurretList.Add(new Speed(4, 2, 10, 10, this));
            TurretList.Add(new Shooter(0, 0, 10, 10, this));
            TurretList[2].levelUpNode(ref capital);
            TurretList[2].levelUpNode(ref capital);
            TurretList[3].levelUpNode(ref capital);
            TurretList[3].levelUpTower(ref capital);
            TurretList[3].levelUpTower(ref capital);
            TurretList[3].levelUpTower(ref capital);
        }
    }
}
