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
    class TowerData
    {
        DataCenter _origin;
        Texture2D Logo;
        Texture2D Button;
        Texture2D[] Tower;
        SpriteFont Font;
        SpriteFont MenuIGFont;
        Rectangle[] _position;
        string[] _description;

        public TowerData(DataCenter game)
        {
            _origin = game;
        }

        public void Initialize()
        {
            _position = new Rectangle[]
             {
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 8, _origin._origin.graphics.PreferredBackBufferHeight * 20 / 128, 100, 60),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 8, _origin._origin.graphics.PreferredBackBufferHeight * 36 / 128, 100, 60),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 8, _origin._origin.graphics.PreferredBackBufferHeight * 55 / 128, 100, 60),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 8, _origin._origin.graphics.PreferredBackBufferHeight * 71 / 128, 100, 60),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 8, _origin._origin.graphics.PreferredBackBufferHeight * 87 / 128, 100, 60),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 3 / 8, _origin._origin.graphics.PreferredBackBufferHeight * 10 / 128, 100, 60),

                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 70, _origin._origin.graphics.PreferredBackBufferHeight * 20 / 128, 70, 50),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 70, _origin._origin.graphics.PreferredBackBufferHeight * 36 / 128, 50, 50),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 70, _origin._origin.graphics.PreferredBackBufferHeight * 55 / 128, 50, 50),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 70, _origin._origin.graphics.PreferredBackBufferHeight * 71 / 128, 50, 50),
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 1 / 70, _origin._origin.graphics.PreferredBackBufferHeight * 87 / 128, 50, 50),
   
                 new Rectangle(_origin._origin.graphics.PreferredBackBufferWidth * 3 / 8, _origin._origin.graphics.PreferredBackBufferHeight * 110 / 128, 120, 60),
             };
            _description = new string[6];
            _description[5] = "STRUCTURE";
            _description[0] = "Ici text";
            _description[1] = "Ici text";
            _description[2] = "Ici text";
            _description[3] = "Ici text";
            _description[4] = "Ici text";
        }

        public void LoadContent()
        {
            Logo = _origin._origin.Content.Load<Texture2D>("Logo");
            Button = _origin._origin.Content.Load<Texture2D>("Button");
            Font = _origin._origin.Content.Load<SpriteFont>("MenuFont");
            MenuIGFont = _origin._origin.Content.Load<SpriteFont>("MenuIG");
            Tower = new Texture2D[5];
            Tower[0] = _origin._origin.Content.Load<Texture2D>("ReactorA");
            Tower[1] = _origin._origin.Content.Load<Texture2D>("TowerFast");
            Tower[2] = _origin._origin.Content.Load<Texture2D>("TowerNormal");
            Tower[3] = _origin._origin.Content.Load<Texture2D>("TowerHeavy");
            Tower[4] = _origin._origin.Content.Load<Texture2D>("Node");
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

                        if ((PositionTouch.X >= _position[11].X && PositionTouch.X <= (_position[11].X + _position[11].Width)) &&
                            (PositionTouch.Y >= _position[11].Y && PositionTouch.Y <= (_position[11].Y + _position[11].Height)))
                        {
                            _origin.change_statut(DataCenter.DataCenter_statut.Main);
                        }
                    }
                }
            }
        }

        public void draw()
        {
            _origin._origin.spriteBatch.Draw(Tower[0], _position[6], Color.White);
            _origin._origin.spriteBatch.Draw(Tower[1], _position[7], new Rectangle(0, 0, 40, 40), Color.White);
            _origin._origin.spriteBatch.Draw(Tower[2], _position[8], new Rectangle(0, 0, 40, 40), Color.White);
            _origin._origin.spriteBatch.Draw(Tower[3], _position[9], new Rectangle(0, 0, 40, 40), Color.White);
            _origin._origin.spriteBatch.Draw(Tower[4], _position[10], new Rectangle(0, 0, 40, 40), Color.White);


            _origin._origin.spriteBatch.DrawString(MenuIGFont, _description[0], new Vector2(_position[0].X, _position[0].Y), Color.Black);
            _origin._origin.spriteBatch.DrawString(MenuIGFont, _description[1], new Vector2(_position[1].X, _position[1].Y), Color.Black);
            _origin._origin.spriteBatch.DrawString(MenuIGFont, _description[2], new Vector2(_position[2].X, _position[2].Y), Color.Black);
            _origin._origin.spriteBatch.DrawString(MenuIGFont, _description[3], new Vector2(_position[3].X, _position[3].Y), Color.Black);
            _origin._origin.spriteBatch.DrawString(MenuIGFont, _description[4], new Vector2(_position[4].X, _position[4].Y), Color.Black);
            _origin._origin.spriteBatch.DrawString(MenuIGFont, _description[5], new Vector2(_position[5].X, _position[5].Y), Color.Black);

            _origin._origin.spriteBatch.Draw(Button, _position[11], Color.White);
            _origin._origin.spriteBatch.DrawString(Font, "Back", new Vector2(_position[11].X + (_position[11].Width / 4), (_position[11].Y + (_position[11].Height / 3))), Color.Black);
        }
    }
}
