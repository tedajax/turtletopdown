using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    public class BoundingRectangle
    {
        private Vector2 position;
        private Vector2 dimensions;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public float Width
        {
            get { return dimensions.X; }
            set { dimensions.X = value; }
        }

        public float Height
        {
            get { return dimensions.Y; }
            set { dimensions.Y = value; }
        }

        public float Top
        {
            get { return position.Y; }
        }

        public float Bottom
        {
            get { return position.Y + dimensions.Y; }
        }

        public float Left
        {
            get { return position.X; }
        }

        public float Right
        {
            get { return position.X + dimensions.X; }
        }

        public Vector2 SizeModifier
        {
            get { return new Vector2(Width / 2, Height / 2); }
        }

        /// <summary>
        /// Create a bounding rectangle using individual floats
        /// </summary>
        /// <param name="x">Position X coordinate</param>
        /// <param name="y">Position Y coordinate</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        public BoundingRectangle(float x, float y, float w, float h)
        {
            position = new Vector2(x, y);
            dimensions = new Vector2(w, h);
        }

        /// <summary>
        /// Create rectangle using vector2 for position and floats for dimensions
        /// </summary>
        /// <param name="pos">Position coordinates stored in a Microsoft.XNA.Framework.Vector2</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        public BoundingRectangle(Vector2 pos, float w, float h)
        {
            position = pos;
            dimensions = new Vector2(w, h);
        }

        /// <summary>
        /// Create a rectangle using Vector2 for both sets of data
        /// </summary>
        /// <param name="pos">Position coordinates stored in a Microsoft.XNA.Framework.Vector2</param>
        /// <param name="dim">Dimensions stored in a Microsoft.XNA.Framework.Vector2</param>
        public BoundingRectangle(Vector2 pos, Vector2 dim)
        {
            position = pos;
            dimensions = dim;
        }

        public bool Intersects(BoundingRectangle r)
        {
            if (Bottom < r.Top) return false;
            if (Top > r.Bottom) return false;
            if (Right < r.Left) return false;
            if (Left > r.Right) return false;

            return true;
        }

        public bool Intersects(BoundingCircle c)
        {
            Vector2 rectCenter = new Vector2(X + Width / 2, Y + Height / 2);
            float dist = Vector2.Distance(rectCenter, c.Position) - c.Radius;
            if (dist < Width / 2)
                if (dist < Height / 2)
                    return true;

            return false;
        }
    }
}
