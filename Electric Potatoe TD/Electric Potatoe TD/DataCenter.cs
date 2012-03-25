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
    class DataCenter
    {
        public enum DataCenter_statut
        {
            Bestiaire,
            TowerData,
            Main,
        };

        public Game1 _origin;
        Texture2D Logo;
        Texture2D Button;
        SpriteFont Font;
        Rectangle[] _position;
        DataCenter_statut _statut;
        Bestiaire _bestiaire;
        TowerData _towerdata;

        public DataCenter(Game1 game)
        {
            _origin = game;
            _bestiaire = new Bestiaire(this);
            _towerdata = new TowerData(this);
            _statut = DataCenter_statut.Main;
        }

        public void Restart()
        {
            _statut = DataCenter_statut.Main;
        }

        public void Initialize()
        {
            _position = new Rectangle[]
             {  new Rectangle(_origin.graphics.PreferredBackBufferWidth / 3, _origin.graphics.PreferredBackBufferHeight / 12, 248, 248),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 1 / 30, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 8 / 30, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
                new Rectangle(_origin.graphics.PreferredBackBufferWidth * 22 / 30, _origin.graphics.PreferredBackBufferHeight * 3 / 4, 180, 90),
             };
            _bestiaire.Initialize();
            _towerdata.Initialize();
        }

        public void LoadContent()
        {
            Logo = _origin.Content.Load<Texture2D>("Logo");
            Button = _origin.Content.Load<Texture2D>("Button");
            Font = _origin.Content.Load<SpriteFont>("MenuFont");
            _bestiaire.LoadContent();
            _towerdata.LoadContent();
        }

        public void UnloadContent()
        {
            _bestiaire.UnloadContent();
            _towerdata.UnloadContent();
        }

        public void change_statut(DataCenter_statut statut)
        {
            _statut = statut;
        }

        public void update()
        {
            switch (_statut)
            {
                case DataCenter_statut.Main :
                    update_Main();break;
                case DataCenter_statut.Bestiaire :
                    _bestiaire.update();break;
                case DataCenter_statut.TowerData :
                    _towerdata.update();break;
            }
        }

        public void update_Main()
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
                            _statut = DataCenter_statut.Bestiaire;
                        }
                        if ((PositionTouch.X >= _position[2].X && PositionTouch.X <= (_position[2].X + _position[2].Width)) &&
                            (PositionTouch.Y >= _position[2].Y && PositionTouch.Y <= (_position[2].Y + _position[2].Height)))
                        {
                            _statut = DataCenter_statut.TowerData;
                        }
                        if ((PositionTouch.X >= _position[3].X && PositionTouch.X <= (_position[3].X + _position[3].Width)) &&
                            (PositionTouch.Y >= _position[3].Y && PositionTouch.Y <= (_position[3].Y + _position[3].Height)))
                        {
                            _origin.change_statut(Game1.Game_Statut.Menu);
                        }
                    }
                }
            }
        }

        public void draw()
        {
            switch (_statut)
            {
                case DataCenter_statut.Main:
                    draw_Main(); break;
                case DataCenter_statut.Bestiaire:
                    _bestiaire.draw(); break;
                case DataCenter_statut.TowerData:
                    _towerdata.draw(); break;
            }
        }

        public void draw_Main()
        {
            _origin.spriteBatch.Draw(Logo, _position[0], Color.White);
            _origin.spriteBatch.Draw(Button, _position[1], Color.White);
            _origin.spriteBatch.DrawString(Font, "BESTIAIRE", new Vector2(_position[1].X + (_position[1].Width / 5), (_position[1].Y + (_position[1].Height / 3))), Color.White);
            _origin.spriteBatch.Draw(Button, _position[2], Color.White);
            _origin.spriteBatch.DrawString(Font, "TOURELLES", new Vector2(_position[2].X + (_position[2].Width / 5), (_position[2].Y + (_position[2].Height / 3))), Color.White);
            _origin.spriteBatch.Draw(Button, _position[3], Color.White);
            _origin.spriteBatch.DrawString(Font, "RETOUR", new Vector2(_position[3].X + (_position[3].Width / 4), (_position[3].Y + (_position[3].Height / 3))), Color.White);
       }
    }
}
