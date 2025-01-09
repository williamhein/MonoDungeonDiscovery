using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoDungeonDiscovery
{
    public enum MouseButtons
    {
        Left, Right, X1, X2, Middle
    }
    public enum GamePadTriggersThumbsticksEnum
    {
        Left, Right
    }
    public class Input
    {
        public static GamePadState[] _gamepadStates;
        public static KeyboardState _keyboardState;
        public static MouseState _mouseState;

        private static GamePadState[] _previousGamepadStates;
        private static KeyboardState _previousKeyboardState;
        private static MouseState _previousMouseState;

        public static void Initialize()
        {
            _gamepadStates = [GamePad.GetState(PlayerIndex.One), GamePad.GetState(PlayerIndex.Two), GamePad.GetState(PlayerIndex.Three), GamePad.GetState(PlayerIndex.Four)];
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
            _previousMouseState = Mouse.GetState();
        }
        public static void UpdateInputs()
        {
            GamePadState[] newGamepadStates = [GamePad.GetState(PlayerIndex.One), GamePad.GetState(PlayerIndex.Two), GamePad.GetState(PlayerIndex.Three), GamePad.GetState(PlayerIndex.Four)];
            KeyboardState newKeyboardStates = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            // Update saved state.
            _previousKeyboardState = _keyboardState;
            _keyboardState = newKeyboardStates;

            _previousGamepadStates = _gamepadStates;
            _gamepadStates = newGamepadStates;
            
            _previousMouseState = _mouseState;
            _mouseState = newMouseState;
        }

        public static bool KeyPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }
        public static bool KeyReleased(Keys key)
        {
            return !_keyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyDown(key);
        }
        public static Point GetMousePosition()
        {
            return _mouseState.Position;
        }
        public static bool MouseButtonPressed(MouseButtons button)
        {
            bool ret = false;
            switch (button)
            {
                case MouseButtons.Left:
                    ret = _mouseState.LeftButton==ButtonState.Pressed;
                    break;
                case MouseButtons.Right:
                    ret = _mouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButtons.X1:
                    ret = _mouseState.XButton1 == ButtonState.Pressed;
                    break;
                case MouseButtons.X2:
                    ret = _mouseState.XButton2 == ButtonState.Pressed;
                    break;
                case MouseButtons.Middle:
                    ret = _mouseState.MiddleButton == ButtonState.Pressed;
                    break;
            }
            return ret;
        }
        public static bool MouseButtonReleased(MouseButtons button)
        {
            bool ret = false;
            switch (button)
            {
                case MouseButtons.Left:
                    ret = _mouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButtons.Right:
                    ret = _mouseState.RightButton == ButtonState.Released && _previousMouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButtons.X1:
                    ret = _mouseState.XButton1 == ButtonState.Released && _previousMouseState.XButton1 == ButtonState.Pressed;
                    break;
                case MouseButtons.X2:
                    ret = _mouseState.XButton2 == ButtonState.Released && _previousMouseState.XButton2 == ButtonState.Pressed;
                    break;
                case MouseButtons.Middle:
                    ret = _mouseState.MiddleButton == ButtonState.Released && _previousMouseState.MiddleButton == ButtonState.Pressed;
                    break;
            }
            return ret;
        }
        public static bool GamepadButtonPressed(Buttons button, int player)
        {
            return _gamepadStates[player].IsButtonDown(button);
        }
        public static bool GamepadButtonReleased(Buttons button, int player)
        {
            return !_gamepadStates[player].IsButtonDown(button) && _previousGamepadStates[player].IsButtonDown(button);
        }
        public static float GamepadTrigger(GamePadTriggersThumbsticksEnum trigger, int player)
        {
            float ret = 0f;
            switch (trigger)
            {
                case GamePadTriggersThumbsticksEnum.Right:
                    ret = _gamepadStates[player].Triggers.Right;
                    break;
                case GamePadTriggersThumbsticksEnum.Left:
                    ret = _gamepadStates[player].Triggers.Left;
                    break;
            }
            return ret;
        }
        public static Vector2 GamepadStick(GamePadTriggersThumbsticksEnum stick, int player)
        {
            Vector2 ret = new Vector2(0);
            switch (stick)
            {
                case GamePadTriggersThumbsticksEnum.Right:
                    ret = _gamepadStates[player].ThumbSticks.Right;
                    break;
                case GamePadTriggersThumbsticksEnum.Left:
                    ret = _gamepadStates[player].ThumbSticks.Left;
                    break;
            }
            return ret;
        }
    }
}
