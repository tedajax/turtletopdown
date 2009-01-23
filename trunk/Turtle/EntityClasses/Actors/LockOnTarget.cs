using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class LockOnTarget : VisibleActor
    {
        List<Enemy> targetList;

        public LockOnTarget()
        {
            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\lockontarget"));
            origin = new Vector2(63, 63);

            this.Type = actorType.Misc;

            targetList = new List<Enemy>();

            SolidObject = true;
            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 64));
            Moderator.toAdd.Push(this);
        }

        public override void Update(GameTime gameTime)
        {
            #if WINDOWS
                Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) + BaseGame.Camera.Position;
            #endif

            Moderator.HasMoved(this);

            base.Update(gameTime);
        }

        public void Lock()
        {
            foreach (GridSquare g in gridSquares)
            {
                foreach (Actor a in g.Actors)
                {
                    if (targetList.Count <= 20)
                    {
                        if (a.getType() == actorType.Enemy)
                        {
                            if (CollidesWith(a))
                            {
                                Enemy e = (Enemy)a;
                                e.Targeted = true;
                                AddTarget(e);
                            }
                        }
                    }
                }
            }
        }

        private void AddTarget(Enemy e)
        {
            if (!targetList.Contains(e))
                targetList.Add(e);
        }

        public List<Enemy> GetTargetList()
        {
            return targetList; 
        }

        public void SetHidden(bool h) { ActorSprite.SetHidden(h); }

        public void ClearTargets() { targetList.Clear(); }
    }
}
