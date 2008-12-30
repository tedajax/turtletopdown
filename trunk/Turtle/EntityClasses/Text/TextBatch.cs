using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class TextBatch
    {
        List<Text> TextList;
        SpriteFont textFont;
        Color textColor;

        public TextBatch(SpriteFont font, Color col)
        {
            textFont = font;
            textColor = col;
            TextList = new List<Text>();
        }

        public void AddText(Text t)
        {
            TextList.Add(t);
        }

        public void AddText(string t)
        {
            TextList.Add(new Text(t, textColor, textFont));
        }

        public void AddText(string t, Vector2 pos)
        {
            TextList.Add(new Text(t, pos, textColor, textFont));
        }

        public void InsertText(int index, Text t)
        {
            TextList.Insert(index, t);
        }

        public void InsertText(int index, string t)
        {
            TextList.Insert(index, new Text(t, textColor, textFont));
        }

        public void InsertText(int index, string t, Vector2 pos)
        {
            TextList.Insert(index, new Text(t, pos, textColor, textFont));
        }

        public void ClearTextList() { TextList.Clear(); }

        public int FindTextIndex(string textstr)
        {
            for (int i = 0; i < TextList.Count; i++)
            {
                if (TextList[i].TextStr.Equals(textstr))
                    return i;
            }

            return -1;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < TextList.Count; i++)
            {
                TextList[i].Update(gameTime);
            }
        }

        public void Draw()
        {
            for (int i = 0; i < TextList.Count; i++)
            {
                TextList[i].Draw();
            }
        }

        public List<Text> GetTextList() { return TextList; }
    }
}
