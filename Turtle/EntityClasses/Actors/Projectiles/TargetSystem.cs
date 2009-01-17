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
            int sizeover2 = size / 2;
            CollisionBoxes.Add(new BoundingRectangle(new Vector2(PositionX - sizeover2, PositionY - sizeover2), Vector2.One * size));

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
