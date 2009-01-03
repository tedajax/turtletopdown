using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Particle : Entity
    {
        protected int alpha; //for controlling alpha values, pretty much every particle will use this
        protected Texture2D particleImage; //Particles will always have some sort of image

        public Particle()
        {
            particleImage = null;
            alpha = 255;
        }

        public override void Update(GameTime gameTime)
        {
            //clamp alpha so it never goes outside byte range
            if (alpha < 0) alpha = 0;
            else if (alpha > 255) alpha = 255;
        }

        public override void Draw()
        {
            
        }
    }
}
