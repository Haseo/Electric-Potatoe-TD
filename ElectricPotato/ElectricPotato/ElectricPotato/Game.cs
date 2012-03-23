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

namespace ElectricPotatoe
{
    public enum EMap
    {
        BACKGROUND = 0,
        CANYON = 1,
        CENTRAL = 2,
    };

    public class Game : Microsoft.Xna.Framework.Game
    {
        EMap[,] map;
        int mapX, mapY;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D myTexture;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            InactiveSleepTime = TimeSpan.FromSeconds(1);
            mapFiller();
        }

        public void  mapFiller()
        {
            this.mapX = 80;
            this.mapY = 5;
            this.map = new EMap[,]
            {
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.BACKGROUND, EMap.CANYON, EMap.BACKGROUND},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CENTRAL, EMap.CENTRAL, EMap.CENTRAL},
                {EMap.BACKGROUND, EMap.BACKGROUND, EMap.CENTRAL, EMap.CENTRAL, EMap.CENTRAL},
            };
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myTexture = base.Content.Load<Texture2D>("grass");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            int x = 0, y = 0;
            spriteBatch.Begin();
            for (x = 0; x < mapX; x++)
            {
                for (y = 0; y < mapY; y++)
                {
                    switch(this.map[x, y])
                    {
                        case EMap.BACKGROUND :  spriteBatch.Draw(myTexture, new Rectangle(10 * x, 10 * y, 10, 10), Color.Green);    break;
                        case EMap.CANYON :      spriteBatch.Draw(myTexture, new Rectangle(10 * x, 10 * y, 10, 10), Color.Red);      break;
                        case EMap.CENTRAL :     spriteBatch.Draw(myTexture, new Rectangle(10 * x, 10 * y, 10, 10), Color.Blue);     break;
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
