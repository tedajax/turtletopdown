﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Turtle            
{
    enum AmmoTypes //I don't know just some ammo types, can change these later
    {
        Gauss, 
        Photon,
        Laser
    }

    enum ArmorTypes //same as ammo just some stuff
    {
        Alluminum,
        Steel,
        Uranium
    }

    class Player : VisibleActor
    {
        //Stats
        /*AmmoTypes Ammo;
        ArmorTypes Armor;*/

        int RateOfFire;
        int RateOfMissileFire;
        TimeSpan TillNextShot;
        TimeSpan TillNextMissile;
        /*byte BulletSpeed;
        byte BulletDamage;*/

        byte MaxSpeed;
        /*byte Accel;

        byte Hull;
        byte Shield;*/

        Target target;

        PlayerIndex plIndex;

        //Oh shit the player has their own projectile manager, we're getting fancy now
        ProjectileManager PlayerProjectiles;

     

        public Player()
        {
            Initialize();
            plIndex = PlayerIndex.One;
            this.Type = actorType.Player;
        }

        public Player(PlayerIndex pi)
        {
            Initialize();
            plIndex = pi;
        }

        private void Initialize()
        {
            InitCollLists();
            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Ships\\playership"));

            MaxSpeed = 5;

            Origin = new Vector2(31, 31);
            Position = new Vector2(640, 360);

            target = new Target();

            PlayerProjectiles = new ProjectileManager();

            RateOfFire = 250;
            RateOfMissileFire = 500;
            TillNextShot = new TimeSpan(0, 0, 0, 0, RateOfFire);
            TillNextMissile = new TimeSpan(0, 0, 0, 0, RateOfMissileFire);

           // this.CollisionCircles.Add(new BoundingCircle(Vector2.Zero, 32));
            this.CollisionBoxes.Add(new BoundingRectangle(Vector2.Zero, new Vector2(64,64)));
            this.SolidObject = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                BaseGame.Quit();

            foreach (GridSquare g in this.gridSquares)
            {
                foreach (Actor A in g.Actors)
                {
                    if (A.getType() == actorType.Environment)
                    {
                        if (A.CollidesWith(this))
                        {
                         
                            Vector2 newVector = A.GetPosition() - this.Position;
                            newVector.Normalize();
                            this.Velocity = 3 * Vector2.Negate(newVector);
                        }
                    }
                }
            }

            TillNextShot -= gameTime.ElapsedGameTime;
            TillNextMissile -= gameTime.ElapsedGameTime;

            Controls();

            target.Update(gameTime);
            PlayerProjectiles.Update(gameTime);

            Position += Velocity;
            if (Velocity.Length() > 0) Moderator.HasMoved(this);
            ActorSprite.SetPosition(Position);
            ActorSprite.SetRotation(Rotation);
            
        }

        public override void Draw()
        {
           // PlayerProjectiles.Draw();
            ActorSprite.Draw();
            target.Draw();
        }

        private void Controls()
        {
            #if WINDOWS
                
                //up/down movement
                if (BaseGame.Input.GetKeyPressedState(Keys.W) == KeyPressedState.Pressed)
                {
                    VelocityY = MathHelper.Lerp(Velocity.Y, -MaxSpeed, 0.1f);
                }
                if (BaseGame.Input.GetKeyPressedState(Keys.S) == KeyPressedState.Pressed)
                {
                    VelocityY = MathHelper.Lerp(Velocity.Y, MaxSpeed, 0.1f);
                }
                if (BaseGame.Input.GetKeyPressedState(Keys.W) == KeyPressedState.Released && BaseGame.Input.GetKeyPressedState(Keys.S) == KeyPressedState.Released)
                {
                    VelocityY = MathHelper.Lerp(Velocity.Y, 0, 0.1f);
                }
                
                //left/right movement
                if (BaseGame.Input.GetKeyPressedState(Keys.A) == KeyPressedState.Pressed)
                {
                    VelocityX = MathHelper.Lerp(Velocity.X, -MaxSpeed, 0.1f);
                }
                if (BaseGame.Input.GetKeyPressedState(Keys.D) == KeyPressedState.Pressed)
                {
                    VelocityX = MathHelper.Lerp(Velocity.X, MaxSpeed, 0.1f);
                }
                if (BaseGame.Input.GetKeyPressedState(Keys.A) == KeyPressedState.Released && BaseGame.Input.GetKeyPressedState(Keys.D) == KeyPressedState.Released)
                {
                    VelocityX = MathHelper.Lerp(Velocity.X, 0, 0.1f);
                }

                //mdifpx == Mouse difference Position X (gets the difference between the mouse and player position x coordinates
                float mdifpx = target.Position.X - Position.X;
                float mdifpy = target.Position.Y - Position.Y; //same as mdifpx but Y coordinate
                Rotation = (float)Math.Atan2(mdifpy, mdifpx) + MathHelper.PiOver2; // get the angle between the two points
                Rotation = BaseGame.WrapValueRadian(Rotation); //contstrain it to between 0 and 2pi
                
                //Shooting with LMB (left mouse button)
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Shooting();
                }
            
            #elif XBOX
                VelocityX = MathHelper.Lerp(Velocity.X, GamePad.GetState(plIndex).ThumbSticks.Left.X * MaxSpeed, 0.1f); //set the velocity based on the left thumbstick position
                VelocityY = MathHelper.Lerp(Velocity.Y, -GamePad.GetState(plIndex).ThumbSticks.Left.Y * MaxSpeed, 0.1f);

                
                if (Math.Abs(GamePad.GetState(plIndex).ThumbSticks.Right.X) > 0.1f || Math.Abs(GamePad.GetState(plIndex).ThumbSticks.Right.Y) > 0.1f)
                {
                    //use the right thumbstick positions to get the rotation value using the position of the thumbstick as the relative position to the player
                    Rotation = -(float)Math.Atan2((double)GamePad.GetState(plIndex).ThumbSticks.Right.Y, (double)GamePad.GetState(plIndex).ThumbSticks.Right.X) + MathHelper.PiOver2;
                    Rotation = BaseGame.WrapValueRadian(Rotation); //constrain the value
                }
                //Position the target nicely
                target.Position = new Vector2(200 * (float)Math.Cos((double)Rotation - MathHelper.PiOver2) + Position.X, 200 * (float)Math.Sin((double)Rotation - MathHelper.PiOver2) + Position.Y);


                //Shooting with Right Trigger
                if (GamePad.GetState(plIndex).Triggers.Right > 0.4f)
                {
                    Shooting();
                }    
            #else
                //compile time error
                #error Unsupported platform
            #endif
        }

        private void Shooting()
        {
            if (TillNextShot.TotalMilliseconds <= 0)
            {
                Vector2 projPos = new Vector2(32 * (float)Math.Cos((double)Rotation - MathHelper.PiOver2) + Position.X, 32 * (float)Math.Sin((double)Rotation - MathHelper.PiOver2) + Position.Y);
                Vector2 projVel = new Vector2(16 * (float)Math.Cos((double)Rotation - MathHelper.PiOver2), 16 * (float)Math.Sin((double)Rotation - MathHelper.PiOver2));
                PlayerProjectiles.Add(new Projectile(projPos, projVel, Rotation, 3000));

                TillNextShot = new TimeSpan(0, 0, 0, 0, RateOfFire);
            }

            if (TillNextMissile.TotalMilliseconds <= 0)
            {
                int rad = 16;

                for (int posMult = 1; posMult <= 1; posMult++)
                {
                    int dist = rad * posMult;

                    PlayerProjectiles.Add(new Missile(Position + new Vector2((float)Math.Cos(Rotation) * dist, (float)Math.Sin(Rotation) * dist), Rotation - MathHelper.PiOver2));
                    PlayerProjectiles.Add(new Missile(Position + new Vector2((float)Math.Cos(Rotation - MathHelper.Pi) * dist, (float)Math.Sin(Rotation - MathHelper.Pi) * dist), Rotation - MathHelper.PiOver2));
                }

                TillNextMissile = new TimeSpan(0, 0, 0, 0, RateOfMissileFire);
            }
        }

        public override void Collision(Actor gameActor)
        {
            if (gameActor.getType() == actorType.Environment)
            {
               
            }
        }

        

        public List<Projectile> GetProjectileList() { return PlayerProjectiles.GetProjectiles(); }
    }
}
