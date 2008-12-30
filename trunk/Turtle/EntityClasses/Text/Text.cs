using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    public class Text : Entity
    {
        protected string textStr;
        protected Color textCol;
        protected SpriteFont textFont;

        public Text()
        {
            textStr = "";
            textCol = Color.White;
            textFont = BaseGame.GetDebugFont();

            Scale = Vector2.One;
        }

        public Text(string t)
        {
            textStr = t;
            textCol = Color.White;
            textFont = BaseGame.GetDebugFont();

            Scale = Vector2.One;
        }

        public Text(string t, Vector2 pos)
        {
            textStr = t;
            textCol = Color.White;
            textFont = BaseGame.GetDebugFont();

            position = pos;

            Scale = Vector2.One;
        }

        public Text(string t, Vector2 pos, Color c, SpriteFont f)
        {
            textStr = t;
            textCol = c;
            textFont = f;
            position = pos;

            Scale = Vector2.One;
        }

        public Text(string t, Color c)
        {
            textStr = t;
            textCol = c;
            textFont = BaseGame.GetDebugFont();

            Scale = Vector2.One;
        }

        public Text(string t, Color c, SpriteFont f)
        {
            textStr = t;
            textCol = c;
            textFont = f;

            Scale = Vector2.One;
        }

        public string TextStr
        {
            get { return textStr; }
            set { textStr = value; }
        }

        public Color TextCol
        {
            get { return textCol; }
            set { textCol = value; }
        }

        public Color getTextCol() { return textCol; }

        public override void Draw()
        {
            BaseGame.GetSpriteBatch().DrawString(textFont,
                                                 textStr,
                                                 Position * BaseGame.GetResScaler(),
                                                 textCol,
                                                 Rotation,
                                                 Origin,
                                                 Scale * BaseGame.GetResScaler(),
                                                 SpriteEffects.None,
                                                 0);
        }
    }
}
