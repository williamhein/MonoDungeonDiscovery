using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;

namespace MonoDungeonDiscovery
{
    public class MetaOptions
    {
        public const float cameraSpeedPercent = 4f;
        public static void Update(MonoDungeonDiscovery parent)
        {
            
            if (Input.KeyPressed(Keys.A))
            {
                //percent times zoom level per seconds
                Renderer.cameraX -= cameraSpeedPercent / Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
            }
            if (Input.KeyPressed(Keys.D))
            {
                //percent times zoom level per second
                Renderer.cameraX += cameraSpeedPercent / Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
            }
            if (Input.KeyPressed(Keys.S))
            {
                //percent times zoom level per second
                Renderer.cameraY += cameraSpeedPercent / Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
            }
            if (Input.KeyPressed(Keys.W))
            {
                //percent times zoom level per second
                Renderer.cameraY -= cameraSpeedPercent / Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
            }
            if (Input.KeyPressed(Keys.Q))
            {
                //percent times zoom level per second
                Renderer.cameraZoom -= Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
                if (Renderer.cameraZoom < 0.1)
                    Renderer.cameraZoom = 0.1f;
            }
            if (Input.KeyPressed(Keys.E))
            {
                //percent times zoom level per second
                Renderer.cameraZoom += Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
            }


            float threshold = 0.1f;
            Vector2 inputStick = Input.GamepadStick(GamePadTriggersThumbsticksEnum.Right, 0);
            if (inputStick.X > threshold || inputStick.X < -threshold)
            {
                Renderer.cameraX += inputStick.X * cameraSpeedPercent / Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
            }
            
            if (inputStick.Y > threshold || inputStick.Y < -threshold)
            {
                Renderer.cameraY -= inputStick.Y * cameraSpeedPercent / Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
            }

            float inputLeftTrigger = Input.GamepadTrigger(GamePadTriggersThumbsticksEnum.Left, 0);
            float inputRightTrigger = Input.GamepadTrigger(GamePadTriggersThumbsticksEnum.Right, 0);
            if (inputLeftTrigger > threshold)
            {
                //percent times zoom level per second
                Renderer.cameraZoom -= inputLeftTrigger * Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
                if (Renderer.cameraZoom < 0.1)
                    Renderer.cameraZoom = 0.1f;
            }
            if (inputRightTrigger > threshold)
            {
                //percent times zoom level per second
                Renderer.cameraZoom += inputRightTrigger * Renderer.cameraZoom / (float)MonoDungeonDiscovery.fps;
            }


            //if (Input.GamepadButtonPressed(Buttons.LeftShoulder, 0) || Input.KeyPressed(Keys.Left))
            //    Renderer.cameraRotation += 0.5f / (float)MonoDungeonDiscovery.fps;
            //
            //if (Input.GamepadButtonPressed(Buttons.RightShoulder, 0) || Input.KeyPressed(Keys.Right))
            //    Renderer.cameraRotation -= 0.5f / (float)MonoDungeonDiscovery.fps;


            if (Input.GamepadButtonReleased(Buttons.Start, 0) || Input.KeyReleased(Keys.Escape))
                parent.CloseGame();

            if (Input.GamepadButtonReleased(Buttons.Back, 0) || Input.KeyReleased(Keys.O))
                if (Renderer.isFullscreen)
                    Renderer.Resize(1280,720,false);
                else
                    Renderer.Resize(parent.Window.ClientBounds.Width, parent.Window.ClientBounds.Height, true);
        }
    }
}
