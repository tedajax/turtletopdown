using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    /// <summary>
    /// Game World class allows each individual level to instatiate a gameworld which will hold together the actors, collision detection, particle generation, etc
    /// 
    /// </summary>
    class GameWorld
    {
        /// <summary>
        /// Specific list of gameplayers for quick access 
        /// </summary>
        List<Player> GamePlayers;
        /// <summary>
        /// Detects Collisions between Actors, also allows them to move around the game world.
        /// </summary>
        CollisionManager CollisionManager;

        /// <summary>
        /// List of Actors that don't have a specified manager but still need update calls
        /// </summary>
        List<Actor> gameActors;
        //TODO: ADD Particle System
        //TODO: ADD ENEMY MANGMENT
        //TODO: ADD WORLD MANAGMENT (Background, etc)

        Background GameBG;

        public static Vector2[] PlayerPositions;

        public GameWorld()
        {
           
            GamePlayers = new List<Player>();
            gameActors = new List<Actor>();
            CollisionManager = new CollisionManager(500);
            GamePlayers.Add(new Player());
            GameBG = new Background();
            Moderator.Initialize();
            Moderator.toAdd.Push(GamePlayers[0]);

            PlayerPositions = new Vector2[GamePlayers.Count];
        }

        public void Update(GameTime gameTime)
        {
            foreach (Player p in GamePlayers)
            {
                p.Update(gameTime);
            }

            PlayerPositions[0] = GamePlayers[0].Position;

            Stack<Actor> disposal = new Stack<Actor>();
            foreach (Actor a in gameActors)
            {
                if (a.IsDisposed())
                    disposal.Push(a);
                else
                    a.Update(gameTime);
            }
            while (disposal.Count > 0)
            {
                gameActors.Remove(disposal.Pop());
            }

            CollisionManager.Update(true);
            BaseGame.Camera.Position = Vector2.Lerp(BaseGame.Camera.Position, GamePlayers[0].Position - new Vector2(640, 360), 0.06f);

        }

        public void Draw()
        {
            GameBG.Draw();

            //Find only the Actors near the player and draw them (including the player)
            Vector2 Position = GamePlayers[0].Position;
            GridSquare Start = CollisionManager.findGridSquare(Position);
            List<GridSquare> allSquares = CollisionManager.findGridSquares(Start, 8);
            foreach (GridSquare g in allSquares)
            {
                foreach (Actor v in g.Actors)
                {
                    v.Draw();
                }
            }
        }


        public void addActor(Actor gameActor)
        {
            this.gameActors.Add(gameActor);
        }

    }
}
