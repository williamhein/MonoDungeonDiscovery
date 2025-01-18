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

            Input.Initialize();

            Renderer.Resize(Window.ClientBounds.Width, Window.ClientBounds.Height, true);

            Renderer.Initialize(this);

            World.Initialize(this);

            base.Initialize();
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

            World.Update(this);

            if (Input.MouseButtonReleased(MouseButtons.Left)) score++;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            World.Draw(this);

            base.Draw(gameTime);
        }

        public void CloseGame()
        {
            Exit();
        }
    }
}
