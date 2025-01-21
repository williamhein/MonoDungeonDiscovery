using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoDungeonDiscovery
{
    public class World
    {
        public static List<GameObject> gameObjects;
        public static void Initialize(MonoDungeonDiscovery parent)
        {
            gameObjects = new List<GameObject>();

            gameObjects.Add(new MapObject(SpriteResource.SpriteResources["deathhouse"].texture));
            gameObjects.Add(new FogForegroundObject(SpriteResource.SpriteResources["cloudtexture"].texture));
            //gameObjects.Add(new MapObject(SpriteResource.SpriteResources["deathhouse"].texture));
            //gameObjects[1].Bounds = new System.Drawing.RectangleF(-5.5f, -1f, 1f, 1f);
        }
        public static void Update(MonoDungeonDiscovery parent)
        {
            foreach (GameObject obj in gameObjects)
            {
                obj.Update();
            }
        }
        public static void Draw(MonoDungeonDiscovery parent)
        {
            Graphics.DrawGameObjects(parent);
        }
    }
}
