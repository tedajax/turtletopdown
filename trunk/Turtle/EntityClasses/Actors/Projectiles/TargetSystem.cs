using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    class TargetSystem : Actor
    {
        public TargetSystem(Vector2 pos, int size)
        {
            Position = pos;

            InitCollLists();
            CollisionBoxes.Add(new BoundingRectangle(Vector2.Zero, Vector2.One * size));

            SolidObject = true;

            this.Type = actorType.Targeting;

            Moderator.toAdd.Push(this);
        }

        public List<GridSquare> GetGridSquares()
        {
            return gridSquares;
        }
    }
}
