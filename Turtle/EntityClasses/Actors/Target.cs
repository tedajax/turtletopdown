using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Target : VisibleActor
    {
        public Target()
        {
            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\target"));
            Origin = new Vector2(15, 15);
        }

        public override void Update(GameTime gameTime)
        {
            Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            base.Update(gameTime);
        }
    }
}
