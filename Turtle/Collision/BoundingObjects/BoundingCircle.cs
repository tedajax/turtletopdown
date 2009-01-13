using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    public class BoundingCircle
    {
        protected Vector2 position;
        protected float radius;

        public BoundingCircle(Vector2 pos, float rad)
        {
            position = pos;
            radius = rad;
        }

        public BoundingCircle(float x, float y, float rad)
        {
            position = new Vector2(x, y);
            radius = rad;
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 WidthModifier
        {
            get { return new Vector2(radius, radius); }
        }

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public bool Intersects(BoundingCircle c)
        {
            if (Vector2.Distance(position, c.Position) <= (radius + c.Radius))
                return true;
            else
                return false;
        }

        public bool Intersects(BoundingRectangle r)
        {            
            /*Vector2 rectCenter = new Vector2(r.X + r.Width / 2, r.Y + r.Height / 2);
            float dist = Vector2.Distance(rectCenter, Position) - Radius;
            if (dist < r.Width / 2)
                if (dist < r.Height / 2)
                    return true;

            return false;*/

            Vector2 Distance = this.Position - (r.Position + new Vector2(r.Width / 2, r.Height / 2));
            Distance.X = Math.Abs(Distance.X);
            Distance.Y = Math.Abs(Distance.Y);

            if (Distance.X > (r.Width / 2 + Radius)) { return false; }
            if (Distance.Y > (r.Height / 2 + Radius)) { return false; }

            if (Distance.X <= (r.Width / 2)) { return true; }
            if (Distance.Y <= (r.Height / 2)) { return true; }

            float cornerDistance = (float)Math.Sqrt(Math.Pow((Distance.X - r.Width / 2), 2) +
                                  Math.Pow((Distance.Y - r.Height / 2), 2));

            return (cornerDistance <= Radius);
        }
    }
}
