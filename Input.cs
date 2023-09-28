using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boids
{
    static class Input
    {
        private static KeyboardState _prevKeyboardState;
        private static KeyboardState _currentKeyboardState;
        private static MouseState _prevMouseState;
        private static MouseState _currentMouseState;

        public static Vector2 MousePosition => _currentMouseState.Position.ToVector2();

        public static void Update(GameTime gameTime)
        {
            _prevKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
            _prevMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        public static bool KeyWasPressed(Keys key)
        {
            return _prevKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key);
        }

        public static bool KeyIsPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        public static bool LeftControlClick()
        {
            return KeyIsPressed(Keys.LeftControl) && LeftMouseBtnWasPressed();
        }

        public static bool LeftMouseBtnWasPressed()
        {
            return _prevMouseState.LeftButton == ButtonState.Pressed &&
                _currentMouseState.LeftButton == ButtonState.Released;
        }

        public static bool RightMouseBtnWasPressed()
        {
            return _prevMouseState.RightButton == ButtonState.Pressed &&
                _currentMouseState.RightButton == ButtonState.Released;
        }

        internal static bool LeftShiftClick()
        {
            return KeyIsPressed(Keys.LeftShift) && LeftMouseBtnWasPressed();
        }
    }
}
