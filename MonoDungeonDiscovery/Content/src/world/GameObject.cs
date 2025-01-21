using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MonoDungeonDiscovery
{
    public class GameObject
    {
        public RectangleF Bounds { get; set; } = new RectangleF();
        public int Z {  get; set; }
        public bool ViewportLocked { get; set; } = false;
        public int Opacity { get; set; } = 255;
        public Dictionary<string,GameObjectTexture> ObjectTextures { get; set; } = new Dictionary<string,GameObjectTexture>();
        public GameObject()
        {
            
        }
        public GameObject(Texture2D texture) 
        {
            Z = 0;
            ObjectTextures.Add("main", new GameObjectTexture(texture, new Vector3(0, 0, 0)));
        }
        public GameObject(Texture2D texture, RectangleF bounds, int z)
        {
            Bounds = bounds;
            Z = z;
            ObjectTextures.Add("main", new GameObjectTexture(texture, new Vector3(0, 0, 0)));
        }

        public virtual void Update()
        {

        }
        public virtual void Draw()
        {

        }
    }
    public class GameObjectTexture
    {
        //
        // Summary:
        //     Expects x, y, and rotation values in world units relative to main texture (main texture should be 0,0 for x,y)
        public Vector3 PositionRotation = new Vector3(0, 0, 0);
        public Texture2D Texture = null;

        public GameObjectTexture(Texture2D texture, Vector3 positionrotation)
        {
            PositionRotation = positionrotation;
            Texture = texture;
        }
    }
}
