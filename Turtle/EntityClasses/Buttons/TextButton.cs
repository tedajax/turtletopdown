using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class TextButton : Button
    {
        protected Text buttonText;
        protected Color hoverColor;
        protected Color activeColor;

        public Text ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; }
        }

        public string ButtonString
        {
            get { return buttonText.TextStr; }
            set { buttonText.TextStr = value; }
        }

        public Color ButtonColor
        {
            get { return buttonText.TextCol; }
            set { buttonText.TextCol = value; }
        }

        public Color HoverColor
        {
            get { return hoverColor; }
            set { hoverColor = value; }
        }

        public Color ActiveColor
        {
            get { return activeColor; }
            set { activeColor = value; }
        }

        public TextButton(string txt, Vector2 pos)
        {
            buttonText = new Text(txt);
            Position = pos;
            buttonText.SetPosition(pos);

            hoverColor = Color.Green;
            activeColor = Color.Red;
        }

        public TextButton()
        {
            buttonText = new Text("");
            Position = Vector2.Zero;
            buttonText.SetPosition(Position);

            hoverColor = Color.Green;
            activeColor = Color.Red;
        }

        public override void Draw()
        {
            buttonText.SetPosition(Position);
            buttonText.SetRotation(Rotation);
            buttonText.Draw();
        }
    }
}
