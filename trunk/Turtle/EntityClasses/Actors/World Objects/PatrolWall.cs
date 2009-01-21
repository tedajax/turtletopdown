using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class PatrolWall : SimpleWall
    {
        Vector2 StartPos;
        Vector2 EndPos;

        Vector2 PatrolDistance;
        Vector2 SpeedChangeDistance;

        Vector2 LerpPos;
        bool direction; //true - go to end position, false go to start position

        Vector2 OldPos;

        /// <summary>
        /// Creates a patrol wall
        /// </summary>
        /// <param name="img">Image for the wall sprite</param>
        /// <param name="spos">Starting position</param>
        /// <param name="epos">Ending position, will cycle between both positions</param>
        /// <param name="spd">Speed at which the wall patrols</param>
        public PatrolWall(Texture2D img, Vector2 spos, Vector2 epos)
        {
            ActorSprite = new Sprite(img);

            Position = spos;
            StartPos = spos;
            EndPos = epos;

            direction = true;

            Origin = new Vector2(img.Width / 2 - 1, img.Height / 2 - 1);
            SolidObject = true;

            PatrolDistance = new Vector2(MathHelper.Distance(StartPos.X, EndPos.X), MathHelper.Distance(StartPos.Y, EndPos.Y));
            SpeedChangeDistance = PatrolDistance / 4;

            LerpPos = Vector2.Zero;

            InitCollLists();
            CollisionBoxes.Add(new BoundingRectangle(Vector2.Zero, new Vector2(img.Width, img.Height)));
            Moderator.toAdd.Push(this);

            OldPos = Position;
        }

        public override void Update(GameTime gameTime)
        {
            OldPos = Position;

            if (direction)
            {
                LerpPos += Vector2.One * 0.01f;

                if (LerpPos.X >= 1)
                    direction = !direction;
            }
            else
            {
                LerpPos -= Vector2.One * 0.01f;

                if (LerpPos.X <= 0)
                    direction = !direction;
            }
                        
            position.X = MathHelper.Lerp(StartPos.X, EndPos.X, LerpPos.X);
            position.Y = MathHelper.Lerp(StartPos.Y, EndPos.Y, LerpPos.Y);

            //log the velocity for collision purposes
            Velocity = Position - OldPos;

            Moderator.HasMoved(this);

            ActorSprite.SetPosition(Position);
        }

        private bool WithinRange(float f1, float f2, float rng)
        {
            if (Math.Abs(f1 - f2) <= rng)
                return true;
            else
                return false;
        }
    }
}
