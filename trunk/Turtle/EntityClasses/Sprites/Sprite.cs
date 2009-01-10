using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    public class Sprite : Entity
    {
        protected Texture2D SpriteImage;
        protected Boolean Hidden;
        protected Color SpriteColor;

        public Sprite()
        {
            SpriteImage = null;
            Hidden = false;
            SpriteColor = Color.White;
         
            Scale = Vector2.One;
        }

        /// <summary>
        /// Make new sprite with specified texture2d, set hidden to false and color to white
        /// </summary>
        /// <param name="img">Loaded Texture2D content</param>
        public Sprite(Texture2D img)
        {
            SpriteImage = img;
            Hidden = false;
            SpriteColor = Color.White;

            Initialize();
        }

        public Sprite(Texture2D img, Vector2 pos)
        {
            SpriteImage = img;
            Hidden = false;
            SpriteColor = Color.White;
            Position = pos;

            Initialize();
        }

        /// <summary>
        /// Make new sprite with specified texture2d and specify hidden status
        /// set color to white
        /// </summary>
        /// <param name="img">Loaded Texture2D content</param>
        /// <param name="hide">If false, will draw, if True will not draw</param>
        public Sprite(Texture2D img, Boolean hide)
        {
            SpriteImage = img;
            Hidden = hide;
            SpriteColor = Color.White;

            Initialize();
        }

        /// <summary>
        /// Make new sprite with specified texture2d and specify hidden status
        /// set color to specified color
        /// </summary>
        /// <param name="img">Loaded Texture2D content</param>
        /// <param name="hide">If false, will draw, if True will not draw</param>
        /// <param name="col">If white will draw normally, otherwise will look different :D</param>
        public Sprite(Texture2D img, Boolean hide, Color col)
        {
            SpriteImage = img;
            Hidden = hide;
            SpriteColor = col;

            Initialize();
        }

        protected void Initialize()
        {
            Origin = new Vector2(SpriteImage.Width / 2, SpriteImage.Height / 2);
            Scale = Vector2.One;
        }

        public override void Update(GameTime gameTime)
        {
             throw new Exception("This method should not be used!");
        }

        /// <summary>
        /// Draws the sprite, simple
        /// </summary>
        public override void Draw()
        {
            if (!Hidden)
            {
                
                BaseGame.GetSpriteBatch().Draw(SpriteImage,         //The image to use
                                               ResPosition() + BaseGame.Camera.PositionAdd,       //Use the entity position 
                                               null,                //We don't need a source rectangle (no animation :'< )
                                               SpriteColor,         //The color of the sprite
                                               Rotation,            //Rotation in radians
                                               Origin,         //Origin should be set to the middle
                                               ResScale(),          //Scale of the image
                                               SpriteEffects.None,  //No sprite effects
                                               0);                  //Layer depth thing is useless for our purposes
            }
        }

        //Accessors
        public Boolean IsHidden() { return Hidden; }
        public Color GetSpriteColor() { return SpriteColor; }

        //Mutators
        public void SetHidden(Boolean hide) { Hidden = hide; }
        public void SetColor(Color col) { SpriteColor = col; }

        public Color SColor
        {
            get { return SpriteColor; }
            set { SpriteColor = value; }
        }

        public int Width
        {
            get { return SpriteImage.Width; }
        }
        public int Height
        {
            get { return SpriteImage.Height; }
        }

        public Texture2D GetImage() { return SpriteImage; }
    }
}
