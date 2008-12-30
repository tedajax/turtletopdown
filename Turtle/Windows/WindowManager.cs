using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Turtle
{
    public class WindowManager
    {
        List<Window> WindowList;

        public WindowManager()
        {
            WindowList = new List<Window>();
        }

        public void AddWindow(Window newwindow)
        {
            WindowList.Add(newwindow);
        }

        public void InsertWindow(int index, Window newwindow)
        {
            WindowList.Insert(index, newwindow);
        }

        public void RemoveWindow(string name)
        {
            Window w = FindWindow(name);
            
            if (w != null)
                WindowList.Remove(w);
        }

        public Window FindWindow(string name)
        {
            for (int i = 0; i < WindowList.Count; i++)
            {
                if (WindowList[i].GetWindowName().Equals(name))
                    return WindowList[i];
            }

            return null;
        }

        public int FindWindowIndex(string name)
        {
            for (int i = 0; i < WindowList.Count; i++)
            {
                if (WindowList[i].GetWindowName().Equals(name))
                    return i;
            }

            return -1;
        }

        public bool FindAndDestroyWindow(string name)
        {
            int i = FindWindowIndex(name);
            if (i != -1)
            {
                WindowList.RemoveAt(i);
                return true;
            }
            else
                return false;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < WindowList.Count; i++)
            {
                Window w = WindowList[i];
                w.WindowUpdate(gameTime);

                if (w.GetWindowMode() == WindowMode.Destroy)
                    WindowList.Remove(w);
            }
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < WindowList.Count; i++)
            {
                WindowList[i].WindowDraw();
            }
        }
    }
}
