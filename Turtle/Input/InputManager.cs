using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Turtle
{
    public enum KeyPressedState
    {
        Pressed,
        Released,
        JustPressed,
        JustReleased
    }

    public enum GamePadButtonState
    {
        Pressed,
        Released,
        JustPressed,
        JustReleased
    }

    public enum Thumbsticks
    {
        Left,
        Right
    }

    public enum ThumbstickDirection
    {
        Down,
        Left,
        Right,
        Up
    }

    public class InputManager
    {        
        private KeyboardState NewKeyState;
        private KeyboardState OldKeyState;

        private GamePadState NewGamePadState;
        private GamePadState OldGamePadState;

        public InputManager()
        {
            NewKeyState = new KeyboardState();
            OldKeyState = new KeyboardState();

            NewGamePadState = new GamePadState();
            OldGamePadState = new GamePadState();
        }

        public KeyPressedState GetKeyPressedState(Keys input)
        {
            if (NewKeyState.IsKeyDown(input))
            {
                if (OldKeyState.IsKeyDown(input))
                    return KeyPressedState.Pressed;
                else
                    return KeyPressedState.JustPressed;
            }
            else
            {
                if (OldKeyState.IsKeyDown(input))
                    return KeyPressedState.JustReleased;
                else
                    return KeyPressedState.Released;
            }
        }

        public GamePadButtonState GetGamepadButtonState(Buttons input)
        {
            if (NewGamePadState.IsButtonDown(input))
            {
                if (OldGamePadState.IsButtonDown(input))
                    return GamePadButtonState.Pressed;
                else
                    return GamePadButtonState.JustPressed;
            }
            else
            {
                if (OldGamePadState.IsButtonDown(input))
                    return GamePadButtonState.JustReleased;
                else
                    return GamePadButtonState.Released;
            }
        }

        public GamePadButtonState ThumbstickJustPressed(Thumbsticks t, ThumbstickDirection d, float val)
        {
            if (t == Thumbsticks.Left)
            {
                if (d == ThumbstickDirection.Left)
                {
                    if (NewGamePadState.ThumbSticks.Left.X < -val)
                    {
                        if (OldGamePadState.ThumbSticks.Left.X < -val)
                            return GamePadButtonState.Pressed;
                        else
                            return GamePadButtonState.JustPressed;
                    }
                    else
                    {
                        if (OldGamePadState.ThumbSticks.Left.X < -val)
                            return GamePadButtonState.JustReleased;
                        else
                            return GamePadButtonState.Released;
                    }
                }
                else if (d == ThumbstickDirection.Right)
                {
                    if (NewGamePadState.ThumbSticks.Left.X > val)
                    {
                        if (OldGamePadState.ThumbSticks.Left.X > val)
                            return GamePadButtonState.Pressed;
                        else
                            return GamePadButtonState.JustPressed;
                    }
                    else
                    {
                        if (OldGamePadState.ThumbSticks.Left.X > val)
                            return GamePadButtonState.JustReleased;
                        else
                            return GamePadButtonState.Released;
                    }
                }
                else if (d == ThumbstickDirection.Down)
                {
                    if (NewGamePadState.ThumbSticks.Left.Y < -val)
                    {
                        if (OldGamePadState.ThumbSticks.Left.Y < -val)
                            return GamePadButtonState.Pressed;
                        else
                            return GamePadButtonState.JustPressed;
                    }
                    else
                    {
                        if (OldGamePadState.ThumbSticks.Left.Y < -val)
                            return GamePadButtonState.JustReleased;
                        else
                            return GamePadButtonState.Released;
                    }
                }
                else
                {
                    if (NewGamePadState.ThumbSticks.Left.Y > val)
                    {
                        if (OldGamePadState.ThumbSticks.Left.Y > val)
                            return GamePadButtonState.Pressed;
                        else
                            return GamePadButtonState.JustPressed;
                    }
                    else
                    {
                        if (OldGamePadState.ThumbSticks.Left.Y > val)
                            return GamePadButtonState.JustReleased;
                        else
                            return GamePadButtonState.Released;
                    }
                }
            }
            else
            {
                if (d == ThumbstickDirection.Left)
                {
                    if (NewGamePadState.ThumbSticks.Right.X < -val)
                    {
                        if (OldGamePadState.ThumbSticks.Right.X < -val)
                            return GamePadButtonState.Pressed;
                        else
                            return GamePadButtonState.JustPressed;
                    }
                    else
                    {
                        if (OldGamePadState.ThumbSticks.Right.X < -val)
                            return GamePadButtonState.JustReleased;
                        else
                            return GamePadButtonState.Released;
                    }
                }
                else if (d == ThumbstickDirection.Right)
                {
                    if (NewGamePadState.ThumbSticks.Right.X > val)
                    {
                        if (OldGamePadState.ThumbSticks.Right.X > val)
                            return GamePadButtonState.Pressed;
                        else
                            return GamePadButtonState.JustPressed;
                    }
                    else
                    {
                        if (OldGamePadState.ThumbSticks.Right.X > val)
                            return GamePadButtonState.JustReleased;
                        else
                            return GamePadButtonState.Released;
                    }
                }
                else if (d == ThumbstickDirection.Down)
                {
                    if (NewGamePadState.ThumbSticks.Right.Y < -val)
                    {
                        if (OldGamePadState.ThumbSticks.Right.Y < -val)
                            return GamePadButtonState.Pressed;
                        else
                            return GamePadButtonState.JustPressed;
                    }
                    else
                    {
                        if (OldGamePadState.ThumbSticks.Right.Y < -val)
                            return GamePadButtonState.JustReleased;
                        else
                            return GamePadButtonState.Released;
                    }
                }
                else
                {
                    if (NewGamePadState.ThumbSticks.Right.Y > val)
                    {
                        if (OldGamePadState.ThumbSticks.Right.Y > val)
                            return GamePadButtonState.Pressed;
                        else
                            return GamePadButtonState.JustPressed;
                    }
                    else
                    {
                        if (OldGamePadState.ThumbSticks.Right.Y > val)
                            return GamePadButtonState.JustReleased;
                        else
                            return GamePadButtonState.Released;
                    }
                }
            }
        }

        public void UpdateNewInput() { NewKeyState = Keyboard.GetState(); }
        public void UpdateOldInput() { OldKeyState = Keyboard.GetState(); }
        public void UpdateNewPadInput(PlayerIndex p) { NewGamePadState = GamePad.GetState(p); }
        public void UpdateOldPadInput(PlayerIndex p) { OldGamePadState = GamePad.GetState(p); }
    }
}
