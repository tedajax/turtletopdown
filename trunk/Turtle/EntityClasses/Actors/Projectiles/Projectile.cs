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
        TimeSpan bulletLife;

        //if true the projectile manager will remove this bullet
        bool destroy;
        //delete this
        bool tempflag = false;
        public Projectile()
        {
            this.Type = actorType.Bullet;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            
            bulletLife = new TimeSpan(0, 0, 0);

            SolidObject = true;

            InitCollLists();

            Moderator.toAdd.Push(this);
            
        }

        public Projectile(Vector2 pos, Vector2 vel, float rot, int msec)
        {
            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Projectiles\\basicbullet"));

            Origin = new Vector2(7, 7);

            Position = pos;
            Velocity = vel;
            Rotation = rot;
            bulletLife = new TimeSpan(0, 0, 0, 0, msec);

            SolidObject = true;

            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 8));
            Moderator.toAdd.Push(this);
        }


        public override void Update(GameTime gameTime)
        {
            //Check if you've collided with any enemies
            foreach (GridSquare g in gridSquares)
            {
                foreach (Actor A in g.Actors)
                {
                    if (A.getType() == actorType.Enemy)
                    {
                        A.Collision(this);
                        //Moderator.toRemove.Push(this);
                        Moderator.Dispose(this);
                        destroy = true;
                        tempflag = true;
                    }
                }
            }

            bulletLife -= gameTime.ElapsedGameTime;

            if (bulletLife.TotalMilliseconds <= 0)
            {
                destroy = true;
                Moderator.Dispose(this);
            }
            Position += Velocity;

            ActorSprite.SetPosition(Position);
            ActorSprite.SetRotation(Rotation);
            if (!tempflag)
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
