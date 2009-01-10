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

        int deathState; //0 alive, 1 dieing, 2 set the destroy to true
        bool destroy;

        Disintegrate enemyDeath;

        public Enemy(Vector2 pos)
        {
            //The base enemy will load the really retarded sprite I made
            EnemySprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Enemies\\basicenemy"));

            Position = pos;
            Origin = new Vector2(31, 31);

            SolidObject = true;

            InitCollLists();
            CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 32));

            EnemySprite.SetColor(new Color((byte)BaseGame.Rand.Next(256), (byte)BaseGame.Rand.Next(256), (byte)BaseGame.Rand.Next(256)));
            Moderator.toAdd.Push(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (deathState == 0)
            {
                //make the enemy just spin in circles
                Rotation += MathHelper.Pi / 16;
                Rotation = BaseGame.WrapValueRadian(Rotation);
            }
            else if (deathState == 1)
            {
                enemyDeath.Update(gameTime);
                if (enemyDeath.Destroy)
                    destroy = true;
            }
            EnemySprite.SetPosition(Position);
            EnemySprite.SetRotation(Rotation);
        }

        public override void Draw()
        {
            if (deathState == 0)
                EnemySprite.Draw();
            else if (deathState == 1)
                enemyDeath.Draw();
        }

        public void KillEnemy()
        {
            if (deathState == 0)
            {
                deathState = 1;
                enemyDeath = new Disintegrate(EnemySprite.GetImage(), Position, 4);
                enemyDeath.ParticleColor = EnemySprite.SColor;
            }
        }

        public bool Destroy
        {
            get { return destroy; }
            set { destroy = value; }
        }

        public override void Collision(Actor gameActor)
        {
            KillEnemy();
            Moderator.toRemove.Push(this);
        }
    }
}
