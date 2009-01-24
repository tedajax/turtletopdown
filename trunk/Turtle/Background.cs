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
                    BgSprites.Add(new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Background\\spacecloud"), new Vector2(x * 64, y * 64)));
                    BgSprites[BgSprites.Count - 1].Layer = 0f;
                    BgSprites[BgSprites.Count - 1].Scale = Vector2.One * (0.6f + (float)BaseGame.Rand.Next(7) / 10f);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Sprite s in BgSprites)
            {
                float x = (float)BaseGame.Rand.Next(-30, 30) / 10f;
                float y = (float)BaseGame.Rand.Next(-30, 30) / 10f;

                s.Velocity = Vector2.Lerp(s.Velocity, new Vector2(x, y), 0.1f);
                s.Position += s.Velocity;
            }
        }
        
        public void Draw()
        {
            for (int i = 0; i < BgSprites.Count; i++)
                BgSprites[i].Draw();
        }
    }
}
