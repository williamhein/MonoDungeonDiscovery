using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;

namespace MonoDungeonDiscovery
{
    public class MetaOptions
    {
        public const float cameraSpeedPercent = 2f;
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
