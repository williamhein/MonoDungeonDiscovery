using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace MonoDungeonDiscovery
{
    public class Graphics
    {
        public static List<Texture2D> sprites;
        public static List<SpriteText> spriteTexts;

        public static SpriteFont defaultFont;
        public static void Initialize(MonoDungeonDiscovery parent)
        {

            defaultFont = parent.Content.Load<SpriteFont>("Hope");

            sprites = new List<Texture2D>();
            
            sprites.Add(Renderer.LoadTextureStream(parent.GraphicsDevice, "135-deathhouseplayer"));

            spriteTexts = new List<SpriteText>();

        }
        public static void Update(MonoDungeonDiscovery parent)
        {
            spriteTexts.Clear();

            spriteTexts.Add(new SpriteText(Graphics.defaultFont, "FPS: " + MonoDungeonDiscovery.fps, new Vector2(100, 200), Color.White));
            spriteTexts.Add(new SpriteText(defaultFont, "Hope" + MonoDungeonDiscovery.score, new Vector2(100, 100), Color.White));
        }
        public static void Draw(MonoDungeonDiscovery parent)
        {
            parent.GraphicsDevice.Clear(Color.Black);

            parent.GraphicsDevice.Clear(Color.CornflowerBlue);

            MonoDungeonDiscovery._spriteBatch.Begin();

            foreach (Texture2D sprite in sprites)
            {
                MonoDungeonDiscovery._spriteBatch.Draw(sprite, sprite.Bounds,Color.White);
            }

            foreach (SpriteText sprite in spriteTexts)
            {
                MonoDungeonDiscovery._spriteBatch.DrawString(sprite.font, sprite.text, sprite.location, sprite.color);
            }

            MonoDungeonDiscovery._spriteBatch.End();
        }
    }
    public class SpriteText
    {
        public SpriteFont font;
        public string text;
        public Vector2 location;
        public Color color;
        public SpriteText(SpriteFont font, string text) {
            this.text = text;
            this.font = font;
        }
        public SpriteText(SpriteFont font, string text, Vector2 location, Color color)
        {
            this.text = text;
            this.font = font;
            this.location = location;
            this.color = color;
        }
    }
}
