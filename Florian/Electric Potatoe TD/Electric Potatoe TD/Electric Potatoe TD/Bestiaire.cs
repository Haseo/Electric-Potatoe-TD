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
    class Bestiaire
    {
        Game1 _origin;
        Texture2D Logo;
        Texture2D Start;
        Texture2D Quit;
        Rectangle[] _position;

        public Bestiaire(Game1 game)
        {
            _origin = game;
        }

        public void Initialize()
        {
            _position = new Rectangle[]
             {  new Rectangle(_origin.graphics.PreferredBackBufferWidth / 3, _origin.graphics.PreferredBackBufferHeight / 12, 248, 248),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 1 / 3, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 100, 60),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 2 / 3, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 100, 60),
             };
        }

        public void LoadContent()
        {
            Logo = _origin.Content.Load<Texture2D>("Logo");
            Start = _origin.Content.Load<Texture2D>("Start");
            Quit = _origin.Content.Load<Texture2D>("Quit");
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

                        if ((PositionTouch.X >= _position[1].X && PositionTouch.X <= (_position[1].X + _position[1].Width)) &&
                            (PositionTouch.Y >= _position[1].Y && PositionTouch.Y <= (_position[1].Y + _position[1].Height)))
                        {
                            _origin.Restart_game();
                            _origin.change_statut(Game1.Game_Statut.Game);
                        }
                        if ((PositionTouch.X >= _position[2].X && PositionTouch.X <= (_position[2].X + _position[2].Width)) &&
                            (PositionTouch.Y >= _position[2].Y && PositionTouch.Y <= (_position[2].Y + _position[2].Height)))
                        {
                            _origin.Exit();
                        }
                    }
                }
            }
        }

        public void draw()
        {
            _origin.spriteBatch.Draw(Logo, _position[0], Color.White);
            _origin.spriteBatch.Draw(Start, _position[1], Color.White);
            _origin.spriteBatch.Draw(Quit, _position[2], Color.White);
        }
    }
}
