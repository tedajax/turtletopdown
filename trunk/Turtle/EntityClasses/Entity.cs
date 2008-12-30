using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Turtle
{
    public abstract class Entity
    {
        protected Vector2 position; //2D position
        protected Vector2 velocity; //Change position by this much
        protected Vector2 acceleration;
        protected Vector2 origin; //Where to rotate around on the entity
        protected Vector2 scale; //(1, 1) would be default scaling, (2, 2) would be twice as large, (0.5f, 0.5f) half size
        protected Vector2 maxVelocity;
        protected float rotation; //angle, top-down rotation in radians
        protected bool lockToScreen;
        protected bool restrictVelocity;

        protected Vector2 ResScaler; //Scaler for position, scale, and velocity based on resolution

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw() { }

        //Returns position multiplied by resolution scaler
        public Vector2 ResPosition() { return (Position - ((!lockToScreen) ? BaseGame.Camera.Position : Vector2.Zero)) * BaseGame.GetResScaler(); }
        public Vector2 ResVelocity() { return Velocity * BaseGame.GetResScaler(); }
        public Vector2 ResOrigin() { return Origin * BaseGame.GetResScaler(); }
        public Vector2 ResScale() { return Scale * BaseGame.GetResScaler(); }
        //no need to scale rotation

        //Accessors
        public Vector2 GetPosition() { return Position; }
        public Vector2 GetVelocity() { return Velocity; }
        public Vector2 GetOrigin() { return Origin; }
        public Vector2 GetScale() { return Scale; }
        public float GetRotation() { return Rotation; }
        public Vector2 GetResScaler() { return ResScaler; } //why not
        public Vector2 GetAcceleration() { return acceleration; }

        //Mutators
        public void SetPosition(Vector2 pos) { Position = pos; }
        public void SetVelocity(Vector2 vel) { Velocity = vel; }
        public void SetOrigin(Vector2 orgn) { Origin = orgn; }
        public void SetScale(Vector2 scl) { Scale = scl; }
        public void SetRotation(float rot) { Rotation = rot; }
        public void SetResScaler(Vector2 rscl) { ResScaler = rscl; } //again, why not
        public void SetAcceleration(Vector2 acl) { acceleration = acl; }

        public bool LockToScreen
        {
            get { return lockToScreen; }
            set { lockToScreen = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float PositionX
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float PositionY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public float VelocityX
        {
            get { return velocity.X; }
            set { velocity.X = value; }
        }

        public float VelocityY
        {
            get { return velocity.Y; }
            set { velocity.Y = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public float OriginX
        {
            get { return origin.X; }
            set { origin.X = value; }
        }

        public float OriginY
        {
            get { return origin.Y; }
            set { origin.Y = value; }
        }

        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public float ScaleX
        {
            get { return scale.X; }
            set { scale.X = value; }
        }

        public float ScaleY
        {
            get { return scale.Y; }
            set { scale.Y = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        public float AccelerationX
        {
            get { return acceleration.X; }
            set { acceleration.X = value; }
        }

        public float AccelerationY
        {
            get { return acceleration.Y; }
            set { acceleration.Y = value; }
        }

        public Vector2 MaxVelocity
        {
            get { return maxVelocity; }
            set { maxVelocity = value; }
        }

        public float MaxVelocityX
        {
            get { return maxVelocity.X; }
            set { maxVelocity.X = value; }
        }

        public float MaxVelocityY
        {
            get { return maxVelocity.Y; }
            set { maxVelocity.Y = value; }
        }

        public bool RestrictVelocity
        {
            get { return restrictVelocity; }
            set { restrictVelocity = value; }
        }
    }
}
