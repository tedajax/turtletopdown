using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Photon : Projectile
    {
        Actor targetActor;

        float turnSpeed;
        float speed;
        float maxspeed;
        float lockAccuracy;

        float imgRotation;

        public Photon(Vector2 pos, Actor a)
        {
            this.Type = actorType.Photon;
            
            Position = pos;

            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Projectiles\\photonshot"));

            targetActor = a;
            turnSpeed = 0.2f;
            maxspeed = 5f;
            speed = 15f;
            lockAccuracy = 0.1f;

            Origin = new Vector2(7, 7);

            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 8));

            SolidObject = true;

            Moderator.toAdd.Push(this);

            projectileLife = new TimeSpan(0, 0, 5);

            ActorSprite.Layer = 0.1f;

            Damage = 20;
        }

        public override void Update(GameTime gameTime)
        {
            //Check if you've collided with any enemies
            foreach (GridSquare g in gridSquares)
            {
                foreach (Actor A in g.Actors)
                {
                    if (A.CollidesWith(this))
                    {
                        if (A.getType() == actorType.Environment)
                        {
                            destroy = true;
                            this.Dispose();
                        }
                    }
                }
            }

            speed = MathHelper.Lerp(speed, maxspeed, 0.1f);

            projectileLife -= gameTime.ElapsedGameTime;
            if (projectileLife.TotalMilliseconds <= 0)
            {
                destroy = true;
                this.Dispose();
            }

            Enemy e = (Enemy)targetActor;
            if (e.Health <= 0)
                projectileLife = new TimeSpan(0, 0, 0);

            TurnToTarget();

            //move forward based on rotation
            VelocityX = (float)Math.Cos(Rotation) * speed;
            VelocityY = (float)Math.Sin(Rotation) * speed;

            Position += Velocity;

            //make it spin
            imgRotation += MathHelper.Pi / 32;

            ActorSprite.SetPosition(Position);
            ActorSprite.SetRotation(imgRotation);

            if (!destroy)
                Moderator.HasMoved(this);
        }

        private void TurnToTarget()
        {
            Vector2 targetVector = targetActor.Position;

            //get differences between position and target positions
            float x = targetVector.X - position.X;
            float y = targetVector.Y - position.Y;

            //get the angle between the two locations
            float targetAngle = (float)Math.Atan2(y, x);
            //get a difference between the two angles and constrain it
            float difference = WrapAngle(targetAngle - Rotation);

            if (Math.Abs(difference) > lockAccuracy)
            {
                //clamp the difference within the turnspeed range
                difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

                Rotation = BaseGame.WrapValueRadian(Rotation + difference);
            }
        }

        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
    }
}
