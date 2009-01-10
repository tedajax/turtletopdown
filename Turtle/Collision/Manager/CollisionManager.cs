using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    public class GridSquare
    {
        public List<Actor> Actors;
        public Vector2 Key;

        public GridSquare(Vector2 Key)
        {
            Actors = new List<Actor>();
            this.Key = Key;
        }
        

    }

    public class CollisionManager
    {
        /// <summary>
        /// The Collison Manager partitions the world into a dictionary of grid sqares, allow us to perform fewer collision checks
        /// for each object
        /// </summary>
        Dictionary<Vector2, GridSquare> theWorld;
        int partitionSize;
        
        /// <summary>
        /// Initialize the Collision Manager
        /// </summary>
        /// <param name="WorldSize"></param>
        /// <param name="PartitionSize"></param>
        public CollisionManager(int PartitionSize)
        {
            this.partitionSize = PartitionSize;
            theWorld = new Dictionary<Vector2, GridSquare>();
         
        }
        /// <summary>
        /// This method allows the collision manager to run one cycle of execution
        /// This consists of adding and removing Actors, as well as checking collision between actors
        /// and firing events if Collision has Occured
        /// </summary>
        /// <param name="checkCollisions">if this parameter is set to true, the Manager will run collision between Actors and fire events occordingly</param>
        public void Update(bool checkCollisions)
        {
            ///Check all the Actors that need to be added
            while (Moderator.toAdd.Count > 0)
            {
                addActor(Moderator.toAdd.Pop());
            }
            //Check all the Actors that need to be removed
            while (Moderator.toRemove.Count > 0)
            {
                Actor gameActor = Moderator.toRemove.Pop();
                for (int i = 0; i < gameActor.GridSquares.Count; i++)
                {
                    gameActor.GridSquares[i].Actors.Remove(gameActor);
                    if (gameActor.GridSquares[i].Actors.Count == 0)
                    {
                        theWorld.Remove(gameActor.GridSquares[i].Key);
                    }
                        
                }
                gameActor.clearGridSquares();
               // gameActor.Dispose();
            }
            //Check all the Actors that need to be updated
            while (Moderator.toUpdate.Count > 0)
            {
                Actor gameActor = Moderator.toUpdate.Pop();
                for (int i = 0; i < gameActor.GridSquares.Count; i++)
                {
                    gameActor.GridSquares[i].Actors.Remove(gameActor);
                }
                gameActor.clearGridSquares();
                addActor(gameActor);
            }

        }

        /// <summary>
        /// Helper function that checks if a point is inside the bounds of a rectangle
        /// </summary>
        /// <param name="Point">A Single Point in world space</param>
        /// <param name="gameRectangle">A Rectangle defined in world space</param>
        /// <returns>true if the point is inside or touching the rectangle false otherwise</returns>
        private bool containsRect(Vector2 Point, Rectangle gameRectangle)
        {
            if (Point.X >= gameRectangle.X - gameRectangle.Width && Point.X <= gameRectangle.X + gameRectangle.Width)
            {
                if (Point.Y >= gameRectangle.Y - gameRectangle.Height && Point.Y <= gameRectangle.Y + gameRectangle.Height)
                {
                    return true;
                }
            }
            return false;


        }
       
        /// <summary>
        /// Helper function that adds an actor into the Collision System based on a Rectangle
        /// </summary>
        /// <param name="gameActor">actor that needs to be added into the Collision Manager</param>
        /// <param name="gameRectangle">Rectangle that is used to get the Grid Squares</param>
        private void addRect(Actor gameActor, Rectangle gameRectangle)
        {
            //Start at the Top Left
            Vector2 Point = new Vector2(gameRectangle.Left, gameRectangle.Top);
            addActor(gameActor, findGridSquare(Point));

            //Add the Actor into all the gridsquares that touch the current game Rectangle
            while (containsRect(Point, gameRectangle))
            {
                while (containsRect(Point + new Vector2(partitionSize,0), gameRectangle))
                {
                    //Move to the Right by 1 Parition
                    Point.X += partitionSize;
                    addActor(gameActor, findGridSquare(Point));
                }
                Point.X = gameRectangle.Left;
                Point.Y += partitionSize;

            }



        }

        /// <summary>
        /// Helper Function, Adds the Actor into a GridSquare making sure that the Actor isn't already in it.
        /// Also puts the Grid Square into the Actors list of Squares.
        /// </summary>
        /// <param name="gameActor">Actor that needs to be added</param>
        /// <param name="gridSquare">Grid Square to be added into</param>
        private void addActor(Actor gameActor, GridSquare gridSquare)
        {
            if (!gridSquare.Actors.Contains(gameActor))
            {
                gridSquare.Actors.Add(gameActor);
                gameActor.addGridSquare(gridSquare);
            }
        }

        /// <summary>
        /// Adds an Actor into the gameWorld by finding which GridSquares it belongs to and adding the Actor into those Squares
        /// </summary>
        /// <param name="gameActor">Actor to be added</param>
        public void addActor(Actor gameActor)
        {
            //first check if the Actor actually has Collision Boxes
            if (gameActor.GetCollBoxes().Count == 0 && gameActor.GetCollCircs().Count == 0)
            {
                //If They don't, just place the actor into a grid square based on the current location
                addActor(gameActor, findGridSquare(gameActor.Position));
                
            }
            else
            {
                //loop through the Collision Boxes
                List<BoundingRectangle> CollBoxes = gameActor.GetCollBoxes();
                for (int i = 0; i < gameActor.GetCollBoxes().Count; i++)
                {
                    BoundingRectangle BoundingRect = CollBoxes[i];
                    Rectangle NormRect = new Rectangle((int)BoundingRect.X+(int)gameActor.Position.X, (int)BoundingRect.Y+(int)gameActor.Position.Y, (int)BoundingRect.Width, (int)BoundingRect.Height);
                    addRect(gameActor, NormRect);
                }
                //Loop through the Collision Circles
                List<BoundingCircle> CollCircs = gameActor.GetCollCircs();
                for (int i = 0; i < gameActor.GetCollCircs().Count; i++)
                {
                    BoundingCircle Circ = CollCircs[i];
                    Rectangle NormRect = new Rectangle((int)Circ.Position.X+(int)gameActor.Position.X, (int)Circ.Position.Y+(int)gameActor.Position.Y, (int)Circ.Radius * 2, (int)Circ.Radius * 2);
                    addRect(gameActor, NormRect);
            

                }
          }

        }

        /// <summary>
        /// Takes a world space position value and returns a Grid Square corresponding to that point. If no Grid Square exists, one is created.
        /// </summary>
        /// <param name="Position">Position coordinate in world space</param>
        /// <returns>A Grid Square correspoinding to the Position argument</returns>
        public GridSquare findGridSquare(Vector2 Position)
        {
            float YPos = Position.Y / partitionSize;
            YPos = (float)Math.Floor(YPos);
            float XPos = Position.X / partitionSize;
            XPos = (float)Math.Floor(XPos);

            if (!theWorld.ContainsKey(new Vector2(XPos, YPos)))
            {
                GridSquare newSquare = new GridSquare(new Vector2(XPos,YPos));
                theWorld.Add(new Vector2(XPos, YPos), newSquare);
                return newSquare;

            }
            else
            {
                return theWorld[new Vector2(XPos, YPos)];
            }

       }

        /// <summary>
        /// Returns grid squares with a range of given input. This function will also return the input inside the list
        /// </summary>
        /// <param name="input">Input Grid Square to use a starting point for the search</param>
        /// <param name="Range">Integer Value to use a range value. A Value of 1 will return all grid squares within (1 * parttion size) of the input</param>
        /// <returns>List of Grid Squares (including input) that are within the range of input square</returns>
        public List<GridSquare> findGridSquares(GridSquare input, int Range)
        {
            Vector2 StartPos = input.Key;
            List<GridSquare> output = new List<GridSquare>();
            if (Range > 0)
            {
                //Start at the Top Left
                Vector2 CurrentPos = StartPos + new Vector2(-(int)Range, -(int)Range);
                for (int i = 0; i <= Range*2; i++)
                {
                    for (int j = 0; j <= Range*2; j++)
                    {
                        if (theWorld.ContainsKey(CurrentPos))
                        {
                            output.Add(theWorld[CurrentPos]);
                        }
                        CurrentPos += new Vector2(1, 0);
                       
                    }
                    CurrentPos.X = StartPos.X -(int)Range;
                    CurrentPos.Y += 1;
                }
            }
            return output;
        }


    }
}
