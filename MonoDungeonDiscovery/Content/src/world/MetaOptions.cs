using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoDungeonDiscovery
{
    public class MetaOptions
    {
        public static void Update(MonoDungeonDiscovery parent)
        {
            if (Input.GamepadButtonReleased(Buttons.Start, 0) || Input.KeyReleased(Keys.Escape))
                parent.CloseGame();
        }
    }
}
