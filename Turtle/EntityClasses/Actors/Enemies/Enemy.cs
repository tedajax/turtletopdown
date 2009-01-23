using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Enemy : Actor
    {
        protected Sprite EnemySprite;
        protected int deathState; //0 alive, 1 dieing, 2 set the destroy to true
        protected Disintegrate enemyDeath;

        protected bool justHit; //set to true when hit, reset to false every update

        protected bool wallCollision; //currently colliding with wall
        protected bool collInertia; //moving from collision with moving object

        protected bool targeted;
        protected Sprite targetedSprite;

        public Enemy()
        {
            Position = Vector2.Zero;
            Initialize();
        }


        public Enemy(Vector2 pos)
        {
            Position = pos;
            Initialize();
        }

        protected virtual void Initialize()
        {
            this.Type = actorType.Enemy;
            //The base enemy will load the really retarded sprite I made
            EnemySprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Enemies\\basicenemy"));

            Origin = new Vector2(31, 31);

            SolidObject = true;

            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 32));

            EnemySprite.SetColor(new Color((byte)BaseGame.Rand.Next(256), (byte)BaseGame.Rand.Next(256), (byte)BaseGame.Rand.Next(256)));
            Moderator.toAdd.Push(this);

            Health = 10;

            wallCollision = false;

            targeted = false;
            LoadTargetedSprite();
        }

        protected virtual void LoadTargetedSprite()
        {
            targetedSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\targeted"));
            targetedSprite.Scale = Vector2.One * 2f;
            targetedSprite.SetHidden(true);
        }

        protected virtual void FindCollisions()
        {
            justHit = false;
            wallCollision = false;
            
            foreach (GridSquare g in gridSquares)
            {
                foreach (Actor a in g.Actors)
                {
                    if (a.getType() == actorType.Bullet || a.getType() == actorType.Photon)
                    {
                        if (a.CollidesWith(this))
                        {
                            Health -= a.Damage;
                            justHit = true;

                            if (Health <= 0)
                                KillEnemy();
                            
                            a.Collision(this);
                        }
                    }
                    else if (a.getType() == actorType.Environment)
                    {
                        if (a.CollidesWith(this))
                        {
                            wallCollision = true;
                            collInertia = true;
                            Vector2 newVector = a.GetPosition() - this.Position;
                            newVector.Normalize();
                            this.Velocity = 1 * Vector2.Negate(newVector) + a.Velocity;
                        }
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            FindCollisions();
            
            if (deathState == 0)
            {
                //make the enemy just spin in circles
                Rotation += MathHelper.Pi / 16;
                Rotation = BaseGame.WrapValueRadian(Rotation);

                if (Math.Abs(VelocityX) < 1f && Math.Abs(VelocityY) < 1f && collInertia)
                    collInertia = false;

                UpdateTargeted();
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
            EnemySprite.SetRotation(Rotation);
        }

        protected virtual void UpdateTargeted()
        {
            if (targeted)
            {
                targetedSprite.SetPosition(Position);
                if (targetedSprite.ScaleX > 1)
                    targetedSprite.Scale = new Vector2(targetedSprite.ScaleX - 0.1f, targetedSprite.ScaleY - 0.1f);

                targetedSprite.SetHidden(false);
            }
            else
            {
                targetedSprite.Scale = Vector2.One * 2;
                targetedSprite.SetHidden(true);
            }
        }

        public override void Draw()
        {
            if (justHit)
                EnemySprite.SColor = new Color(255, 255, 255, 127);
            else
                EnemySprite.SColor = Color.White;

            if (deathState == 0)
            {
                EnemySprite.Draw();
                targetedSprite.Draw();
            }
            else if (deathState == 1)
                enemyDeath.Draw();
        }

        public virtual void KillEnemy()
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

        public override void Collision(Actor gameActor)
        {
            KillEnemy();
        }

        public bool Targeted
        {
            get { return targeted; }
            set { targeted = value; }
        }
    }
}
