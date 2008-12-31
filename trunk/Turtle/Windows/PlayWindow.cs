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
        
        public PlayWindow()
        {
            Mode = WindowMode.Active;
            Name = "PlayWindow";

            GamePlayer = new Player();

            
        }

        protected override void Initialize()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            GamePlayer.Update(gameTime);
        }

        protected override void Draw()
        {
            GamePlayer.Draw();
        }
    }
}
