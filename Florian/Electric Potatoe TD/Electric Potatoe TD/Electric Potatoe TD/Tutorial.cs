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
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "Les Bases:\n\nDans un \"Tower Defense\" vous devez survivre\nà des vagues successives d'ennemis\nen plaçant strategiquement des tours\nsur leur chemin.", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "Le principe d'ElectricPotatoe est un peu\nplus compliqué car tout est basé sur\nl'énergie électrique.", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "1) La centrale génère de l'électricité\n cela permet d'alimenter les tours.", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "2) Les tours doivent être posées\n sur des nodes.\nPour poser une node:\n Double-cliquez sur la map pour passer\nen mode construction.\n\n Glissez ensuite votre doigt depuis la centrale\nou la node d'origine jusqu'au point où vous\nvoulez placer votre node.\n\nLes nodes permettent aussi\nde générer un peu d'énergie.", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "3) Pour construire une tour\nplacez votre doigt deux secondes sur la node\npuis cliquez sur CRÉER TOUR.\n\nChoisissez ensuite quelle tour vous voulez\nplacer par une simple pression du doigt sur\nle nom de la tour.", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "4) Quand vous tuez des ennemis votre\ncentrale gagne en énergie.\n\nCela vous permet de placer de nouvelles\nnodes, de nouvelles tourelles\net de les alimenter.\nSi vous produisez plusieurs nodes/tourelles\nassurez-vous d'avoir assez d'énergie pour les\nalimenter. Si ce n'est pas le cas elles ne tirerons\npas.\n\nDe plus vous pouvez utiliser votre énergie\npour améliorer vos nodes et vos tours.", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "5) Améliorez vos défenses:\nAméliorer une node lui permet de générer un\npeu plus de d'énergie.\nPour améliorer vos nodes cliquez sur une node\ndeux secondes pour passer en mode\nconstruction puis cliquez sur \"AMÉLIORER\"\n\nUne node peux monter jusqu'au niveau 4,\nelle change de couleur à chaque fois.\n", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "6) Vous pouvez aussi améliorer vos tours !\n Cela augmente leur puissance ou bien\nleur cadence de tir.\nUne petite boule lumineuse se place sur\nle coté d'une tour quand elle\nest améliorée.\nVous pouvez en cumuler jusqu'à quatre.", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "7) Vous pouvez améliorer à la fois vos nodes\net vos tours.", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "8) Plus les tours sont proches de la centrale,\nplus elle est sont efficaces.\n\nAussi une tour trop éloignée ne tirera pas.\n", new Vector2(-1, -1)));
            _scenes.Add(new scene(new List<Node>(), new List<Mob.Mob>(), new EMap[1, 1], "Pour obtenir un bonus de production\nd'énergie, vous pouvez booster votre\ncentrale en agitant votre téléphone.", new Vector2(-1, -1)));
            _scenes[3]._nod.Add(new Node(1, 2, 10, 42, null));

            _scenes[4]._nod.Add(new Shooter(1, 2, 10, 42, null));

            _scenes[5]._nod.Add(new Shooter(2, 2, 10, 42, null));
            _scenes[5]._nod.Add(new Shooter(0, 2, 10, 42, null));
            _scenes[5]._nod.Add(new Shooter(0, 2, 10, 42, null));


            _scenes[6]._nod.Add(new Node(0, 0, 10, 42, null));
            _scenes[6]._nod.Add(new Node(1, 0, 10, 42, null));
            _scenes[6]._nod[1].levelUpNode();
            _scenes[6]._nod.Add(new Node(2, 0, 10, 42, null));
            _scenes[6]._nod[2].levelUpNode();
            _scenes[6]._nod[2].levelUpNode();

            _scenes[7]._nod.Add(new Shooter(0, 0, 10, 42, null));
            _scenes[7]._nod.Add(new Shooter(1, 0, 10, 42, null));
            _scenes[7]._nod[1].levelUpTower();
            _scenes[7]._nod.Add(new Shooter(2, 0, 10, 42, null));
            _scenes[7]._nod[2].levelUpTower();
            _scenes[7]._nod[2].levelUpTower();

            _scenes[8]._nod.Add(new Shooter(2, 0, 10, 42, null));
            _scenes[8]._nod[0].levelUpTower();
            _scenes[8]._nod[0].levelUpTower();
            _scenes[8]._nod[0].levelUpNode();
        }

        public void Initialize()
        {
            _position = new Rectangle[]
             {  new Rectangle(_origin.graphics.PreferredBackBufferWidth / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 2 / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 3 / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth / 2, _origin.graphics.PreferredBackBufferHeight * 1 / 20, 180, 90),
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
            _origin.spriteBatch.DrawString(Font, "MENU", new Vector2(_position[0].X + (_position[0].Width / 4), (_position[0].Y + (_position[0].Height / 3))), Color.White);

            if (_current > 0)
            {
                _origin.spriteBatch.Draw(Button, _position[1], Color.White);
                _origin.spriteBatch.DrawString(Font, "PREC.", new Vector2(_position[1].X + (_position[1].Width / 4), (_position[1].Y + (_position[1].Height / 3))), Color.White);
            }

            if ((_current + 1) < _scenes.Count)
            {
                _origin.spriteBatch.Draw(Button, _position[2], Color.White);
                _origin.spriteBatch.DrawString(Font, "SUIV.", new Vector2(_position[2].X + (_position[2].Width / 4), (_position[2].Y + (_position[2].Height / 3))), Color.White);
            }
        }
    }
}
