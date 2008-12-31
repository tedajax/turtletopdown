using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    public abstract class Actor : Entity
    {
        protected List<BoundingRectangle> CollisionBoxes;
        protected List<BoundingCircle> CollisionCircles;
        protected Boolean SolidObject;

        protected void InitCollLists()
        {
            CollisionBoxes = new List<BoundingRectangle>();
            CollisionCircles = new List<BoundingCircle>();
        }

        public override void Update(GameTime gameTime)
        {
            throw new Exception("This method should not be used!");
        }

        public override void Draw()
        {
            throw new Exception("This method should not be used!");
        }
                
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
                        BoundingCircle c2 = new BoundingCircle(a.GetCollCircs()[j].Position - a.GetOrigin() + a.GetPosition(), a.GetCollCircs()[j].Radius);

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

        public Boolean Solid() { return SolidObject; }
        public void SetSolid(Boolean sol) { SolidObject = sol; }
    }
}
