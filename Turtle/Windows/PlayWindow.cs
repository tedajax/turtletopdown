using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class PlayWindow : Window
    {
        Player GamePlayer;
        Background GameBG;

        List<Enemy> SomeEnemies;
        int ECount;
        
        public PlayWindow()
        {
            Mode = WindowMode.Active;
            Name = "PlayWindow";

            GamePlayer = new Player();
            GameBG = new Background();

            SomeEnemies = new List<Enemy>();
            //make 50 enemies spread them out across the level in random spots
            for (int i = 0; i < 500; i++)
            {
                SomeEnemies.Add(new Enemy(BaseGame.randomPoint(-1920, 1920)));
            }
            ECount = SomeEnemies.Count;
        }

        protected override void Initialize()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            //make sure to erase this when proper enemy handling is done
            for (int i = 0; i < ECount; i++)
            {
                SomeEnemies[i].Update(gameTime);
                for (int b = 0; b < GamePlayer.GetProjectileList().Count; b++)
                {
                    Projectile p = GamePlayer.GetProjectileList()[b];
                    if (p.CollidesWith(SomeEnemies[i]))
                    {
                        SomeEnemies.RemoveAt(i);
                        GamePlayer.GetProjectileList().RemoveAt(b);
                        ECount--;
                        if (i > ECount) i = ECount;
                    }
                }
            }

            GamePlayer.Update(gameTime);

            BaseGame.Camera.Position = Vector2.Lerp(BaseGame.Camera.Position, GamePlayer.Position - new Vector2(640, 360), 0.06f);
        }

        protected override void Draw()
        {
            GameBG.Draw();
            
            for (int i = 0; i < SomeEnemies.Count; i++)
            {
                SomeEnemies[i].Draw();
            }

            GamePlayer.Draw();
        }
    }
}
