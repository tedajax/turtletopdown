using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    class Enemy : Actor
    {
        Sprite EnemySprite;

        public Enemy(Vector2 pos)
        {
            //The base enemy will load the really retarded sprite I made
            EnemySprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Enemies\\basicenemy"));

            Position = pos;
            Origin = new Vector2(31, 31);

            SolidObject = true;

            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 32));
        }

        public override void Update(GameTime gameTime)
        {
            //make the enemy just spin in circles
            Rotation += MathHelper.Pi / 16;
            Rotation = BaseGame.WrapValueRadian(Rotation);

            EnemySprite.SetPosition(Position);
            EnemySprite.SetRotation(Rotation);
        }

        public override void Draw()
        {
            EnemySprite.Draw();
        }
    }
}
