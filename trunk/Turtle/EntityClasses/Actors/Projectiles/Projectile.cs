using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Projectile : VisibleActor
    {
        //when this reaches 0 set the destroy flag to true
        protected TimeSpan projectileLife;

        //if true the projectile manager will remove this bullet
        protected bool destroy;
        
        public Projectile()
        {
            this.Type = actorType.Bullet;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            
            projectileLife = new TimeSpan(0, 0, 0);

            SolidObject = true;

            InitCollLists();
            Moderator.toAdd.Push(this);
        }

        public Projectile(Vector2 pos, Vector2 vel, float rot, int msec)
        {
            this.Type = actorType.Bullet;

            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Projectiles\\basicbullet"));

            Origin = new Vector2(7, 7);

            Position = pos;
            Velocity = vel;
            Rotation = rot;
            projectileLife = new TimeSpan(0, 0, 0, 0, msec);

            SolidObject = true;

            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 8));
            //CollisionBoxes.Add(new BoundingRectangle(Vector2.Zero,new Vector2(16,16)));
            Moderator.toAdd.Push(this);

            ActorSprite.Layer = 0.1f;
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
                        if (A.getType() == actorType.Enemy || A.getType() == actorType.Environment)
                        {
                            this.Dispose();
                            destroy = true;
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
            Position += Velocity;

            ActorSprite.SetPosition(Position);
            ActorSprite.SetRotation(Rotation);
            if (!destroy)
            {
                Moderator.HasMoved(this);
            }
        }

        public bool Destroy
        {
            get { return destroy; }
            set { destroy = value; }
        }
    }
}
