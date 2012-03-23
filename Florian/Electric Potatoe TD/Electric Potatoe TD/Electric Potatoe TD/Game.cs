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
        Game1 _origin;
        Texture2D Menu;
        Rectangle[] _position;

        public Game(Game1 game)
        {
            _origin = game;
        }

        public void Initialize()
        {
            _position = new Rectangle[]
             {  new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, _origin.graphics.PreferredBackBufferHeight * 5 / 6, _origin.graphics.PreferredBackBufferWidth / 10, _origin.graphics.PreferredBackBufferHeight / 6),
             };
        }

        public void LoadContent()
        {
            Menu = _origin.Content.Load<Texture2D>("Menu");
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
                            _origin.change_statut(Game1.Game_Statut.Menu_Ig);
                        }
                    }
                }
            }
        }

        public void draw()
        {
            _origin.spriteBatch.Draw(Menu, _position[0], Color.White);
        }

        public void Restart()
        {

        }
    }
}
