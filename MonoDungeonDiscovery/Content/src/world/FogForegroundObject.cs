using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MonoDungeonDiscovery
{
    public class FogForegroundObject : GameObject
    {
        public float moveSpeed = 0.05f;
        public Vector2 currentDirection = new Vector2(1, 0);
        public double speedChangeInterval = 10000;
        public double intervalChangeGameTime = 0;
        public Vector2 newDirection = new Vector2(1, 0);
        public Vector2 oldDirection = new Vector2(1, 0);
        public FogForegroundObject()
        {
            ViewportLocked = true;
            Opacity = 127;
        }
        public FogForegroundObject(Texture2D texture)
        {
            ViewportLocked = true;
            Opacity = 127;

            Z = 1000;
            ObjectTextures.Add("main", new GameObjectTexture(texture, new Vector3(0, 0, 0)));
            //ObjectTextures.Add("left", new GameObjectTexture(texture, new Vector3(-Renderer.unitsWide, 0, 0)));
            //ObjectTextures.Add("catty", new GameObjectTexture(texture, new Vector3(-Renderer.unitsWide, -Renderer.unitsTall, 0)));
            //ObjectTextures.Add("top", new GameObjectTexture(texture, new Vector3(0, -Renderer.unitsTall, 0)));

            Bounds = new RectangleF(
                0,
                0,
                Renderer.unitsWide * 4,
                Renderer.unitsTall * 4
                );

            intervalChangeGameTime = MonoDungeonDiscovery.time;
        }

        public override void Update()
        {
            if (intervalChangeGameTime + speedChangeInterval < MonoDungeonDiscovery.time)
            {
                oldDirection = newDirection;
                newDirection = new Vector2(MonoDungeonDiscovery.random.Next(-100, 100), MonoDungeonDiscovery.random.Next(-100, 100));
                intervalChangeGameTime = MonoDungeonDiscovery.time;
            }

            currentDirection = 
                Vector2.Multiply(oldDirection, (float)((speedChangeInterval - (MonoDungeonDiscovery.time - intervalChangeGameTime)) / speedChangeInterval)) +
                Vector2.Multiply(newDirection, (float)((MonoDungeonDiscovery.time - intervalChangeGameTime) / speedChangeInterval))
                ;

            currentDirection.Normalize();

            ObjectTextures["main"].PositionRotation.X += currentDirection.X * moveSpeed / (float)MonoDungeonDiscovery.fps;
            ObjectTextures["main"].PositionRotation.Y += currentDirection.Y * moveSpeed / (float)MonoDungeonDiscovery.fps;
            //ObjectTextures["left"].PositionRotation.X += direction.X * moveSpeed / (float)MonoDungeonDiscovery.fps;
            //ObjectTextures["left"].PositionRotation.Y += direction.Y * moveSpeed / (float)MonoDungeonDiscovery.fps;
            //ObjectTextures["catty"].PositionRotation.X += direction.X * moveSpeed / (float)MonoDungeonDiscovery.fps;
            //ObjectTextures["catty"].PositionRotation.Y += direction.Y * moveSpeed / (float)MonoDungeonDiscovery.fps;
            //ObjectTextures["top"].PositionRotation.X += direction.X * moveSpeed / (float)MonoDungeonDiscovery.fps;
            //ObjectTextures["top"].PositionRotation.Y += direction.Y * moveSpeed / (float)MonoDungeonDiscovery.fps;



            //if (ObjectTextures["main"].PositionRotation.X + ObjectTextures["main"].Texture.Width / 2 > Bounds.Width)
            //    ObjectTextures["main"].PositionRotation.X = ObjectTextures["left"].PositionRotation.X - ObjectTextures["left"].Texture.Width / 2;
            //else if (ObjectTextures["main"].PositionRotation.X < -Bounds.Width)
            //    ObjectTextures["main"].PositionRotation.X = ObjectTextures["left"].PositionRotation.X + ObjectTextures["left"].Texture.Width;
            //
            //if (ObjectTextures["main"].PositionRotation.X > Bounds.Height)
            //    ObjectTextures["main"].PositionRotation.X = ObjectTextures["left"].PositionRotation.X - 2 * Bounds.Height;
            //else if (ObjectTextures["main"].PositionRotation.X < -Bounds.Height)
            //    ObjectTextures["main"].PositionRotation.X = ObjectTextures["left"].PositionRotation.X + 2 * Bounds.Height;


            //if (Texture != null)
            //{
            //    Size = new Size(Texture.Width,Texture.Height);
            //}

            //if (Input.GamepadButtonPressed(Buttons.LeftShoulder, 0) || Input.KeyPressed(Keys.Left))
            //    ObjectTextures["main"].PositionRotation.Z += 0.5f / (float)MonoDungeonDiscovery.fps;
            //
            //if (Input.GamepadButtonPressed(Buttons.RightShoulder, 0) || Input.KeyPressed(Keys.Right))
            //    ObjectTextures["main"].PositionRotation.Z -= 0.5f / (float)MonoDungeonDiscovery.fps;
        }
    }
}
