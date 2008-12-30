using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class FlashingText : Text
    {
        int alphamin, alphamax;
        int alphadelta;
        int alphavelocity;
        int alpha;

        public FlashingText()
        {
            textStr = "";
            textCol = Color.White;
            textFont = BaseGame.GetDebugFont();

            Scale = Vector2.One;

            alphamin = 127;
            alphamax = 255;
            alphadelta = 2;
            alphavelocity = -alphadelta;
            alpha = alphamax;
        }

        public FlashingText(int amin, int amax, int adelta)
        {
            textStr = "";
            textCol = Color.White;
            textFont = BaseGame.GetDebugFont();

            Scale = Vector2.One;

            alphamin = amin;
            alphamax = amax;
            alphadelta = adelta;
            alphavelocity = -adelta;
            alpha = amax;
        }

        public FlashingText(string t, int amin, int amax, int adelta)
        {
            textStr = t;
            textCol = Color.White;
            textFont = BaseGame.GetDebugFont();

            Scale = Vector2.One;

            alphamin = amin;
            alphamax = amax;
            alphadelta = adelta;
            alphavelocity = -adelta;
            alpha = amax;
        }

        public FlashingText(string t, Vector2 pos, int amin, int amax, int adelta)
        {
            textStr = t;
            textCol = Color.White;
            textFont = BaseGame.GetDebugFont();

            position = pos;

            Scale = Vector2.One;

            alphamin = amin;
            alphamax = amax;
            alphadelta = adelta;
            alphavelocity = -adelta;
            alpha = amax;
        }

        public FlashingText(string t, Vector2 pos, Color c, SpriteFont f, int amin, int amax, int adelta)
        {
            textStr = t;
            textCol = c;
            textFont = f;
            position = pos;

            Scale = Vector2.One;

            alphamin = amin;
            alphamax = amax;
            alphadelta = adelta;
            alphavelocity = -adelta;
            alpha = amax;
        }

        public FlashingText(string t, Color c, int amin, int amax, int adelta)
        {
            textStr = t;
            textCol = c;
            textFont = BaseGame.GetDebugFont();

            Scale = Vector2.One;

            alphamin = amin;
            alphamax = amax;
            alphadelta = adelta;
            alphavelocity = -adelta;
            alpha = amax;
        }

        public FlashingText(string t, Color c, SpriteFont f, int amin, int amax, int adelta)
        {
            textStr = t;
            textCol = c;
            textFont = f;

            Scale = Vector2.One;
            alphamin = amin;
            alphamax = amax;
            alphadelta = adelta;
            alphavelocity = -adelta;
            alpha = amax;
        }

        public override void Update(GameTime gameTime)
        {
            alpha += alphavelocity;
            if (alpha < alphamin && alphavelocity < 0)
                alphavelocity = alphadelta;
            if (alpha > alphamax && alphavelocity > 0)
                alphavelocity = -alphadelta;

            if (alpha < 0) alpha = 0;
            if (alpha > 255) alpha = 255;

            textCol = new Color(textCol.R, textCol.G, textCol.B, (byte)alpha);
        }
    }
}
