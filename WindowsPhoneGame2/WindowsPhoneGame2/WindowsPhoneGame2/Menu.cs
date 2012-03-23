using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
    class Menu
    {
        Game1 _origin;
        SpriteFont Font;
        Texture2D TextureCadre;
        Texture2D Tetris_Logo;
        Rectangle[] _landscape;
        Rectangle[] _portrait;
        int _X;
        int _Y;

        public Menu(Game1 origin)
         {
             _origin = origin;
             _X = (_origin.graphics.PreferredBackBufferWidth / 2);
             _Y = (_origin.graphics.PreferredBackBufferHeight / 2);
       }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            Font = _origin.Content.Load<SpriteFont>("MenuFont");
            TextureCadre = _origin.Content.Load<Texture2D>("Cadre");
            Tetris_Logo = _origin.Content.Load<Texture2D>("Tetris Logo");
            _landscape = new Rectangle[]
             {  new Rectangle(_X - (Tetris_Logo.Width / 6), (_Y / 3) - (TextureCadre.Height / 4) - 20, _X / 2, _Y / 5 * 2),
                new Rectangle(_X - (_X / 2) - (TextureCadre.Width / 2), _Y - (TextureCadre.Height / 2), _X / 2, _Y / 5 * 2),
                new Rectangle(_X + (_X / 2) - (TextureCadre.Width / 2), _Y - (TextureCadre.Height / 2), _X / 2, _Y / 5 * 2),
                new Rectangle(_X - (_X / 2) - (TextureCadre.Width / 2), _Y  + (_Y / 2) - (TextureCadre.Height / 4), _X / 2, _Y / 5 * 2),
                new Rectangle(_X + (_X / 2) - (TextureCadre.Width / 2), _Y  + (_Y / 2) - (TextureCadre.Height / 4), _X / 2, _Y / 5 * 2)};
            // A Configurer :: 
            _portrait = new Rectangle[]
             {  new Rectangle(_Y - (Tetris_Logo.Width / 6), (_X / 4) - (TextureCadre.Height / 4) - 20, _X / 2, _Y / 5 * 2),
                new Rectangle(_Y - (TextureCadre.Width / 2), _X - (_X * 3 / 10) - (TextureCadre.Height / 2), _X / 2, _Y / 5 * 2),
                new Rectangle(_Y - (TextureCadre.Width / 2), _X - (TextureCadre.Height / 2), _X / 2, _Y / 5 * 2),
                new Rectangle(_Y - (TextureCadre.Width / 2), _X + (_X * 3 / 10) - (TextureCadre.Height / 2), _X / 2, _Y / 5 * 2),
                new Rectangle(_Y - (TextureCadre.Width / 2), _X + (_X * 6 / 10) - (TextureCadre.Height / 2), _X / 2, _Y / 5 * 2)};
 
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime, DisplayOrientation orientation)
        {
            switch (orientation)
            {
                case DisplayOrientation.LandscapeLeft:
                    Update_Content(_landscape);
                    break;
                case DisplayOrientation.LandscapeRight:
                    Update_Content(_landscape);
                    break;
                case DisplayOrientation.Portrait:
                    Update_Content(_portrait);
                    break;
                case DisplayOrientation.Default:
                    Update_Content(_portrait);
                    break;
            }
        }

        /* Gestion Statut
         * touches[0].State 
         * TouchLocationState.Invalid
         * TouchLocationState.Moved
         * TouchLocationState.Pressed
         * TouchLocationState.Released
         */

        public void Update_Content(Rectangle[] array)
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

                        if ((PositionTouch.X >= array[1].X && PositionTouch.X <= (array[1].X + array[1].Width)) &&
                            (PositionTouch.Y >= array[1].Y && PositionTouch.Y <= (array[1].Y + array[1].Height)))
                        {
                            _origin.setStatut(Statut.Game, true);
                        }
                        if ((PositionTouch.X >= array[2].X && PositionTouch.X <= (array[2].X + array[2].Width)) &&
                            (PositionTouch.Y >= array[2].Y && PositionTouch.Y <= (array[2].Y + array[2].Height)))
                        {
                            _origin.setStatut(Statut.Help, false);
                        }
                        if ((PositionTouch.X >= array[3].X && PositionTouch.X <= (array[3].X + array[3].Width)) &&
                            (PositionTouch.Y >= array[3].Y && PositionTouch.Y <= (array[3].Y + array[3].Height)))
                        {
                            _origin.setStatut(Statut.Credit, false);
                        }
                        if ((PositionTouch.X >= array[4].X && PositionTouch.X <= (array[4].X + array[4].Width)) &&
                            (PositionTouch.Y >= array[4].Y && PositionTouch.Y <= (array[4].Y + array[4].Height)))
                        {
                            _origin.Exit();
                        }
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, DisplayOrientation orientation)
        {
            switch (orientation)
            {
                case DisplayOrientation.LandscapeLeft:
                    Draw_Content(spriteBatch, _landscape);
                    break;
                case DisplayOrientation.LandscapeRight:
                    Draw_Content(spriteBatch, _landscape);
                    break;
                case DisplayOrientation.Portrait:
                    Draw_Content(spriteBatch, _portrait);
                    break;
                case DisplayOrientation.Default:
                    Draw_Content(spriteBatch, _portrait);
                    break;
            }
        }

        public void Draw_Content(SpriteBatch spriteBatch, Rectangle[] array)
        {
            spriteBatch.Draw(Tetris_Logo, array[0], Color.White);

            spriteBatch.Draw(TextureCadre, array[1], Color.White);
            spriteBatch.Draw(TextureCadre, array[2], Color.White);
            spriteBatch.Draw(TextureCadre, array[3], Color.White);
            spriteBatch.Draw(TextureCadre, array[4], Color.White);
            spriteBatch.DrawString(Font, "Start", new Vector2(array[1].X + (array[1].Width / 3), array[1].Y + (array[1].Height / 3)), Color.DarkRed);
            spriteBatch.DrawString(Font, "Help", new Vector2(array[2].X + (array[2].Width / 3), array[2].Y + (array[2].Height / 3)), Color.DarkRed);
            spriteBatch.DrawString(Font, "Credits", new Vector2(array[3].X + (array[3].Width / 3), array[3].Y + (array[3].Height / 3)), Color.DarkRed);
            spriteBatch.DrawString(Font, "Quit", new Vector2(array[4].X + (array[4].Width / 3), array[4].Y + (array[4].Height / 3)), Color.DarkRed);
       }

        public void Restart()
        {

        }
    }
}
