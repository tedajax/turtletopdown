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

        bool hit;

        public SimpleWall(Vector2 Position)
        {
            this.Type = actorType.Environment;
            this.position = Position;
            this.ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\BackGround\\walltile"));
            this.ActorSprite.Position = Position;
            this.origin = new Vector2(20,50);
            this.SolidObject = true;

            InitCollLists();
            CollisionBoxes.Add(new BoundingRectangle(Vector2.Zero,new Vector2(40,100)));
            Moderator.toAdd.Push(this);
        }

        public override void  Update(GameTime gameTime)
        {
            foreach(GridSquare g in this.gridSquares)
            {
                foreach(Actor A in g.Actors)
                {
                    if (A.getType() == actorType.Player)
                    {
                        if (A.CollidesWith(this))
                        {
                            A.Collision(this);
                        }
                    }
                }
            }

 	       
        }

     

    }


}
