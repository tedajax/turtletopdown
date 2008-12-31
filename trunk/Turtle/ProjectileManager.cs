using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    class ProjectileManager
    {
        private List<Projectile> ProjectileList;

        public ProjectileManager()
        {
            ProjectileList = new List<Projectile>();
        }

        public void Add(Projectile p)
        {
            ProjectileList.Add(p);
        }

        public void Update(GameTime gameTime)
        {
            //iterate through every projectile in the list, update it, check if the destroy flag is true, remove it
            for (int p = 0; p < ProjectileList.Count; p++)
            {
                Projectile proj = ProjectileList[p];
                proj.Update(gameTime);
                if (proj.Destroy)
                    ProjectileList.Remove(proj);
            }
        }

        public void Draw()
        {
            //iterate through call all projectile draw methods
            for (int p = 0; p < ProjectileList.Count; p++)
            {
                ProjectileList[p].Draw();
            }
        }

        public List<Projectile> GetProjectiles() { return ProjectileList; }
    }
}
