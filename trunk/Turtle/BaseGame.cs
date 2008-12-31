using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Turtle
{
    public class ResolutionManager
    {
        Vector2 Resolution;
        Vector2 BaseResolution;
        Vector2 Scaler;

        public ResolutionManager(Vector2 res)
        {
            Resolution = res;
            BaseResolution = new Vector2(1280, 720);

            Scaler = Resolution / BaseResolution;
        }

        public Vector2 GetResolution() { return Resolution; }
        public int GetResX() { return (int)Resolution.X; }
        public int GetResY() { return (int)Resolution.Y; }
        public Vector2 GetResScaler() { return Scaler; }
    }

    public class BaseGame : Microsoft.Xna.Framework.Game
    {
        private static GraphicsDeviceManager graphics;
        private static SpriteBatch spriteBatch;
        private static ContentManager ContMan;
        private static InputManager GameInput;
        private static ResolutionManager GameRes;
        private static WindowManager WinMan;
        private static SpriteFont DebugFont;
        private static Random rand;
        private static Camera2D camera;

        public static PlayerIndex PlayerOneIndex;

        private static bool quit;

        public BaseGame()
        {
            graphics = new GraphicsDeviceManager(this);

            GameRes = new ResolutionManager(new Vector2(1280, 720));
            quit = false;
            camera = new Camera2D();

            graphics.PreferredBackBufferWidth = GameRes.GetResX();
            graphics.PreferredBackBufferHeight = GameRes.GetResY();

            Content.RootDirectory = "Content";
            ContMan = Content;

            rand = new Random();
                        
            GameInput = new InputManager();
            WinMan = new WindowManager();
        }

        protected override void Initialize()
        {
            base.Initialize();

            PlayerOneIndex = PlayerIndex.One;

            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            DebugFont = Content.Load<SpriteFont>("Fonts\\Debug");

            WinMan.AddWindow(new PlayWindow());
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Graphics.Clear(Color.White);

            GameInput.UpdateNewInput();
            GameInput.UpdateNewPadInput(PlayerOneIndex);

            if (quit || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
                this.Exit();

            WinMan.Update(gameTime);

            GameInput.UpdateOldInput();
            GameInput.UpdateOldPadInput(PlayerOneIndex);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //graphics.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(); //Begin so that all 2D drawing doesn't fuck up

            WinMan.Draw(gameTime);

            spriteBatch.End(); //End the spritebatch so that all 2D drawing doesn't fuck up
            base.Draw(gameTime);
        }

        public static void Quit() { quit = true; }

        //public method to get the Resolution Scaler
        public static Vector2 GetResScaler() { return GameRes.GetResScaler(); }
        public static SpriteBatch GetSpriteBatch() { return spriteBatch; }
        public static ContentManager GetContent() { return ContMan; }
        public static InputManager GetInMan() { return GameInput; }
        public static WindowManager GetWinMan() { return WinMan; }
        public static SpriteFont GetDebugFont() { return DebugFont; }

        public static int MaxX
        {
            get { return GameRes.GetResX(); }
        }

        public static int MaxY
        {
            get { return GameRes.GetResY(); }
        }

        public static InputManager Input
        {
            get { return GameInput; }
        }

        public static Random Rand
        {
            get { return rand; }
        }

        public static Camera2D Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public static void SetCamera(Vector2 pos) { camera.Position = pos; }

        public static float WrapValueDegree(float degree)
        {
            while (degree > 360) degree -= 360;
            while (degree < 0) degree += 360;

            return degree;
        }

        public static float WrapValueRadian(float radian)
        {
            while (radian > MathHelper.TwoPi) radian -= MathHelper.TwoPi;
            while (radian < 0) radian += MathHelper.TwoPi;

            return radian;
        }

        public static GraphicsDevice Graphics
        {
            get { return graphics.GraphicsDevice; }
        }

        public static Vector2 randomPoint(int min, int max)
        {
            return new Vector2(Rand.Next(min, max), Rand.Next(min, max));
        }
    }
}
