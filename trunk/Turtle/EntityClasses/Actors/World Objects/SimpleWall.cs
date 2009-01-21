using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class SimpleWall : VisibleActor
    {
        public SimpleWall()
        {
            this.position = Vector2.Zero;

            Initialize();
        }

        public SimpleWall(Vector2 Position)
        {
            this.position = Position;

            Initialize();
        }

        protected virtual void Initialize()
        {
            this.Type = actorType.Environment;
            this.ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\BackGround\\walltile"));
            this.ActorSprite.Position = Position;
            this.origin = new Vector2(20, 50);
            this.SolidObject = true;

            InitCollLists();
            CollisionBoxes.Add(new BoundingRectangle(Vector2.Zero, new Vector2(40, 100)));
            Moderator.toAdd.Push(this);
        }

        public override void Update(GameTime gameTime)
        {
            /*
             * IMPORTANT:
             *   Any moving wall you create must set velocity somewhere if other actors are expected to react to the collision properly
             *   Players and Enemies use the velocity of the wall to determine how to be pushed properly
             *   If you do not use velocity for movement I suggest just simply subtracting the old position from the current position and setting that
             */
        }
    }
}
