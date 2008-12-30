using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    public class Camera2D
    {
        Vector2 position;
        Vector2 posAdd;

        public Camera2D() { position = Vector2.Zero; posAdd = Vector2.Zero; }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 PositionAdd
        {
            get { return posAdd; }
            set { posAdd = value; }
        }

        public void Reset() { position = Vector2.Zero; posAdd = Vector2.Zero; }
        public void Shake(int mag)
        {
            posAdd = BaseGame.randomPoint(-mag, mag);
        }
        public void ClearShake() { posAdd = Vector2.Zero; }
    }
}
