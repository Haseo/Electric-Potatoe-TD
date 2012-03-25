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
    class Game_End
    {
        Game1 _origin;
        Texture2D LogoWin;
        Texture2D LogoLoose;
        Texture2D Button;
        SpriteFont Font;
        Rectangle[] _position;

        bool _victory;
        int _score;

        public Game_End(Game1 game)
        {
            _origin = game;
            _victory = false;
            _score = 0;
        }

        public void setVictory(bool value)
        {
            _victory = value;
        }

        public void setScore(int value)
        {
            _score = value;
        }

        public void Initialize()
        {
            _position = new Rectangle[]
             {  new Rectangle(_origin.graphics.PreferredBackBufferWidth / 3, _origin.graphics.PreferredBackBufferHeight / 12, 124, 124),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth / 2, _origin.graphics.PreferredBackBufferHeight * 6 / 8, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth / 4, _origin.graphics.PreferredBackBufferHeight * 4 / 8, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth / 4, _origin.graphics.PreferredBackBufferHeight * 5 / 8, 180, 90),
             };
        }

        public void LoadContent()
        {
            LogoWin = _origin.Content.Load<Texture2D>("ReactorH");
            LogoLoose = _origin.Content.Load<Texture2D>("ReactorD");
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

                        if ((PositionTouch.X >= _position[1].X && PositionTouch.X <= (_position[1].X + _position[1].Width)) &&
                            (PositionTouch.Y >= _position[1].Y && PositionTouch.Y <= (_position[1].Y + _position[1].Height)))
                        {
                            _origin.change_statut(Game1.Game_Statut.Menu);
                        }
                    }
                }
            }
        }

        public void draw()
        {
            _origin.spriteBatch.Draw(Button, _position[1], Color.White);
            _origin.spriteBatch.DrawString(Font, "Back", new Vector2(_position[1].X + (_position[1].Width / 3), (_position[1].Y + (_position[1].Height / 3))), Color.Black);
            if (_victory == true)
            {
                _origin.spriteBatch.Draw(LogoWin, _position[0], Color.White);
                draw_victory();
            }
            else
            {
                _origin.spriteBatch.Draw(LogoLoose, _position[0], Color.White);
                draw_lose();
            }
        }

        public void draw_victory()
        {
            _origin.spriteBatch.DrawString(Font, "You Win The Game", new Vector2(_position[2].X, _position[2].Y), Color.Black);
            _origin.spriteBatch.DrawString(Font, "Your score is : " + _score.ToString(), new Vector2(_position[3].X, _position[3].Y), Color.Black);
        }

        public void draw_lose()
        {
            _origin.spriteBatch.DrawString(Font, "You Lose The Game", new Vector2(_position[2].X, _position[2].Y), Color.Black);
        }
    }
}
