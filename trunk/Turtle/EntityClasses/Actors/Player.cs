using System;
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
        ArmorTypes Armor;

        byte RateOfFire;
        byte BulletSpeed;
        byte BulletDamage;*/

        byte MaxSpeed;
        /*byte Accel;

        byte Hull;
        byte Shield;*/

        Target target;

        public Player()
        {
            ActorSprite = new Sprite(BaseGame.GetContent().Load<Texture2D>("Images\\Ships\\playership"));

            MaxSpeed = 5;

            Origin = new Vector2(31, 31);
            Position = new Vector2(640, 360);

            target = new Target();
        }

        public override void Update(GameTime gameTime)
        {
            Controls();

            target.Update(gameTime);

            Position += Velocity;
            ActorSprite.SetPosition(Position);
            ActorSprite.SetRotation(Rotation);
        }

        public override void Draw()
        {
            base.Draw();
            target.Draw();
        }

        private void Controls()
        {
            #if WINDOWS
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
            float mdifpx = Mouse.GetState().X - Position.X;
            float mdifpy = Mouse.GetState().Y - Position.Y; //same as mdifpx but Y coordinate
            Rotation = (float)Math.Atan2(mdifpy, mdifpx) + MathHelper.PiOver2;
            Rotation = BaseGame.WrapValueRadian(Rotation);

            #elif XBOX
                //xbox controls

            #else
                //compile time error
                #error Unsupported platform

            #endif
        }
    }
}
