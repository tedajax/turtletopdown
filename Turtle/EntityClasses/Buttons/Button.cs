using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    class Button : Entity
    {
        protected Rectangle selectionRectangle;
        protected bool selected;

        public Rectangle SelectionRectangle
        {
            get { return selectionRectangle; }
            set { selectionRectangle = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
    }
}
