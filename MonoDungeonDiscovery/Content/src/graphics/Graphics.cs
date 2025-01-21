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
        public static Effect fogMask;

        // Rectangle array (store as normalized coordinates: [x, y, width, height])
        public static float[] rectangles = new float[40]; // 10 rectangles, 4 values each
        public static int rectangleCount = 0; // Active rectangle count
        public static void Initialize(MonoDungeonDiscovery parent)
        {
            defaultFont = parent.Content.Load<SpriteFont>("Hope");
            fogMask = parent.Content.Load<Effect>("FogMask");

            sprites = new List<Texture2D>();

            //sprites.Add(SpriteResource.SpriteResources["deathhouse"].texture);

            spriteTexts = new List<SpriteText>();

            AddRectangle(new Rectangle(100,100,100,100),100,100);

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

            //parent.GraphicsDevice.Clear(Color.CornflowerBlue);
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

            //parent.GraphicsDevice.Clear(Color.CornflowerBlue);

            World.gameObjects.Sort((p, q) => p.Z.CompareTo(q.Z));

            float pixelPerUnitNoZoom = Renderer.GetWindowWidth() / Renderer.unitsWide;
            float pixelPerUnit = Renderer.GetWindowWidth() / Renderer.unitsWide * Renderer.cameraZoom;

            //Matrix rotationMatrix = Matrix.CreateRotationZ(Renderer.cameraRotation);
            //start batch
            //MonoDungeonDiscovery._spriteBatch.Begin(transformMatrix: rotationMatrix);

            MonoDungeonDiscovery._spriteBatch.Begin(effect: fogMask);

            // Set the shader parameters

            // Pass the rectangle data to the shader
            fogMask.Parameters["Rectangles"].SetValue(rectangles);
            fogMask.Parameters["RectangleCount"].SetValue(rectangleCount);

            foreach (GameObject gameObject in World.gameObjects)
            {
                float x = gameObject.Bounds.X, y = gameObject.Bounds.Y, width = gameObject.Bounds.Width, height = gameObject.Bounds.Height;
                if (
                    Renderer.cameraZoom * (x - Renderer.cameraX - width / 2) > Renderer.unitsWide / 2 ||
                    Renderer.cameraZoom * (x + width / 2 - Renderer.cameraX) < -Renderer.unitsWide / 2 ||
                    Renderer.cameraZoom * (y - height / 2 - Renderer.cameraY) > Renderer.unitsTall / 2 ||
                    Renderer.cameraZoom * (y + height / 2 - Renderer.cameraY) < -Renderer.unitsTall / 2
                ) continue;

                foreach (GameObjectTexture gameObjectTexture in gameObject.ObjectTextures.Values)
                {
                    Rectangle bounds = new Rectangle(
                        //(int)((Renderer.unitsWide / 2 - Renderer.cameraX + x - width / 2) * pixelPerUnit),
                        //(int)((Renderer.unitsTall / 2 - Renderer.cameraY + y - height / 2) * pixelPerUnit),

                        (int)((Renderer.unitsWide / 2 * pixelPerUnitNoZoom) + (-Renderer.cameraX + x + gameObjectTexture.PositionRotation.X - width / 2) * pixelPerUnit),
                        (int)((Renderer.unitsTall / 2 * pixelPerUnitNoZoom) + (-Renderer.cameraY + y + gameObjectTexture.PositionRotation.Y - height / 2) * pixelPerUnit),

                        (int)(width * pixelPerUnit),
                        (int)(height * pixelPerUnit)
                        );

                    //MonoDungeonDiscovery._spriteBatch.Draw(gameObjectTexture.Texture, bounds, Color.White);
                    //MonoDungeonDiscovery._spriteBatch.Draw(gameObjectTexture.Texture, bounds, null, Color.White, gameObjectTexture.PositionRotation.Z, new Vector2(gameObjectTexture.Texture.Width/2, gameObjectTexture.Texture.Height/2), SpriteEffects.None, 1);

                    fogMask.Parameters["TextureSampler"].SetValue(gameObjectTexture.Texture);

                    MonoDungeonDiscovery._spriteBatch.Draw(
                        gameObjectTexture.Texture, 
                        (gameObject.ViewportLocked)?new Rectangle(
                            (int)(Renderer.unitsWide / 2 * pixelPerUnitNoZoom + (gameObjectTexture.PositionRotation.X - width / 2) * pixelPerUnit),
                            (int)(Renderer.unitsTall / 2 * pixelPerUnitNoZoom + (gameObjectTexture.PositionRotation.Y - height / 2) * pixelPerUnit),
                            (int)(width * pixelPerUnit),
                            (int)(height * pixelPerUnit)
                            ) : bounds, 
                        null, 
                        Color.FromNonPremultiplied(255,255,255,gameObject.Opacity), 
                        gameObjectTexture.PositionRotation.Z, 
                        new Vector2(0,0), 
                        SpriteEffects.None, 
                        1
                    );
                }
                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "ScreenRef Obj: " + bounds.ToString(), new Vector2(50, 250), Color.White);
                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "World Ref Obj: x:" + x + ", y:" + y + ", width:" + width + ", height:" + height, new Vector2(50, 300), Color.White);

            }

                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "FPS: " + MonoDungeonDiscovery.fps, new Vector2(50, 50), Color.White);
                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "Camera: " + Renderer.cameraX + " ," + Renderer.cameraY, new Vector2(50, 100), Color.White);
                //System.Drawing.PointF mouseWorldPos = new System.Drawing.PointF(
                //    //Input._mouseState.Position.X / pixelPerUnitNoZoom + Renderer.cameraX - Renderer.unitsWide / 2, 
                //    //Input._mouseState.Position.Y / pixelPerUnitNoZoom + Renderer.cameraY - Renderer.unitsTall / 2 
                //
                //);
                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "Mouse: " + Input._mouseState.Position.ToString(), new Vector2(50, 150), Color.White);
                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "Mouse (World): " + mouseWorldPos.X.ToString("{0.0}") + ", " + mouseWorldPos.Y.ToString("{0.0}"), new Vector2(50, 200), Color.White);
                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, "Zoom: " + Renderer.cameraZoom, new Vector2(50, 250), Color.White);
                //MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, Input.GamepadStick(GamePadTriggersThumbsticksEnum.Right, 0).ToString(), new Vector2(50, 250), Color.White);
            MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, (World.gameObjects[1] as FogForegroundObject).oldDirection.ToString(), new Vector2(50, 250), Color.White);
            MonoDungeonDiscovery._spriteBatch.DrawString(Graphics.defaultFont, (World.gameObjects[1] as FogForegroundObject).intervalChangeGameTime.ToString() + " , " + MonoDungeonDiscovery.time, new Vector2(50, 200), Color.White);
            MonoDungeonDiscovery._spriteBatch.End();
        }
        public static void AddRectangle(Rectangle rect, int textureWidth, int textureHeight)
        {
            if (rectangleCount >= 10) return; // Limit to 10 rectangles

            float normalizedX = (float)rect.X / textureWidth;
            float normalizedY = (float)rect.Y / textureHeight;
            float normalizedWidth = (float)rect.Width / textureWidth;
            float normalizedHeight = (float)rect.Height / textureHeight;

            // Store in the array
            rectangles[rectangleCount * 4 + 0] = normalizedX;
            rectangles[rectangleCount * 4 + 1] = normalizedY;
            rectangles[rectangleCount * 4 + 2] = normalizedWidth;
            rectangles[rectangleCount * 4 + 3] = normalizedHeight;

            rectangleCount++;
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
