using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.Http;
using System.Reflection;

namespace MonoDungeonDiscovery
{
    public class MonoDungeonDiscovery : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static int score = 0;

        public static double fps;
        GameWindow otherWindow;
        public MonoDungeonDiscovery()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.HardwareModeSwitch = false;

            Renderer.Resize(Window.ClientBounds.Width, Window.ClientBounds.Height, true);

            Input.Initialize();
            
            base.Initialize();

            Renderer.Initialize(this);

            Graphics.Initialize(this);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            fps = (int)Math.Floor(1f/gameTime.ElapsedGameTime.TotalSeconds);


            Input.UpdateInputs();
            MetaOptions.Update(this);
            Renderer.Update(this);
            Graphics.Update(this);
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            //if (Input.GetInput(Keys.F12)) 
            //    if (Renderer.isFullscreen)
            //        Renderer.Resize(1280, 720, false);
            //    else
            //        Renderer.Resize(Window.ClientBounds.Width, Window.ClientBounds.Height, true);

            // TODO: Add your update logic here
            if (Input.MouseButtonReleased(MouseButtons.Left)) score++;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.Draw(this);

            base.Draw(gameTime);
        }

        public void CloseGame()
        {
            Exit();
        }
    }
}
