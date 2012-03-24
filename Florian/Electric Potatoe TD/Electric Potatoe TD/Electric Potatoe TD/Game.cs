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
    class Game
    {
        public enum EMap
        {
            BACKGROUND = 0,
            CANYON = 1,
            CENTRAL = 2,
        };

        Game1 _origin;
        Texture2D Menu;
        Texture2D RageMetter_top;
        Texture2D RageMetter_mid;
        Texture2D RageMetter_bot;
        SpriteFont RageMetter_font;
        Rectangle[] _position;

        int RageMetter;
        int RageMetter_flag;

        // Map
        EMap[,] map;
        int mapX, mapY;
        Vector2 pos_map;
        int size_case;
        Texture2D myTexture;

        bool _zoom;

        public Game(Game1 game)
        {
            _origin = game;
            RageMetter = 0;
            RageMetter_flag = 0;
            _zoom = false;
        }


        public void Oriented_changed()
        {
            if (RageMetter < 100)
                RageMetter = (RageMetter + 3) % 100;
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
             };
        }

        public void LoadContent()
        {
            Menu = _origin.Content.Load<Texture2D>("Menu");
            RageMetter_top = _origin.Content.Load<Texture2D>("RageMeterHigh");
            RageMetter_mid = _origin.Content.Load<Texture2D>("RageMeterMiddle");
            RageMetter_bot = _origin.Content.Load<Texture2D>("RageMeterLow");
            RageMetter_font = _origin.Content.Load<SpriteFont>("RageMetter");
            myTexture = _origin.Content.Load<Texture2D>("grass");
        }

        public void UnloadContent()
        {
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
                    if (touches[0].State == TouchLocationState.Pressed)
                    {
                        Vector2 PositionTouch = touches[0].Position;

                        if ((PositionTouch.X >= _position[0].X && PositionTouch.X <= (_position[0].X + _position[0].Width)) &&
                            (PositionTouch.Y >= _position[0].Y && PositionTouch.Y <= (_position[0].Y + _position[0].Height)))
                        {
                            _origin.change_statut(Game1.Game_Statut.Menu_Ig);
                        }
                    }
                }
            }
        }

        public void draw()
        {
            draw_map();
            draw_content();
            
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
                        case EMap.BACKGROUND: _origin.spriteBatch.Draw(myTexture, new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.Green); break;
                        case EMap.CANYON: _origin.spriteBatch.Draw(myTexture, new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.Red); break;
                        case EMap.CENTRAL: _origin.spriteBatch.Draw(myTexture, new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.Blue); break;
                    }
                }
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
        }

        public void Restart()
        {
            RageMetter = 0;
            RageMetter_flag = 0;
            _zoom = false;
            mapFiller();
        }

        public void mapFiller()
        {
            this.mapX = 10;
            this.mapY = 5;
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
            this.map = new EMap[,]
            {
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CENTRAL, EMap.CENTRAL, EMap.CENTRAL},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CENTRAL, EMap.CENTRAL, EMap.CENTRAL},
            };
        }

    }
}
