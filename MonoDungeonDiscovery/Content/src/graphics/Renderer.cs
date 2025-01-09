using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics.Tracing;
using System.IO;

namespace MonoDungeonDiscovery
{
    public class Renderer
    {

        public static int screenWidth = 1280;
        public static int screenHeight = 720;

        public static float unitsWide = 12;
        public static float unitsTall = 0;

        public static BlendState _blendColor;
        public static BlendState _blendAlpha;

        public static bool isFullscreen = false;

        public static void Initialize(MonoDungeonDiscovery parent)
        {
            
        }
        public static void Update(MonoDungeonDiscovery parent)
        {

        }
        public static void Resize(int x, int y, bool fs)
        {
            MonoDungeonDiscovery._graphics.IsFullScreen = fs;
            isFullscreen = fs;
            MonoDungeonDiscovery._graphics.PreferredBackBufferWidth = x;
            MonoDungeonDiscovery._graphics.PreferredBackBufferHeight = y;
            MonoDungeonDiscovery._graphics.ApplyChanges();
        }

        public static int GetWindowWidth()
        {
            return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        }

        public static int GetWindowHeight()
        {
            return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

        public static Texture2D LoadTextureStream(GraphicsDevice graphics, string loc)
        {
            Texture2D file = null;
            RenderTarget2D result = null;

            using (Stream titleStream = TitleContainer.OpenStream("Content/" + loc + ".png"))
            {
                file = Texture2D.FromStream(graphics, titleStream);
            }

            //Setup a render target to hold our final texture which will have premulitplied alpha values
            result = new RenderTarget2D(graphics, file.Width, file.Height);

            graphics.SetRenderTarget(result);
            graphics.Clear(Color.Black);

            //Multiply each color by the source alpha, and write in just the color values into the final texture
            if (_blendColor == null)
            {
                _blendColor = new BlendState();
                _blendColor.ColorWriteChannels = ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue;

                _blendColor.AlphaDestinationBlend = Blend.Zero;
                _blendColor.ColorDestinationBlend = Blend.Zero;

                _blendColor.AlphaSourceBlend = Blend.SourceAlpha;
                _blendColor.ColorSourceBlend = Blend.SourceAlpha;
            }

            SpriteBatch spriteBatch = new SpriteBatch(graphics);
            spriteBatch.Begin(SpriteSortMode.Immediate, _blendColor);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();

            //Now copy over the alpha values from the PNG source texture to the final one, without multiplying them
            if (_blendAlpha == null)
            {
                _blendAlpha = new BlendState();
                _blendAlpha.ColorWriteChannels = ColorWriteChannels.Alpha;

                _blendAlpha.AlphaDestinationBlend = Blend.Zero;
                _blendAlpha.ColorDestinationBlend = Blend.Zero;

                _blendAlpha.AlphaSourceBlend = Blend.One;
                _blendAlpha.ColorSourceBlend = Blend.One;
            }

            spriteBatch.Begin(SpriteSortMode.Immediate, _blendAlpha);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();

            //Release the GPU back to drawing to the screen
            graphics.SetRenderTarget(null);

            return result as Texture2D;
        }
    }
}
