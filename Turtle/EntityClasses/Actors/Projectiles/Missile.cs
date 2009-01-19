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

        Detector targeting;

        /// <summary>
        /// Parameter free constructor
        /// Not recommended for use
        /// </summary>
        public Missile(Vector2 pos, float rot)
        {
            this.Type = actorType.Bullet;

            lockedOn = false;
            targetVector = Vector2.Zero;

            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Projectiles\\missile"));

            Position = pos;
            Rotation = rot;

            speed = 12f;
            turnSpeed = 0.05f;

            tillLockOn = new TimeSpan(0, 0, 0, 0, 250);

            Origin = new Vector2(15, 31);

            lockAccuracy = 0.1f;

            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(new Vector2(16, 0), 16));

            Moderator.toAdd.Push(this);

            projectileLife = new TimeSpan(0, 0, 2);

            ActorSprite.Layer = 0.1f;
            targeting = new Detector(Position, 1000);

            Damage = 5;
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

            projectileLife -= gameTime.ElapsedGameTime;
            if (projectileLife.TotalMilliseconds <= 0)
            {
                destroy = true;
                this.Dispose();
            }

            if (tillLockOn.TotalMilliseconds <= 0)
                lockToEnemy();
            if (tillLockOn.TotalMilliseconds > 0) tillLockOn -= gameTime.ElapsedGameTime;

            if (lockedOn)
            {
                TurnToTarget();
            }

            UpdateColCirc();

            //move forward based on rotation
            VelocityX = (float)Math.Cos(Rotation) * speed;
            VelocityY = (float)Math.Sin(Rotation) * speed;

            Position += Velocity;

            ActorSprite.SetPosition(Position);
            ActorSprite.SetRotation(Rotation);

            if (!destroy)
                Moderator.HasMoved(this);
        }

        private void UpdateColCirc()
        {
            CollisionCircles[0].Position = new Vector2((float)Math.Cos(Rotation) * 16, (float)Math.Sin(Rotation) * 16);
        }

        private void lockToEnemy()
        {
            targeting.Position = Position;
            Moderator.HasMoved(targeting);

            List<Vector2> possibleTargets = new List<Vector2>();

            //find all enemey positions and put into possible targets list
            foreach (GridSquare g in targeting.GetGridSquares())
            {
                foreach (Actor a in g.Actors)
                {
                    if (a.getType() == actorType.Enemy)
                        possibleTargets.Add(a.Position);
                }
            }

            if (possibleTargets.Count > 0)
            {
                float lowdistance = Vector2.DistanceSquared(Position, possibleTargets[0]);
                int lowindex = 0;

                for (int i = 1; i < possibleTargets.Count; i++)
                {
                    float dist = Vector2.DistanceSquared(Position, possibleTargets[i]);
                    if (dist < lowdistance)
                    {
                        lowdistance = dist;
                        lowindex = i;
                    }
                }

                targetVector = possibleTargets[lowindex];
                lockedOn = true;
            }
            else
            {
                lockedOn = false;
            }
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

        public override void Collision(Actor gameActor)
        {
            destroy = true;
        }
    }
}
