using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoDungeonDiscovery
{
    public class Animation
    {
        public SpriteResource[] Frames;

        private int currentFrame = 0;

        private int fps = 4;

        private double lastFrameTime = 0;

        public bool Loop {  get; set; }
        public Animation (SpriteResource[] frames)
        {
            this.Frames = frames;
        }
        public Animation(SpriteResource frame)
        {
            this.Frames = [frame];
        }
        public void Play(GameTime gameTime)
        {
            double currentTime = gameTime.TotalGameTime.TotalMilliseconds;
            if (currentTime > lastFrameTime + 1000000000 / fps)
            {
                currentFrame++;
                if (currentFrame >= Frames.Length)
                {
                    if (Loop)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame--;
                    }
                }

                lastFrameTime = currentTime;
            }
        }

        public Texture2D GetTexture()
        {
            return Frames[currentFrame].texture;
        }

        public void Reset()
        {
            currentFrame = 0;
        }
    }
}
