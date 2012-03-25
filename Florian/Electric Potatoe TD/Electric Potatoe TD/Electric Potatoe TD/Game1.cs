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
            Tutorial,
            DataCenter,
        };


        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Game_Statut _statut;
        private Menu _menu;
        private Game _game;
        private Tutorial _tuto;
        private DataCenter _datacenter;
        private Menu_IG _menuIg;
        private int CurrentFrame;
        private int FrameStart;
        private int FPS;
        private int SheetSize;
        private int FrameCounter;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _statut = Game_Statut.Menu;
            _menu = new Menu(this);
            _game = new Game(this);
            _menuIg = new Menu_IG(this);
            _tuto = new Tutorial(this);
            _datacenter = new DataCenter(this);
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            FrameStart = 0;
            FPS = 30;
            SheetSize = 5;
            FrameCounter = 0;
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
                case Game_Statut.DataCenter:
                    break;
                case Game_Statut.Tutorial:
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
            _tuto.Initialize();
            _datacenter.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _menu.LoadContent();
            _game.LoadContent();
            _menuIg.LoadContent();
            _tuto.LoadContent();
            _datacenter.LoadContent();
        }

        protected override void UnloadContent()
        {
            _menu.UnloadContent();
            _game.UnloadContent();
            _menuIg.UnloadContent();
            _tuto.UnloadContent();
            _datacenter.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            FrameStart += gameTime.ElapsedGameTime.Milliseconds;
            if (FrameStart > FPS)
            {
                ++FrameCounter;
                FrameStart -= FPS;
                if (FrameCounter == 4)
                {
                    ++CurrentFrame;
                    FrameCounter = 0;
                }
                if (CurrentFrame >= SheetSize)
                    CurrentFrame = 0;
            }

            switch (_statut)
            {
                case Game_Statut.Menu:
                    _menu.update(); break;
                case Game_Statut.Game:
                    _game.update(gameTime); break;
                case Game_Statut.Menu_Ig:
                    _menuIg.update(); break;
                case Game_Statut.Tutorial:
                    _tuto.update(); break;
                case Game_Statut.DataCenter:
                    _datacenter.update(); break;
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
                    _game.draw(FrameStart, FPS, CurrentFrame, SheetSize); break;
                case Game_Statut.Menu_Ig:
                    _menuIg.draw(); break;
                case Game_Statut.Tutorial:
                    _tuto.draw(); break;
                case Game_Statut.DataCenter:
                    _datacenter.draw(); break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
