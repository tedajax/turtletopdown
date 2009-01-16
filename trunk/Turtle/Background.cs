using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Background
    {
        List<Sprite> BgSprites;

        public Background()
        {
            BgSprites = new List<Sprite>();

            for (int x = -30; x < 30; x++)
            {
                for (int y = -30; y < 30; y++)
                {
                    BgSprites.Add(new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Background\\sand"), new Vector2(x * 64, y * 64)));
                    BgSprites[BgSprites.Count - 1].Layer = 0f;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < BgSprites.Count; i++)
                BgSprites[i].Draw();
        }
    }
}
