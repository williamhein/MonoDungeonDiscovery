using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MonoDungeonDiscovery
{
    public class GameObject
    {
        public RectangleF Bounds { get; set; }
        public int Z {  get; set; }
        public Texture2D Texture { get; set; }
        public GameObject()
        {

        }
        public GameObject(Texture2D texture) 
        {
            Bounds = new RectangleF();
            Z = 0; 
            Texture = texture;
        }
        public GameObject(Texture2D texture,RectangleF bounds, int z)
        {
            Bounds = bounds;
            Z = z;
            Texture = texture;
        }

        public virtual void Update()
        {

        }
        public virtual Texture2D Draw()
        {
            return Texture;
        }
    }
}
