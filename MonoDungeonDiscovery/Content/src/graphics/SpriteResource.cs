using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System.IO;

namespace MonoDungeonDiscovery
{
    public class SpriteResource
    {
        enum SpriteType
        {
            Texture,
            Font
        }

        public static Dictionary<string, SpriteResource> SpriteResources;
        public static void Initialize(MonoDungeonDiscovery parent)
        {
            SpriteResources = new Dictionary<string, SpriteResource>();

            SpriteResources["deathhouse"] = new SpriteResource(Renderer.LoadTextureStream(parent.GraphicsDevice, "135-deathhouseplayer"));
        }
        public SpriteFont font;
        public Texture2D texture;
        public SpriteResource(SpriteFont font)
        {
            this.font = font;
        }
        public SpriteResource(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
