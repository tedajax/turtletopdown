using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Missile : Projectile
    {
        Vector2 targetVector; //Where the missile should fly to
        bool lockedOn; //If true will travel towards the target vector otherwise will travel in a straight line
        float turnSpeed;

        float speed;

        TimeSpan tillLockOn;

        float lockAccuracy;

        /// <summary>
        /// Parameter free constructor
        /// Not recommended for use
        /// </summary>
        public Missile(Vector2 pos, float rot)
        {
            lockedOn = false;
            targetVector = Vector2.One * 100;

            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Projectiles\\missile"));

            Position = pos;
            Rotation = rot;

            speed = 8f;
            turnSpeed = 0.1f;

            tillLockOn = new TimeSpan(0, 0, 0, 0, 500);

            Origin = new Vector2(15, 31);

            lockAccuracy = 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            lockedOn = (tillLockOn.TotalMilliseconds > 0) ? false : true;
            if (tillLockOn.TotalMilliseconds > 0) tillLockOn -= gameTime.ElapsedGameTime;

            if (lockedOn)
            {
                TurnToTarget();
            }

            //move forward based on rotation
            VelocityX = (float)Math.Cos(Rotation) * speed;
            VelocityY = (float)Math.Sin(Rotation) * speed;

            Position += Velocity;

            ActorSprite.SetPosition(Position);
            ActorSprite.SetRotation(Rotation);
        }

        private void TurnToTarget()
        {
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

        /// <summary>
        /// Set the target vector explicitly
        /// </summary>
        /// <param name="tar">location to go to</param>
        public void SetTarget(Vector2 tar)
        {
            if (tillLockOn.TotalMilliseconds <= 0)
            {
                targetVector = tar;
                lockedOn = true;
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

        /// <summary>
        /// Set target implicitly using an actor and getting the position from it
        /// </summary>
        /// <param name="a">actor to lock on to</param>
        public void SetTarget(Actor a)
        {
            if (tillLockOn.TotalMilliseconds <= 0)
            {
                targetVector = a.Position;
                lockedOn = true;
            }
        }

        public override void Draw()
        {
            ActorSprite.Draw();
        }
    }
}
