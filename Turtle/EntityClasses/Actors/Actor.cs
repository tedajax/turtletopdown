using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    public enum actorType
    {
        Player,
        Bullet,
        Enemy,
        Environment,
        Misc
    }


    public abstract class Actor : Entity
    {
        protected List<BoundingRectangle> CollisionBoxes;
        protected List<BoundingCircle> CollisionCircles;
        protected actorType Type;

        /// <summary>
        /// This boolean will be set to true once the Actor has been disposed of by Moderator/CollisionManager
        /// </summary>
        protected bool isDisposed = false;
        


        /// <summary>
        /// Solid Object means that it will Fire collision events if Colliding with another object
        /// If the object is not solid, it may still recieve collision events from other solid objects
        /// it is up to each individual object to utilize the data
        /// </summary>
        protected Boolean SolidObject;

        protected List<GridSquare> gridSquares;
        public List<GridSquare> GridSquares
        {
            get { return this.gridSquares; }
        }
        public void addGridSquare(GridSquare newSquare)
        {
            gridSquares.Add(newSquare);
        }
        public void clearGridSquares()
        {
            gridSquares.Clear();
        }

   

        protected void InitCollLists()
        {
            CollisionBoxes = new List<BoundingRectangle>();
            CollisionCircles = new List<BoundingCircle>();
            gridSquares = new List<GridSquare>();
        }

        public override void Update(GameTime gameTime)
        {
            throw new Exception("This method should not be used!");
        }

        public override void Draw()
        {
            throw new Exception("This method should not be used!");
        }

        /// <summary>
        /// By Default this dispose method calls the dispose method in the Moderator but if a special manager needs to take care of disposal
        /// seperately, then this method can be overridden for that purpose.
        /// </summary>
        public virtual void Dispose()
        {
            this.isDisposed = true;
            Moderator.Dispose(this);
        }

        public actorType getType() { return this.Type; }
        /// <summary>
        /// This Value is true if the Collision Manager/Moderator has disposed of the Object
        /// </summary>
        /// <returns></returns>
        public bool IsDisposed() { return isDisposed; }


        public List<BoundingRectangle> GetCollBoxes() { return CollisionBoxes; }
        public void SetCollBoxes(List<BoundingRectangle> newcol) { CollisionBoxes = newcol; }
        public void AddCollBox(BoundingRectangle newcol) { CollisionBoxes.Add(newcol); }
        public void ClearCollBoxes() { CollisionBoxes.Clear(); }
        public int CollBoxCount() { return CollisionBoxes.Count; }

        public List<BoundingCircle> GetCollCircs() { return CollisionCircles; }
        public void SetCollCircs(List<BoundingCircle> newcol) { CollisionCircles = newcol; }
        public void AddCollCirc(BoundingCircle newcol) { CollisionCircles.Add(newcol); }
        public void ClearCollCircs() { CollisionCircles.Clear(); }
        public int CollCircCount() { return CollisionCircles.Count; }

        public int TotalCollCount() { return CollisionBoxes.Count + CollisionCircles.Count; }

        public bool CollidesWith(Actor a)
        {
            //do some quick exclusions, (i.e. one or more of the actors is not solid or neither has any bounding objects)
            if (!this.Solid() || !a.Solid() || this.TotalCollCount() == 0 || a.TotalCollCount() == 0)
                return false;
            else
            {
                //check for circle collisions
                for (int i = 0; i < this.CollCircCount(); i++)
                {
                    BoundingCircle c1 = new BoundingCircle(this.GetCollCircs()[i].Position + this.GetPosition(), this.GetCollCircs()[i].Radius);

                    for (int j = 0; j < a.CollCircCount(); j++)
                    {
                        BoundingCircle c2 = new BoundingCircle(a.GetCollCircs()[j].Position + a.GetPosition(), a.GetCollCircs()[j].Radius);
                        
                        if (c1.Intersects(c2))
                            return true;
                    }

                    for (int h = i; h < a.CollBoxCount(); h++)
                    {
                        BoundingRectangle b2 = new BoundingRectangle(a.GetCollBoxes()[h].Position - a.GetOrigin() + a.GetPosition(), a.GetCollBoxes()[h].Dimensions);
                        
                        if (c1.Intersects(b2))
                            return true;
                    }
                }

                //check for rectangle collisions
                for (int i = 0; i < this.CollBoxCount(); i++)
                {
                    BoundingRectangle b1 = new BoundingRectangle(this.GetCollBoxes()[i].Position + this.GetPosition() - GetOrigin(), this.GetCollBoxes()[i].Dimensions);
                    for (int j = 0; j < a.CollCircCount(); j++)
                    {
                        BoundingCircle c2 = new BoundingCircle(a.GetCollCircs()[j].Position + a.GetPosition(), a.GetCollCircs()[j].Radius);

                        if (b1.Intersects(c2))
                            return true;
                    }

                    for (int h = i; h < a.CollBoxCount(); h++)
                    {
                        BoundingRectangle b2 = new BoundingRectangle(a.GetCollBoxes()[h].Position + a.GetPosition() - a.GetOrigin(), a.GetCollBoxes()[h].Dimensions);

                        if (b1.Intersects(b2))
                            return true;
                    }
                }
            }

            return false;
        }

        public virtual void Collision(Actor gameActor)
        {
            //do nothing
        }

        public Boolean Solid() { return SolidObject; }
        public void SetSolid(Boolean sol) { SolidObject = sol; }
    }
}
