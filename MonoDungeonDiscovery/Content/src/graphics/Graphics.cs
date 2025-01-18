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

            sprites.Add(SpriteResource.SpriteResources["deathhouse"].texture);

            spriteTexts = new List<SpriteText>();

        }
        public static void Update(MonoDungeonDiscovery parent)
        {
            
        }
        public static void Clear()
        {
            spriteTexts.Clear();
            sprites.Clear();
        }
        public static void Draw(MonoDungeonDiscovery parent)
        {
            spriteTexts.Clear();

            spriteTexts.Add(new SpriteText(Graphics.defaultFont, "FPS: " + MonoDungeonDiscovery.fps, new Vector2(100, 200), Color.White));
            //spriteTexts.Add(new SpriteText(defaultFont, "Hope" + MonoDungeonDiscovery.score, new Vector2(100, 100), Color.White));

            parent.GraphicsDevice.Clear(Color.Black);

            parent.GraphicsDevice.Clear(Color.CornflowerBlue);
            //start batch
            MonoDungeonDiscovery._spriteBatch.Begin();

            foreach (Texture2D sprite in sprites)
            {
                MonoDungeonDiscovery._spriteBatch.Draw(sprite, sprite.Bounds,Color.White);
            }

            foreach (SpriteText sprite in spriteTexts)
            {
                MonoDungeonDiscovery._spriteBatch.DrawString(sprite.font, sprite.text, sprite.location, sprite.color);
            }
            //end batch
            MonoDungeonDiscovery._spriteBatch.End();
        }
        public static void DrawGameObjects(MonoDungeonDiscovery parent)
        {
            parent.GraphicsDevice.Clear(Color.Black);

            parent.GraphicsDevice.Clear(Color.CornflowerBlue);

            World.gameObjects.Sort((p, q) => p.Z.CompareTo(q.Z));

            float pixelPerUnitNoZoom = Renderer.GetWindowWidth() / Renderer.unitsWide;
            float pixelPerUnit = Renderer.GetWindowWidth() / Renderer.unitsWide * Renderer.cameraZoom;
            //start batch
            MonoDungeonDiscovery._spriteBatch.Begin();

            foreach (GameObject gameObject in World.gameObjects)
            {
                float x = gameObject.Bounds.X, y = gameObject.Bounds.Y, width = gameObject.Bounds.Width, height = gameObject.Bounds.Height;
                if (
                    Renderer.cameraZoom * (x - Renderer.cameraX - width / 2) > Renderer.unitsWide / 2 ||
                    Renderer.cameraZoom * (x + width / 2 - Renderer.cameraX) < -Renderer.unitsWide / 2 ||
                    Renderer.cameraZoom * (y - height / 2 - Renderer.cameraY) > Renderer.unitsTall / 2 ||
                    Renderer.cameraZoom * (y + height / 2 - Renderer.cameraY) < -Renderer.unitsTall / 2
                ) continue;

                Texture2D texture = gameObject.Draw();
                Rectangle bounds = new Rectangle(
                     //(int)((Renderer.unitsWide / 2 - Renderer.cameraX + x - width / 2) * pixelPerUnit),
                     //(int)((Renderer.unitsTall / 2 - Renderer.cameraY + y - height / 2) * pixelPerUnit),

                    (int)((Renderer.unitsWide / 2 * pixelPerUnitNoZoom) + (-Renderer.cameraX + x - width / 2) * pixelPerUnit),// + (int)Renderer.cameraZoom,
                    (int)((Renderer.unitsTall / 2 * pixelPerUnitNoZoom) + (-Renderer.cameraY + y - height / 2) * pixelPerUnit),// + (int)Renderer.cameraZoom,
                    
                    (int)(width * pixelPerUnit),
                    (int)(height * pixelPerUnit)
                    );

                MonoDungeonDiscovery._spriteBatch.Draw(texture, bounds, Color.White);

                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "ScreenRef Obj: " + bounds.ToString(), new Vector2(50, 250), Color.White);
                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "World Ref Obj: x:" + x + ", y:" + y + ", width:" + width + ", height:" + height, new Vector2(50, 300), Color.White);

            }

            MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "FPS: " + MonoDungeonDiscovery.fps, new Vector2(50, 50), Color.White);
            MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "Camera: " + Renderer.cameraX + " ," + Renderer.cameraY, new Vector2(50, 100), Color.White);
            System.Drawing.PointF mouseWorldPos = new System.Drawing.PointF(
                //Input._mouseState.Position.X / pixelPerUnitNoZoom + Renderer.cameraX - Renderer.unitsWide / 2, 
                //Input._mouseState.Position.Y / pixelPerUnitNoZoom + Renderer.cameraY - Renderer.unitsTall / 2 

            );
            MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "Mouse: " + Input._mouseState.Position.ToString(), new Vector2(50, 150), Color.White);
            MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "Mouse (World): " + mouseWorldPos.X.ToString("{0.0}") + ", " + mouseWorldPos.Y.ToString("{0.0}"), new Vector2(50, 200), Color.White);
            MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "Zoom: " + Renderer.cameraZoom, new Vector2(50, 250), Color.White);

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
