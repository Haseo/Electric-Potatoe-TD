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
        Game1 _origin;
        Texture2D Logo;
        Texture2D Button;
        SpriteFont Font;
        Rectangle[] _position;

        public Tutorial(Game1 game)
        {
            _origin = game;
        }

        public void Initialize()
        {
            _position = new Rectangle[]
             {  new Rectangle(_origin.graphics.PreferredBackBufferWidth / 3, _origin.graphics.PreferredBackBufferHeight / 12, 248, 248),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 2 / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 3 / 4, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
             };
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

                        if ((PositionTouch.X >= _position[1].X && PositionTouch.X <= (_position[1].X + _position[1].Width)) &&
                            (PositionTouch.Y >= _position[1].Y && PositionTouch.Y <= (_position[1].Y + _position[1].Height)))
                        {
                            _origin.change_statut(Game1.Game_Statut.Menu);
                        }
                        if ((PositionTouch.X >= _position[2].X && PositionTouch.X <= (_position[2].X + _position[2].Width)) &&
                            (PositionTouch.Y >= _position[2].Y && PositionTouch.Y <= (_position[2].Y + _position[2].Height)))
                        {
                            _origin.End_Game(true, 4242);
                        }
                        if ((PositionTouch.X >= _position[3].X && PositionTouch.X <= (_position[3].X + _position[3].Width)) &&
                            (PositionTouch.Y >= _position[3].Y && PositionTouch.Y <= (_position[3].Y + _position[3].Height)))
                        {
                            _origin.End_Game(false, 0);
                        }
                    }
                }
            }
        }

        public void draw()
        {
            _origin.spriteBatch.Draw(Logo, _position[0], Color.White);
            _origin.spriteBatch.Draw(Button, _position[1], Color.White);
            _origin.spriteBatch.DrawString(Font, "Back", new Vector2(_position[1].X + (_position[1].Width / 3), (_position[1].Y + (_position[1].Height / 3))), Color.Black);

            _origin.spriteBatch.Draw(Button, _position[2], Color.White);
            _origin.spriteBatch.DrawString(Font, "Win", new Vector2(_position[2].X + (_position[2].Width / 3), (_position[2].Y + (_position[2].Height / 3))), Color.Black);

            _origin.spriteBatch.Draw(Button, _position[3], Color.White);
            _origin.spriteBatch.DrawString(Font, "Lose", new Vector2(_position[3].X + (_position[3].Width / 3), (_position[3].Y + (_position[3].Height / 3))), Color.Black);
        }
    }
}
