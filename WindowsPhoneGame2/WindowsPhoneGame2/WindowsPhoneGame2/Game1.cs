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

namespace Tetris
{
    enum Tetris_color
    {
        TWhite,
        TBlack,
        TRed,
        TBlue,
        TCyan,
        TGreen,
        TYellow,
        TOrange,
        TMagenta,
        TBrun,
    };

    public enum Statut
    {
        Menu = 0,
        Game = 1,
        Menu_IG = 2,
        Help = 3,
        Credit = 4
    };

    struct using_color
    {
        Tetris_color _color;
        Color _value;
        public using_color(Tetris_color color, Color value)
        {
            _color = color;
            _value = value;
        }
    }
    /* Largueur window : graphics.PreferredBackBufferWidth
     * Hauteur window : graphics.PreferredBackBufferHeight
     * Ces donnees se rapportent a la forme suivante :
     * 
     * |------|
     * |      |
     * |------|
     * 
     * (et pas la forme intuitive) :
     * 
     * |----|
     * |    |
     * |    |
     * |----|
     */

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 spritePosition = Vector2.Zero; 
        Texture2D myTexture;
        SpriteFont font1;

        Statut _statut;
        Menu _menu;
        MenuIg _menuig;
        Game _game;
        Help _help;
        Credit _credit;

      //  String _test;


        public Game1()
        {
      //      _test = "";
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            graphics.SupportedOrientations = DisplayOrientation.Portrait |
                                     DisplayOrientation.LandscapeLeft |
                                     DisplayOrientation.LandscapeRight;
         //   graphics.SupportedOrientations = DisplayOrientation.Portrait;
            //graphics.PreferredBackBufferWidth = 480;
            //graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            _menu = new Menu(this);
            _menuig = new MenuIg(this);
            _game = new Game(this);
            _help = new Help(this);
            _credit = new Credit(this);

            _statut = Statut.Menu;
            //  this.Window.CurrentOrientation = DisplayOrientation.Portrait;
       //     this.Window.Title = "Tetris";
        //    this.Window.OrientationChanged += new EventHandler<EventArgs>(OnOrientationChanged);
        }

        public Statut getStatut()
        {
            return (_statut);
        }

        public void setStatut(Statut new_statut, bool restart)
        {
            _statut = new_statut;
            if (restart == true)
            {
                switch (_statut)
                {
                    case Statut.Menu:
                        _menu.Restart();
                        break;
                    case Statut.Game:
                        _game.Restart();
                        break;
                    case Statut.Menu_IG:
                        _menuig.Restart();
                        break;
                    case Statut.Help:
                        _help.Restart();
                        break;
                    case Statut.Credit:
                        _credit.Restart();
                        break;
                }
            }
        }

        protected override void Initialize()
        {
            Vector2 spriteSpeed = new Vector2(0f, 0f);
            _menu.Initialize();
            _menuig.Initialize();
            _game.Initialize();
            _help.Initialize();
            _credit.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myTexture = base.Content.Load<Texture2D>("Cadre");

            font1 = base.Content.Load<SpriteFont>("Font1");

            _menu.LoadContent();
            _menuig.LoadContent();
            _game.LoadContent();
            _help.LoadContent();
            _credit.LoadContent();
        }

        protected override void UnloadContent()
        {
            _menu.UnloadContent();
            _menuig.UnloadContent();
            _game.UnloadContent();
            _help.UnloadContent();
            _credit.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            switch (_statut)
            {
                case Statut.Menu:
                    _menu.Update(gameTime, this.Window.CurrentOrientation);
                    break;
                case Statut.Game:
                    _game.Update(gameTime, this.Window.CurrentOrientation);
                    break;
                case Statut.Menu_IG:
                    _menuig.Update(gameTime, this.Window.CurrentOrientation);
                    break;
                case Statut.Help:
                    _help.Update(gameTime, this.Window.CurrentOrientation);
                    break;
                case Statut.Credit:
                    _credit.Update(gameTime, this.Window.CurrentOrientation);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);
            spriteBatch.Begin();
            /*
            spritePosition.X = 0.0f;
            spritePosition.Y = 50.0f;
            
            spriteBatch.Draw(myTexture, spritePosition, Color.LightGreen);
        spritePosition.X = 300.0f;
            spriteBatch.DrawString(font1, "Format = " + this.Window.CurrentOrientation, spritePosition, Color.LightGreen);

            spritePosition.Y = 150.0f;
            spritePosition.X = 50.0f;
            spriteBatch.DrawString(font1, "ClientBounds = " + this.Window.ClientBounds, spritePosition, Color.LightGreen);
            */
     //       spritePosition.Y = 220.0f;
       //     spritePosition.X = 50.0f;
       //     spriteBatch.DrawString(font1, "Test = " + _test, spritePosition, Color.LightGreen);
 
            switch (_statut)
            {
                case Statut.Menu:
                    _menu.Draw(gameTime, spriteBatch, this.Window.CurrentOrientation);
                    break;
                case Statut.Game:
                    _game.Draw(gameTime, spriteBatch, this.Window.CurrentOrientation);
                    break;
                case Statut.Menu_IG:
                    _menuig.Draw(gameTime, spriteBatch, this.Window.CurrentOrientation);
                    break;
                case Statut.Help:
                    _help.Draw(gameTime, spriteBatch, this.Window.CurrentOrientation);
                    break;
                case Statut.Credit:
                    _credit.Draw(gameTime, spriteBatch, this.Window.CurrentOrientation);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
