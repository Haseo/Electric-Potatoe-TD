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

namespace Electric_Potatoe_TD
{
    class Tutorial
    {
        struct scene
        {
            public List<Node> _nod;
            public List<Mob.Mob> _mobs;
            public EMap[,] map;
            public string Description;
            public Vector2 posNoConstruct;

            public scene(List<Node> nod, List<Mob.Mob> mobs, EMap[,] m, string Des, Vector2 posNC)
            {
                _nod = nod;
                _mobs = mobs;
                map = new EMap[,]
                {
                    {EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND},
                    {EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND},
                    {EMap.BACKGROUND, EMap.CANYON_HORIZONTAL, EMap.BACKGROUND},
                };
                Description = Des;
                posNoConstruct = posNC;
            }
        };

        Game1 _origin;
        Texture2D Logo;
        Texture2D Button;
        SpriteFont Font;
        Rectangle[] _position;
        List<scene> _scenes;
        int _current;
        int _sizeCase;

        public Tutorial(Game1 game)
        {
            _origin = game;
            _scenes = new List<scene>();
            _current = 0;
            _sizeCase = 100;
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "Exemple", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "Ceci est une description\n2nd ligne ....\n3rd Ligne...", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "Ceci est une description\n2nd ligne ....\n3rd Ligne...", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "The Game", new Vector2(2, 1)));
            _scenes[1]._nod.Add(new Node(1, 1, 10, 42, null));
            _scenes[2]._nod.Add(new Shooter(1, 1, 10, 42, null));
            _scenes[2]._nod.Last<Node>().levelUpTower();
            _scenes[2]._nod.Last<Node>().levelUpNode();

            _scenes[1]._mobs.Add(new Mob.Speed(null));
            _scenes[2]._mobs.Add(new Mob.Berserk(null));
        }

        public void Initialize()
        {
            _position = new Rectangle[]
             {  new Rectangle(_origin.graphics.PreferredBackBufferWidth / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 2 / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 3 / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth / 2, _origin.graphics.PreferredBackBufferHeight * 1 / 10, 180, 90),
             };
        }

        public void Restart()
        {
            _current = 0;
        }

        public void LoadContent()
        {
            Logo = _origin.Content.Load<Texture2D>("Logo");
            Button = _origin.Content.Load<Texture2D>("Button");
            Font = _origin.Content.Load<SpriteFont>("MenuFont");
        }

        public void UnloadContent()
        {
        }


        public void update()
        {
            TouchPanelCapabilities touchCap = TouchPanel.GetCapabilities();
            if (touchCap.IsConnected)
            {
                TouchCollection touches = TouchPanel.GetState();
                if (touches.Count >= 1)
                {
                    if (touches[0].State == TouchLocationState.Pressed)
                    {
                        Vector2 PositionTouch = touches[0].Position;

                        if ((PositionTouch.X >= _position[0].X && PositionTouch.X <= (_position[0].X + _position[0].Width)) &&
                            (PositionTouch.Y >= _position[0].Y && PositionTouch.Y <= (_position[0].Y + _position[0].Height)))
                        {
                            _origin.change_statut(Game1.Game_Statut.Menu);
                        }
                        if ((PositionTouch.X >= _position[1].X && PositionTouch.X <= (_position[1].X + _position[1].Width)) &&
                            (PositionTouch.Y >= _position[1].Y && PositionTouch.Y <= (_position[1].Y + _position[1].Height)))
                        {
                            if (_current > 0)
                                _current--;
                        }
                        if ((PositionTouch.X >= _position[2].X && PositionTouch.X <= (_position[2].X + _position[2].Width)) &&
                            (PositionTouch.Y >= _position[2].Y && PositionTouch.Y <= (_position[2].Y + _position[2].Height)))
                        {
                            if ((_current + 1) < _scenes.Count)
                                _current++;
                        }
                    }
                }
            }
        }

        public void draw()
        {
            Rectangle Frame = new Rectangle(0, 0, 40, 40);

            if (_current >= 0 && _current < _scenes.Count)
            {
                scene cur = _scenes.ElementAt<scene>(_current);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        switch (cur.map[i, j])
                        {
                            case EMap.BACKGROUND: _origin.spriteBatch.Draw(_origin._game.MapTexture[EMapTexture.GROUND], new Rectangle(10 + (_sizeCase * i), 10 + (_sizeCase * j), _sizeCase, _sizeCase), Color.White); break;
                            case EMap.CANYON_HORIZONTAL: _origin.spriteBatch.Draw(_origin._game.MapTexture[EMapTexture.HORIZONTAL], new Rectangle(10 + (_sizeCase * i), 10 + (_sizeCase * j), _sizeCase, _sizeCase), Color.White); break;
                            case EMap.CENTRAL: _origin.spriteBatch.Draw(_origin._game.MapTexture[EMapTexture.GROUND], new Rectangle(10 + (_sizeCase * i), 10 + (_sizeCase * j), _sizeCase, _sizeCase), Color.White); break;
                            case EMap.CANYON_VERTICAL: _origin.spriteBatch.Draw(_origin._game.MapTexture[EMapTexture.VERTICAL], new Rectangle(10 + (_sizeCase * i), 10 + (_sizeCase * j), _sizeCase, _sizeCase), Color.White); break;
                            case EMap.CANYON_TOPRIGHT: _origin.spriteBatch.Draw(_origin._game.MapTexture[EMapTexture.TOPRIGHT], new Rectangle(10 + (_sizeCase * i), 10 + (_sizeCase * j), _sizeCase, _sizeCase), Color.White); break;
                            case EMap.CANYON_TOPLEFT: _origin.spriteBatch.Draw(_origin._game.MapTexture[EMapTexture.TOPLEFT], new Rectangle(10 + (_sizeCase * i), 10 + (_sizeCase * j), _sizeCase, _sizeCase), Color.White); break;
                            case EMap.CANYON_BOTRIGHT: _origin.spriteBatch.Draw(_origin._game.MapTexture[EMapTexture.BOTRIGHT], new Rectangle(10 + (_sizeCase * i), 10 + (_sizeCase * j), _sizeCase, _sizeCase), Color.White); break;
                            case EMap.CANYON_BOTLEFT: _origin.spriteBatch.Draw(_origin._game.MapTexture[EMapTexture.BOTLEFT], new Rectangle(10 + (_sizeCase * i), 10 + (_sizeCase * j), _sizeCase, _sizeCase), Color.White); break;
                        }
                    }
                }

                if (cur.posNoConstruct.X != -1 && cur.posNoConstruct.Y != -1 && cur.posNoConstruct.X < 3 && cur.posNoConstruct.Y < 3)
                    _origin.spriteBatch.Draw(_origin._game.NoConstruct, new Rectangle(10 + ((int)cur.posNoConstruct.X * _sizeCase), 10 + ((int)cur.posNoConstruct.Y * _sizeCase), _sizeCase, _sizeCase), Color.White);
                foreach (Node myTurret in cur._nod)
                {
                    if (myTurret._position.X < 3 && myTurret._position.Y < 3)
                    {
                        _origin.spriteBatch.Draw(_origin._game.TypeTexture[myTurret.getType()], new Rectangle(10 + ((int)myTurret._position.X * _sizeCase), 10 + ((int)myTurret._position.Y * _sizeCase), _sizeCase, _sizeCase), Frame, _origin._game.LevelColor[myTurret.getNodeLevel()]);
                        if (myTurret.getType() == EType.STRENGHT || myTurret.getType() == EType.SPEED || myTurret.getType() == EType.SHOOTER || myTurret.getType() == EType.GENERATOR)
                            _origin.spriteBatch.Draw(_origin._game.LevelTexture[myTurret.getTowerLevel()], new Rectangle(10 + ((int)myTurret._position.X * _sizeCase), 10 + ((int)myTurret._position.Y * _sizeCase), _sizeCase, _sizeCase), _origin._game.LevelColor[myTurret.getNodeLevel()]);
                    }
                }
                _origin.spriteBatch.DrawString(Font, _scenes.ElementAt<scene>(_current).Description, new Vector2(_position[3].X, _position[3].Y), Color.White);

                Vector2 pos = new Vector2();
                foreach (Mob.Mob myMob in cur._mobs)
                {
                    pos.X = 10 + (_sizeCase * (int)myMob.MobPos.X);
                    pos.Y = 10 + (_sizeCase * (int)myMob.MobPos.Y);
                    _origin.spriteBatch.Draw(_origin._game.MobTexture[(Mob.EMobType)myMob.GetMobType()], new Rectangle((int)myMob.MobPos.X + 20, (int)myMob.MobPos.Y + 20, _sizeCase - 20, _sizeCase - 20), Frame, Color.White);
                }
            }

            _origin.spriteBatch.Draw(Button, _position[0], Color.White);
            _origin.spriteBatch.DrawString(Font, "Menu", new Vector2(_position[0].X + (_position[0].Width / 4), (_position[0].Y + (_position[0].Height / 3))), Color.White);

            if (_current > 0)
            {
                _origin.spriteBatch.Draw(Button, _position[1], Color.White);
                _origin.spriteBatch.DrawString(Font, "Prev", new Vector2(_position[1].X + (_position[1].Width / 4), (_position[1].Y + (_position[1].Height / 3))), Color.White);
            }

            if ((_current + 1) < _scenes.Count)
            {
                _origin.spriteBatch.Draw(Button, _position[2], Color.White);
                _origin.spriteBatch.DrawString(Font, "Next", new Vector2(_position[2].X + (_position[2].Width / 4), (_position[2].Y + (_position[2].Height / 3))), Color.White);
            }
        }
    }
}
