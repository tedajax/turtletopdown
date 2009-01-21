using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    /// <summary>
    /// Moves towards the player no matter what, slightly faster than the player can move
    /// </summary>
    class Chaser : Enemy
    {
        float speed;
        float maxspeed;

        Vector2 targetVector;
        bool lockedOn;

        public Chaser(Vector2 pos)
        {
            this.Type = actorType.Enemy;

            EnemySprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Enemies\\circle"));

            Position = pos;
            Origin = new Vector2(31, 31);

            SolidObject = true;

            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 27));

            Moderator.toAdd.Push(this);

            speed = 0f;
            maxspeed = 3f;

            targetVector = Vector2.Zero;
            lockedOn = false;

            deathState = 0;

            Health = 20;
        }

        public override void Update(GameTime gameTime)
        {
            FindCollisions();

            if (deathState == 0)
            {
                targetVector = GameWorld.PlayerPositions[0];

                if (Vector2.DistanceSquared(Position, targetVector) < 250000)
                    lockedOn = true;
                else
                    lockedOn = false;

                if (lockedOn)
                {
                    Rotation = (float)Math.Atan2((targetVector.Y - Position.Y), (targetVector.X - Position.X));
                    speed = MathHelper.Lerp(speed, maxspeed, 0.1f);
                }
                else
                    speed = MathHelper.Lerp(speed, 0, 0.1f);
                
                if (!wallCollision && !collInertia)
                    Velocity = new Vector2((float)Math.Cos(Rotation) * speed, (float)Math.Sin(Rotation) * speed);

                if (collInertia)
                {
                    Velocity *= .9f;

                    if (Math.Abs(VelocityX) < 1f && Math.Abs(VelocityY) < 1f)
                        collInertia = false;
                }

                Position += Velocity;

                Moderator.HasMoved(this);
            }
            else if (deathState == 1)
            {
                enemyDeath.Update(gameTime);
                if (enemyDeath.Destroy)
                {
                    destroy = true;
                    Moderator.toRemove.Push(this);
                    deathState = 3;
                }
            }

            EnemySprite.SetPosition(Position);
        }

        public override void KillEnemy()
        {
            if (deathState == 0)
            {
                deathState = 1;
                enemyDeath = new Disintegrate(EnemySprite.GetImage(), Position, 8);
                enemyDeath.ParticleColor = EnemySprite.SColor;
                this.Type = actorType.Misc;
                SolidObject = false;
            }
        }
    }
}
