using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MonoDungeonDiscovery
{
    public class MapObject : GameObject
    {
        public MapObject()
        {
            
        }
        public MapObject(Texture2D texture) 
        {
            Z = 0;
            ObjectTextures.Add("main", new GameObjectTexture(texture, new Vector3(0, 0, 0)));

            float aspectRatio = (float)texture.Width / (float)texture.Height;

            Bounds = new RectangleF(
                0,
                0,
                (aspectRatio > 1f) ? Renderer.unitsWide : (Renderer.unitsTall * aspectRatio),
                (aspectRatio > 1f) ? (Renderer.unitsWide / aspectRatio) : Renderer.unitsTall
                );
        }

        public override void Update()
        {
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
