﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Disintegrate : Particle
    {
        Vector2[] imgPixels;
        Rectangle sourceRect;
        int pixels;

        Vector2 imgCenter;

        bool destroy;

        int pxScale;

        public Disintegrate(Texture2D pimg, Vector2 pos)
        {
            particleImage = pimg;
            Position = pos;

            alpha = 255;

            pxScale = 16;

            pixels = (pimg.Width * pimg.Height) / pxScale;

            imgPixels = new Vector2[pixels];
            for (int x = 0; x < pimg.Width / pxScale; x++)
                for (int y = 0; y < pimg.Height / pxScale; y++)
                {
                    imgPixels[(pimg.Width * x) + y] = new Vector2(x * pxScale, y * pxScale);
                }

            sourceRect = new Rectangle(0, 0, pxScale, pxScale);

            imgCenter = new Vector2(pimg.Width / 2, pimg.Height / 2);

            Origin = imgCenter;

            Scale = Vector2.One;
        }

        public override void Update(GameTime gameTime)
        {
            alpha -= 5;
            if (alpha <= 0) destroy = true;

            base.Update(gameTime);

            //cycle through move everything in random directions
            for (int i = 0; i < pixels; i++)
            {
                //calculate the angle from the center of the image to the current pixel point
                float pxRotation = (float)Math.Atan2((double)(imgPixels[i].Y - imgCenter.Y), (double)(imgPixels[i].X - imgCenter.X));
                float speed = BaseGame.Rand.Next(100) / 35; // to add some randomness
                imgPixels[i] += new Vector2((float)Math.Cos((double)pxRotation) * speed, (float)Math.Sin((double)pxRotation) * speed);
            }
        }

        public override void Draw()
        {
            for (int x = 0; x < particleImage.Width / pxScale; x++)
            {
                for (int y = 0; y < particleImage.Height / pxScale; y++)
                {
                    sourceRect.X = x * pxScale;
                    sourceRect.Y = y * pxScale;
                    BaseGame.GetSpriteBatch().Draw(particleImage,
                                                   ResPosition() + BaseGame.Camera.PositionAdd + imgPixels[(particleImage.Width * x) + y],
                                                   sourceRect,
                                                   new Color(255, 255, 255, (byte)alpha),
                                                   Rotation,
                                                   Origin,
                                                   ResScale(),
                                                   SpriteEffects.None,
                                                   0);
                }
            }
        }

        public bool Destroy
        {
            get { return destroy; }
            set { destroy = value; }
        }
    }
}
