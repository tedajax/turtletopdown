using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle.Collision.Manager
{
    /// <summary>
    /// Stores data about a single collision that occured
    /// </summary>
    class Collision
    {
    
        Actor collidedWith;
        /// <summary>
        /// The Actor that was involved in this Collision
        /// </summary>
        public Actor CollidedWith
        {
            get { return collidedWith; }
            set { this.collidedWith = value; }
        }
           

        BoundingCircle[] boundingCircles;
        /// <summary>
        /// Array of Bounding Circles which were invovled in this Collision
        /// </summary>
        public BoundingCircle[] BoundingCircles
        {
            get { return boundingCircles; }
            set { this.boundingCircles = value; }
        }

      
        BoundingRectangle[] boundingRectangles;
        /// <summary>
        /// Bounding Rectangles involved in this Collision
        /// </summary>
        public BoundingRectangle[] BoundingRectangle
        {
            get { return boundingRectangles; }
            set { this.boundingRectangles = value; }
        }
      
        Vector2 collisionLocation;
        /// <summary>
        /// Specific Location of the Collision
        /// </summary>
        public Vector2 CollisionLocation
        {
            get { return collisionLocation; }
            set { this.collisionLocation = value; }
        }

    
        Vector2 collidorVelocity;
        /// <summary>
        /// The Velocity that the other actor was moving with when Collision Occured
        /// </summary>
        public Vector2 CollidorVelocity
        {
            get { return collidorVelocity; }
            set { this.collidorVelocity = value; }
        }


     
        Vector2 velocity;
        /// <summary>
        /// The Velocity that this Actor was moving at when Collision Occured
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
            set { this.velocity = value; }
        }


    }
}
