using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class SpinningButton : TextButton
    {
        public SpinningButton(string txt, Vector2 pos)
        {
            buttonText = new Text(txt);
            Position = pos;
            buttonText.SetPosition(pos);

            hoverColor = Color.Green;
            activeColor = Color.Red;
        }

        public override void Update(GameTime gameTime)
        {
            Rotation += 1.0f;
        }
    }
}
