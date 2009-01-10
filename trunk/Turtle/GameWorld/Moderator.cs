using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turtle
{
    /// <summary>
    /// This Moderator class exists to allow non static classes such as entities and levels to talk to each other.
    /// For example, a single entity may use the Moderator to request that it be added (or removed) from the collision manager
    /// even though that entity does not have a direct link to the Collision Manager, the Moderator can serve as the link between the two
    /// </summary>
    public static class Moderator
    {
        /// <summary>
        /// Actors that requested to be added into the World
        /// </summary>
        public static Stack<Actor> toAdd;
        /// <summary>
        /// Actors that requested to be removed from the World
        /// </summary>
        public static Stack<Actor> toRemove;
        /// <summary>
        /// Actors that are already in the World but have moved or have updated their Collision Boxes
        /// </summary>
        public static Stack<Actor> toUpdate;

        /// <summary>
        /// Actors that have been disposed of. Any Manager can Pop actors from this stack and use it instead of creating a new instance
        /// </summary>
        public static Stack<Actor> Disposed;

        /// <summary>
        /// This method (re)Initailizes the Moderator, clearing any Types that may contain old data
        /// </summary>
        public static void Initialize()
        {

           toAdd = new Stack<Actor>();
           toRemove = new Stack<Actor>();
           toUpdate = new Stack<Actor>();
           Disposed = new Stack<Actor>();


        }

        public static void HasMoved(Actor gameActor)
        {
            toUpdate.Push(gameActor);
        }

        public static void Dispose(Actor gameActor)
        {
            toRemove.Push(gameActor);
            Disposed.Push(gameActor);
        }



       


    }
}