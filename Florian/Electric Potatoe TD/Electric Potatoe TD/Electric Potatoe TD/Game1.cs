using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Phone.Controls;

namespace Electric_Potatoe_TD
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum Game_Statut
        {
            Menu,
            Game,
            Menu_Ig,
        };

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Game_Statut _statut;
        private Menu _menu;
        private Game _game;
        private Menu_IG _menuIg;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _statut = Game_Statut.Menu;
            _menu = new Menu(this);
            _game = new Game(this);
            _menuIg = new Menu_IG(this);
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            this.Window.OrientationChanged += new EventHandler<EventArgs>(this.Oriented_changed);
        }

        private void Oriented_changed(object sender, EventArgs e)
        {
            switch (_statut)
            {
                case Game_Statut.Menu:
                    break;
                case Game_Statut.Game:
                    _game.Oriented_changed(); break;
                case Game_Statut.Menu_Ig:
                    break;
            }
        }

        public int getScore()
        {
            return (_game.getScore());
        }

        public void change_statut(Game_Statut statut)
        {
            _statut = statut;
        }

        public void Restart_game()
        {
            _game.Restart();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _menu.Initialize();
            _game.Initialize();
            _menuIg.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _menu.LoadContent();
            _menu.LoadContent();
            _game.LoadContent();
            _menuIg.LoadContent();
        }

        protected override void UnloadContent()
        {
            _menu.UnloadContent();
            _menu.UnloadContent();
            _game.UnloadContent();
            _menuIg.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (_statut)
            {
                case Game_Statut.Menu:
                    _menu.update(); break;
                case Game_Statut.Game:
                    _game.update(); break;
                case Game_Statut.Menu_Ig:
                    _menuIg.update(); break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            switch (_statut)
            {
                case Game_Statut.Menu:
                    _menu.draw(); break;
                case Game_Statut.Game:
                    _game.draw(); break;
                case Game_Statut.Menu_Ig:
                    _menuIg.draw(); break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
