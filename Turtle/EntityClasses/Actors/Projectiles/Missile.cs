using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Missile : Projectile
    {
        Vector2 targetVector; //Where the missile should fly to
        bool lockedOn; //If true will travel towards the target vector otherwise will travel in a straight line

        /// <summary>
        /// Parameter free constructor
        /// Not recommended for use
        /// </summary>
        public Missile()
        {

        }
    }
}
