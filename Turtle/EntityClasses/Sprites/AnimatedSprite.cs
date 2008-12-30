using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class AnimatedSprite : Sprite
    {
        int rows, cols; //specify how many frames to break the image into
        int maxframe; //max frame integer
        int frame; //current frame
        Rectangle frameRectangle; //source rectangle that will allow for selecting pieces of the sprite sheet
        Vector2 frameDimensions; //stores dimensions of an individual frame
        int frameDelay;
        int curDelay;

        public AnimatedSprite(Texture2D img, int r, int c, int fdelay)
        {
            SpriteImage = img;
            rows = r;
            cols = c;

            maxframe = rows * cols - 1;
            frame = 0;
            frameDimensions = new Vector2((int)(SpriteImage.Width / cols), (int)(SpriteImage.Height / rows));
            frameRectangle = new Rectangle(0, 0, (int)frameDimensions.X, (int)frameDimensions.Y);
            frameDelay = fdelay;
            curDelay = 0;

            Position = Vector2.Zero;
            base.Initialize();
        }

        public void stepFrame()
        {
            curDelay++;
            if (curDelay >= frameDelay)
            {
                frame++;
                if (frame > maxframe)
                    frame = 0;
                int xpos = (frame % cols) * (int)frameDimensions.X;

                int frameCountdown = frame;
                int frameCounter = 0;
                while (frameCountdown > cols - 1)
                {
                    frameCountdown -= cols;
                    frameCounter++;
                }

                int ypos = frameCounter * (int)frameDimensions.Y;
                frameRectangle.X = xpos; frameRectangle.Y = ypos;

                curDelay = 0;
            }
        }

        public void stepFrame(int n)
        {
            curDelay++;
            if (curDelay >= frameDelay)
            {
                frame += n;
                while (frame > maxframe)
                {
                    frame -= maxframe;
                }
                int xpos = (frame % cols) * (int)frameDimensions.X;

                int frameCountdown = frame;
                int frameCounter = 0;
                while (frameCountdown > cols - 1)
                {
                    frameCountdown -= cols;
                    frameCounter++;
                }

                int ypos = frameCounter * (int)frameDimensions.Y;
                frameRectangle.X = xpos; frameRectangle.Y = ypos;

                curDelay = 0;
            }
        }

        public override void Draw()
        {
            if (!Hidden)
            {
                BaseGame.GetSpriteBatch().Draw(SpriteImage,
                                               ResPosition() + BaseGame.Camera.PositionAdd,
                                               frameRectangle,
                                               SpriteColor,
                                               Rotation,
                                               Origin,
                                               ResScale(),
                                               SpriteEffects.None,
                                               0);
            }
        }
    }
}
