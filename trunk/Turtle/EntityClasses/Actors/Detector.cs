using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    class Detector : Actor
    {
        public Detector(Vector2 pos, int size)
        {
            Position = pos;

            InitCollLists();
            CollisionBoxes.Add(new BoundingRectangle(Vector2.Zero, Vector2.One * size));

            SolidObject = true;

            this.Type = actorType.Detector;

            Moderator.toAdd.Push(this);
        }

        public List<GridSquare> GetGridSquares()
        {
            return gridSquares;
        }

        public List<Actor> GetDetectedActors(actorType type)
        {
            List<Actor> actors = new List<Actor>();

            foreach (GridSquare g in gridSquares)
            {
                foreach (Actor A in g.Actors)
                {
                    if (A.getType() == type)
                        actors.Add(A);
                }
            }

            return actors;
        }
    }
}
