using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    /// <summary>
    /// Active:     An active window will update and draw
    /// Disabled:   A disabled window will draw but NOT update
    /// Hidden:     A hidden window will update but NOT draw
    /// Frozen:     A frozen window will NOT update OR draw
    /// Destroy:    A window set to destroy will be removed on window cleanup
    /// </summary>
    public enum WindowMode
    {
        Active,     //An active window will update and draw
        Disabled,   //A disabled window will draw but NOT update
        Hidden,     //A hidden window will update but NOT draw
        Frozen,     //A frozen window will NOT update OR draw
        Destroy     //A window set to destroy will be removed on cleanup
    }

    public abstract class Window
    {
        protected WindowMode Mode;
        protected string Name;

        public void WindowUpdate(GameTime gameTime)
        {
            if (Mode == WindowMode.Active || Mode == WindowMode.Hidden)
                Update(gameTime);
        }

        public void WindowDraw()
        {
            if (Mode == WindowMode.Active || Mode == WindowMode.Disabled)
                Draw();
        }

        protected abstract void Initialize();
        protected abstract void Update(GameTime gameTime);
        protected  abstract void Draw();

        public WindowMode GetWindowMode() { return Mode; }
        public string GetWindowName() { return Name; }

        public virtual void Activate() { Mode = WindowMode.Active; }
        public virtual void Disable() { Mode = WindowMode.Disabled; }
        public virtual void Hide() { Mode = WindowMode.Hidden; }
        public virtual void Freeze() { Mode = WindowMode.Frozen; }
        public virtual void Destroy() { Mode = WindowMode.Destroy; }
    }
}
